/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.stoendpoint = fanikiwa.stoendpoint || {};
fanikiwa.stoendpoint.stodetail = fanikiwa.stoendpoint.stodetail || {};

fanikiwa.stoendpoint.stodetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('stodetailsid');
	gapi.client.stoendpoint
			.retrieveSTO({
				'id' : id
			})
			.execute(
					function(resp) {
						console.log(resp);
						if (!resp.code) {
							if (resp.result.result == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								fanikiwa.stoendpoint.stodetail
										.populateControls(resp.result.clientToken);
								$('#successmessage').html('');
								$('#errormessage').html('');
								$('#apiResults').html('');
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message.toString());
							$('#successmessage').html('');
							$('#apiResults').html('');
						}
					},
					function(reason) {
						console.log('Error: ' + reason.result.error.message);
						$('#errormessage').html(
								'operation failed! Error...<br/>'
										+ reason.result.error.message
												.toString());
						$('#successmessage').html('');
						$('#apiResults').html('');
					});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.stoendpoint.stodetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.stoendpoint.stodetail.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('stoendpoint', 'v1', callback, apiRoot);

};

fanikiwa.stoendpoint.stodetail.populateControls = function(resp) {
	$("#txtStoId").val(resp.result.id);
	$("#txtDescription").val(resp.result.description);
	$("#txtAmount").val(resp.result.amount);
	$("#txtInterest").val(resp.result.interest);
	$("#txtTerm").val(resp.result.term);
	$("#txtstoees").val(resp.result.stoees);
	$("#cboStoType").val(resp.result.stoType);
	document.getElementById('chkPublicSto').checked = resp.result.publicSto;
	document.getElementById('chkPartialPay').checked = resp.result.partialPay;
	$("#dtpcreatedDate").val(formatDateForControl(resp.result.createdDate));
	$("#dtpexpiryDate").val(formatDateForControl(resp.result.expiryDate));
	$("#cbostatus").val(resp.result.status);
}
