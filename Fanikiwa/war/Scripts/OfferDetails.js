/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.offerdetail = fanikiwa.offerendpoint.offerdetail || {};

fanikiwa.offerendpoint.offerdetail.LoadOfferDetails = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('offerdetailsid');
	gapi.client.offerendpoint.getOfferByID({
		'id' : id
	}).execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.id == undefined) {
				$('#apiResults').html('failed to load offer details...');
			} else {
				fanikiwa.offerendpoint.offerdetail.populateOfferDetails(resp);
				$('#apiResults').html('');
			}
		}

	}, function(reason) {
		console.log('Error: ' + reason.result.error.message); 
		$('#errormessage').html(
				'operation failed! Error...<br/>'
						+ reason.result.error.message);
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
fanikiwa.offerendpoint.offerdetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.offerdetail.LoadOfferDetails();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.offerendpoint.offerdetail.populateOfferDetails = function(resp) {
	$("#txtOfferId").val(resp.result.id);
	$("#txtDescription").val(resp.result.description);
	$("#txtAmount").val(resp.result.amount);
	$("#txtInterest").val(resp.result.interest);
	$("#txtTerm").val(resp.result.term);
	$("#txtofferees").val(resp.result.offerees);
	$("#cboOfferType").val(resp.result.offerType);
	document.getElementById('chkPublicOffer').checked = resp.result.publicOffer;
	document.getElementById('chkPartialPay').checked = resp.result.partialPay;
	$("#dtpcreatedDate").val(formatDateForControl(resp.result.createdDate));
	$("#dtpexpiryDate").val(formatDateForControl(resp.result.expiryDate));
	$("#cbostatus").val(resp.result.status);
}
