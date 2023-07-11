/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.stoendpoint = fanikiwa.stoendpoint || {};
fanikiwa.stoendpoint.stodetails = fanikiwa.stoendpoint.stodetails || {};

fanikiwa.stoendpoint.stodetails.initializeControls = function() {
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
							if (resp.result.success == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								fanikiwa.stoendpoint.stodetails
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

fanikiwa.stoendpoint.stodetails.populateControls = function(sto) {

	if (sto.id != undefined)
		document.getElementById('txtid').value = sto.id;
	if (sto.amountDefaulted != undefined)
		document.getElementById('txtamountDefaulted').value = sto.amountDefaulted;
	if (sto.amountPaid != undefined)
		document.getElementById('txtamountPaid').value = sto.amountPaid;
	if (sto.chargeCommFlag != undefined)
		document.getElementById('chkchargeCommFlag').checked = sto.chargeCommFlag;
	if (sto.chargeWho != undefined)
		document.getElementById('txtchargeWho').value = sto.chargeWho;
	if (sto.commFreqFlag != undefined)
		document.getElementById('txtcommFreqFlag').value = sto.commFreqFlag;
	if (sto.commissionAccount != undefined)
		document.getElementById('txtcommissionAccount').value = sto.commissionAccount;
	if (sto.commissionPaidFlag != undefined)
		document.getElementById('chkcommissionPaidFlag').checked = sto.commissionPaidFlag;
	if (sto.commSourceFlag != undefined)
		document.getElementById('txtcommSourceFlag').value = sto.commSourceFlag;
	if (sto.crAccount != undefined)
		document.getElementById('txtcrAccount').value = sto.crAccount;
	if (sto.createDate != undefined)
		document.getElementById('dtpcreateDate').value = formatDateForControl(sto.createDate);
	if (sto.crTxnType != undefined)
		document.getElementById('txtcrTxnType').value = sto.crTxnType;
	if (sto.drAccount != undefined)
		document.getElementById('txtdrAccount').value = sto.drAccount;
	if (sto.drTxnType != undefined)
		document.getElementById('txtdrTxnType').value = sto.drTxnType;
	if (sto.endDate != undefined)
		document.getElementById('dtpendDate').value = formatDateForControl(sto.endDate);
	if (sto.interval != undefined)
		document.getElementById('txtinterval').value = sto.interval;
	if (sto.limitFlag != undefined)
		document.getElementById('txtlimitFlag').value = sto.limitFlag;
	if (sto.feesFlag != undefined)
		document.getElementById('txtfeesFlag').value = sto.feesFlag;
	if (sto.loanId != undefined)
		document.getElementById('txtloanId').value = sto.loanId;
	if (sto.nextPayDate != undefined)
		document.getElementById('dtpnextPayDate').value = formatDateForControl(sto.nextPayDate);
	if (sto.noOfDefaults != undefined)
		document.getElementById('txtnoOfDefaults').value = sto.noOfDefaults;
	if (sto.noOfPayments != undefined)
		document.getElementById('txtnoOfPayments').value = sto.noOfPayments;
	if (sto.noOfPaymentsMade != undefined)
		document.getElementById('txtnoOfPaymentsMade').value = sto.noOfPaymentsMade;
	if (sto.partialPay != undefined)
		document.getElementById('chkpartialPay').checked = sto.partialPay;
	if (sto.payAmount != undefined)
		document.getElementById('txtpayAmount').value = sto.payAmount;
	if (sto.interestAmount != undefined)
		document.getElementById('txtinterestAmount').value = sto.interestAmount;
	if (sto.startDate != undefined)
		document.getElementById('dtpstartDate').value = formatDateForControl(sto.startDate);
	if (sto.stoaccType != undefined)
		document.getElementById('txtstoaccType').value = sto.stoaccType;
	if (sto.stotype != undefined)
		document.getElementById('txtstotype').value = sto.stotype;
	if (sto.totalToPay != undefined)
		document.getElementById('txttotalToPay').value = sto.totalToPay;

};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.stoendpoint.stodetails.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.stoendpoint.stodetails.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('stoendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}