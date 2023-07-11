/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.mpesaipnmessageendpoint = fanikiwa.mpesaipnmessageendpoint || {};
fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail = fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail
		|| {};

fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('mpesaipnmessagedetailsid');
	gapi.client.mpesaipnmessageendpoint
			.retrieveMpesaIPNMessage({
				'id' : id
			})
			.execute(
					function(resp) {
						console.log(resp);
						if (!resp.code) {
							if (resp.result.success == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail
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
}

fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail.populateControls = function(
		mpesaipnmessage) {

	if (mpesaipnmessage.id != undefined)
		document.getElementById('txtid').value = mpesaipnmessage.id;
	if (mpesaipnmessage.mpesaIPNMessageID != undefined)
		document.getElementById('txtmpesaIPNMessageID').value = mpesaipnmessage.mpesaIPNMessageID;
	if (mpesaipnmessage.orig != undefined)
		document.getElementById('txtorig').value = mpesaipnmessage.orig;
	if (mpesaipnmessage.dest != undefined)
		document.getElementById('txtdest').value = mpesaipnmessage.dest;
	if (mpesaipnmessage.tstamp != undefined)
		document.getElementById('txttstamp').value = mpesaipnmessage.tstamp;
	if (mpesaipnmessage.text != undefined)
		document.getElementById('txttext').value = mpesaipnmessage.text;
	if (mpesaipnmessage.user != undefined)
		document.getElementById('txtuser').value = mpesaipnmessage.user;
	if (mpesaipnmessage.pass != undefined)
		document.getElementById('txtpass').value = mpesaipnmessage.pass;
	if (mpesaipnmessage.mpesa_code != undefined)
		document.getElementById('txtmpesa_code').value = mpesaipnmessage.mpesa_code;
	if (mpesaipnmessage.mpesa_acc != undefined)
		document.getElementById('txtmpesa_acc').value = mpesaipnmessage.mpesa_acc;
	if (mpesaipnmessage.mpesa_msisdn != undefined)
		document.getElementById('txtmpesa_msisdn').value = mpesaipnmessage.mpesa_msisdn;
	if (mpesaipnmessage.mpesa_trx_date != undefined)
		document.getElementById('txtmpesa_trx_date').value = mpesaipnmessage.mpesa_trx_date;
	if (mpesaipnmessage.mpesa_trx_time != undefined)
		document.getElementById('txtmpesa_trx_time').value = mpesaipnmessage.mpesa_trx_time;
	if (mpesaipnmessage.mpesa_amt != undefined)
		document.getElementById('txtmpesa_amt').value = mpesaipnmessage.mpesa_amt;
	if (mpesaipnmessage.mpesa_sender != undefined)
		document.getElementById('txtmpesa_sender').value = mpesaipnmessage.mpesa_sender;
	if (mpesaipnmessage.status != undefined)
		document.getElementById('txtstatus').value = mpesaipnmessage.status;

};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.mpesaipnmessageendpoint.mpesaipnmessagedetail
					.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('mpesaipnmessageendpoint', 'v1', callback, apiRoot);

};
