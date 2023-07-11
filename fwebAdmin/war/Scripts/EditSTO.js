/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.stoendpoint = fanikiwa.stoendpoint || {};
fanikiwa.stoendpoint.editsto = fanikiwa.stoendpoint.editsto || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.stoendpoint.editsto = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _id = sessionStorage.getItem('editstoid');
	var _amountDefaulted = document.getElementById('txtamountDefaulted').value;
	var _amountPaid = document.getElementById('txtamountPaid').value;
	var _chargeCommFlag = document.getElementById('chkchargeCommFlag').checked;
	var _chargeWho = document.getElementById('txtchargeWho').value;
	var _commFreqFlag = document.getElementById('txtcommFreqFlag').value;
	var _commissionAccount = document.getElementById('txtcommissionAccount').value;
	var _commissionPaidFlag = document.getElementById('chkcommissionPaidFlag').checked;
	var _commSourceFlag = document.getElementById('txtcommSourceFlag').value;
	var _crAccount = document.getElementById('txtcrAccount').value;
	var _createDate = document.getElementById('dtpcreateDate').value;
	var _crTxnType = document.getElementById('txtcrTxnType').value;
	var _drAccount = document.getElementById('txtdrAccount').value;
	var _drTxnType = document.getElementById('txtdrTxnType').value;
	var _endDate = document.getElementById('dtpendDate').value;
	var _interval = document.getElementById('txtinterval').value;
	var _limitFlag = document.getElementById('txtlimitFlag').value;
	var _feesFlag = document.getElementById('txtfeesFlag').value;
	var _loanId = document.getElementById('txtloanId').value;
	var _nextPayDate = document.getElementById('dtpnextPayDate').value;
	var _noOfDefaults = document.getElementById('txtnoOfDefaults').value;
	var _noOfPayments = document.getElementById('txtnoOfPayments').value;
	var _noOfPaymentsMade = document.getElementById('txtnoOfPaymentsMade').value;
	var _partialPay = document.getElementById('chkpartialPay').checked;
	var _payAmount = document.getElementById('txtpayAmount').value;
	var _interestAmount = document.getElementById('txtinterestAmount').value;
	var _startDate = document.getElementById('dtpstartDate').value;
	var _stoaccType = document.getElementById('txtstoaccType').value;
	var _stotype = document.getElementById('txtstotype').value;
	var _totalToPay = document.getElementById('txttotalToPay').value;

	if (!error_free) {
		errormsg += "</ul>";
		$("#error-display-div").html(errormsg);
		$("#error-display-div").removeClass('displaynone');
		$("#error-display-div").addClass('displayblock');
		$("#error-display-div").show();
		return;
	} else {
		ClearException();
	}

	$('#apiResults').html('updating customer...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var sto = {};
	sto.id = _id;
	sto.amountDefaulted = _amountDefaulted;
	sto.amountPaid = _amountPaid;
	sto.chargeCommFlag = _chargeCommFlag;
	sto.chargeWho = _chargeWho;
	sto.commFreqFlag = _commFreqFlag;
	sto.commissionAccount = _commissionAccount;
	sto.commissionAmount = _commissionAmount;
	sto.commissionPaidFlag = _commissionPaidFlag;
	sto.commSourceFlag = _commSourceFlag;
	sto.crAccount = _crAccount;
	sto.createDate = _createDate;
	sto.crTxnType = _crTxnType;
	sto.drAccount = _drAccount;
	sto.drTxnType = _drTxnType;
	sto.endDate = _endDate;
	sto.interval = _interval;
	sto.limitFlag = _limitFlag;
	sto.feesFlag = _feesFlag;
	sto.loanId = _loanId;
	sto.nextPayDate = _nextPayDate;
	sto.noOfDefaults = _noOfDefaults;
	sto.noOfPayments = _noOfPayments;
	sto.noOfPaymentsMade = _noOfPaymentsMade;
	sto.partialPay = _partialPay;
	sto.payAmount = _payAmount;
	sto.interestAmount = _interestAmount;
	sto.startDate = _startDate;
	sto.stoaccType = _stoaccType;
	sto.stotype = _stotype;
	sto.totalToPay = _totalToPay;

	gapi.client.stoendpoint
			.updateSTO(sto)
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp.result.success == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								$('#successmessage').html(
										'operation successful... <br/>'
												+ resp.result.resultMessage
														.toString());
								$('#errormessage').html('');
								$('#apiResults').html('');
								window
										.setTimeout(
												'window.location.href = "/Views/STO/List.html";',
												1000);
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message);
							$('#successmessage').html('');
							$('#apiResults').html('');
						}

					},
					function(reason) {
						console.log('Error: ' + reason.result.error.message);
						$('#errormessage').html(
								'operation failed! Error...<br/>'
										+ reason.result.error.message);
						$('#successmessage').html('');
						$('#apiResults').html('');
					});
};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.stoendpoint.editsto.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.stoendpoint.editsto();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.stoendpoint.editsto.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.stoendpoint.editsto.enableButtons();
			fanikiwa.stoendpoint.editsto.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('stoendpoint', 'v1', callback, apiRoot);

};

fanikiwa.stoendpoint.editsto.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editstoid');
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
								fanikiwa.stoendpoint.editsto
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

fanikiwa.stoendpoint.editsto.populateControls = function(sto) {

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

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}