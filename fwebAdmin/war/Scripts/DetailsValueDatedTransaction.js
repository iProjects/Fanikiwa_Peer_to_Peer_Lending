/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.valuedatedtransactionendpoint = fanikiwa.valuedatedtransactionendpoint
		|| {};
fanikiwa.valuedatedtransactionendpoint.transactiondetails = fanikiwa.valuedatedtransactionendpoint.transactiondetails
		|| {};

fanikiwa.valuedatedtransactionendpoint.transactiondetails.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('transactiondetailsid');
	gapi.client.valuedatedtransactionendpoint
			.retrieveTransaction({
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
								fanikiwa.valuedatedtransactionendpoint.transactiondetails
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

fanikiwa.valuedatedtransactionendpoint.transactiondetails.populateControls = function(
		transaction) {

	if (transaction.transactionID != undefined)
		document.getElementById('txttransactionID').value = transaction.transactionID;
	if (transaction.amount != undefined)
		document.getElementById('txtamount').value = transaction.amount;
	if (transaction.authorizer != undefined)
		document.getElementById('txtauthorizer').value = transaction.authorizer;
	if (transaction.contraReference != undefined)
		document.getElementById('txtcontraReference').value = transaction.contraReference;
	if (transaction.forcePostFlag != undefined)
		document.getElementById('txtforcePostFlag').value = transaction.forcePostFlag;
	if (transaction.narrative != undefined)
		document.getElementById('txtnarrative').value = transaction.narrative;
	if (transaction.postDate != undefined)
		document.getElementById('dtppostDate').value = formatDateForControl(transaction.postDate);
	if (transaction.recordDate != undefined)
		document.getElementById('dtprecordDate').value = formatDateForControl(transaction.recordDate);
	if (transaction.reference != undefined)
		document.getElementById('txtreference').value = transaction.reference;
	if (transaction.statementFlag != undefined)
		document.getElementById('txtstatementFlag').value = transaction.statementFlag;
	if (transaction.userID != undefined)
		document.getElementById('txtuserID').value = transaction.userID;
	if (transaction.valueDate != undefined)
		document.getElementById('txtvalueDate').value = formatDateForControl(transaction.valueDate);
	if (transaction.account != undefined)
		document.getElementById('txtaccount').value = transaction.account;
	if (transaction.transactionType != undefined)
		document.getElementById('txttransactionType').value = transaction.transactionType;
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.valuedatedtransactionendpoint.transactiondetails.init = function(
		apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.valuedatedtransactionendpoint.transactiondetails
					.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('valuedatedtransactionendpoint', 'v1', callback, apiRoot);

};
