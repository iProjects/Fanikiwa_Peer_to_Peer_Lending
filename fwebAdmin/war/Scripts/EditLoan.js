/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.editloan = fanikiwa.loanendpoint.editloan || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.loanendpoint.editloan = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _id = sessionStorage.getItem('editloanid');
	var _amount = document.getElementById('txtamount').value;
	var _term = document.getElementById('txtterm').value;
	var _interestRate = document.getElementById('txtinterestRate').value;
	var _accruedInterest = document.getElementById('txtAccruedInterest').value;
	var _interestRateSusp = document.getElementById('txtinterestRateSusp').value;
	var _accruedIntInSusp = document.getElementById('txtaccruedIntInSusp').value;
	var _intPayingAccount = document.getElementById('txtintPayingAccount').value;
	var _intPaidAccount = document.getElementById('txtintPaidAccount').value;
	var _borrowerId = document.getElementById('txtborrowerId').value;
	var _lenderId = document.getElementById('txtlenderId').value;
	var _offerId = document.getElementById('txtofferId').value;
	var _transactionType = document.getElementById('txttransactionType').value;
	var _interestAccrualInterval = document
			.getElementById('cbointerestAccrualInterval').value;
	var _interestComputationMethod = document
			.getElementById('cbointerestComputationMethod').value;
	var _interestComputationTerm = document
			.getElementById('cbointerestComputationTerm').value;
	var _interestApplicationMethod = document
			.getElementById('cbointerestApplicationMethod').value;
	var _accrueInSusp = document.getElementById('chkaccrueInSusp').checked;
	var _partialPay = document.getElementById('chkPartialPay').checked;
	var _createdDate = document.getElementById('dtpcreatedDate').value;
	var _maturityDate = document.getElementById('dtpmaturityDate').value;
	var _lastIntAccrualDate = document.getElementById('dtplastIntAccrualDate').value;
	var _nextIntAccrualDate = document.getElementById('dtpnextIntAccrualDate').value;
	var _lastIntAppDate = document.getElementById('dtplastIntAppDate').value;
	var _nextIntAppDate = document.getElementById('dtpnextIntAppDate').value;

	// if (_description.length == 0) {
	// errormsg += '<li>' + " Description cannot be null " + '</li>';
	// error_free = false;
	// }

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

	$('#apiResults').html('updating chart of account...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var loan = {};
	loan.id = _id;
	loan.amount = _amount;
	loan.term = _term;
	loan.interestRate = _interestRate;
	loan.accruedInterest = _accruedInterest;
	loan.interestRateSusp = _interestRateSusp;
	loan.accruedIntInSusp = _accruedIntInSusp;
	loan.intPayingAccount = _intPayingAccount;
	loan.intPaidAccount = _intPaidAccount;
	loan.borrowerId = _borrowerId;
	loan.lenderId = _lenderId;
	loan.offerId = _offerId;
	loan.transactionType = _transactionType;
	loan.interestAccrualInterval = _interestAccrualInterval;
	loan.interestComputationMethod = _interestComputationMethod;
	loan.interestComputationTerm = _interestComputationTerm;
	loan.interestApplicationMethod = _interestApplicationMethod;
	loan.accrueInSusp = _accrueInSusp;
	loan.partialPay = _partialPay;
	loan.createdDate = _createdDate;
	loan.maturityDate = _maturityDate;
	loan.lastIntAccrualDate = _lastIntAccrualDate;
	loan.nextIntAccrualDate = _nextIntAccrualDate;
	loan.lastIntAppDate = _lastIntAppDate;
	loan.nextIntAppDate = _nextIntAppDate;

	gapi.client.loanendpoint
			.editLoan(loan)
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
												'window.location.href = "/Views/Loan/List.html";',
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

fanikiwa.loanendpoint.editloan.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editloanid');
	gapi.client.loanendpoint
			.retrieveLoan({
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
								fanikiwa.loanendpoint.editloan
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
 * Enables the button callbacks in the UI.
 */
fanikiwa.loanendpoint.editloan.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.loanendpoint.editloan();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.loanendpoint.editloan.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.editloan.populateInterestComputationMethod();
			fanikiwa.loanendpoint.editloan.populateInterestComputationTerm();
			fanikiwa.loanendpoint.editloan.populateInterestAccrualInterval();
			fanikiwa.loanendpoint.editloan.populateInterestApplicationMethod();
			fanikiwa.loanendpoint.editloan.enableButtons();
			fanikiwa.loanendpoint.editloan.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

fanikiwa.loanendpoint.editloan.populateControls = function(loan) {
	if (loan.id != undefined)
		document.getElementById('txtLoanId').value = loan.id;
	if (loan.amount != undefined)
		document.getElementById('txtamount').value = loan.amount;
	if (loan.term != undefined)
		document.getElementById('txtterm').value = loan.term;
	if (loan.interestRate != undefined)
		document.getElementById('txtinterestRate').value = loan.interestRate;
	if (loan.accruedInterest != undefined)
		document.getElementById('txtAccruedInterest').value = loan.accruedInterest;
	if (loan.interestRateSusp != undefined)
		document.getElementById('txtinterestRateSusp').value = loan.interestRateSusp;
	if (loan.accruedIntInSusp != undefined)
		document.getElementById('txtaccruedIntInSusp').value = loan.accruedIntInSusp;
	if (loan.intPayingAccount != undefined)
		document.getElementById('txtintPayingAccount').value = loan.intPayingAccount;
	if (loan.intPaidAccount != undefined)
		document.getElementById('txtintPaidAccount').value = loan.intPaidAccount;
	if (loan.borrowerId != undefined)
		document.getElementById('txtborrowerId').value = loan.borrowerId;
	if (loan.lenderId != undefined)
		document.getElementById('txtlenderId').value = loan.lenderId;
	if (loan.offerId != undefined)
		document.getElementById('txtofferId').value = loan.offerId;
	if (loan.transactionType != undefined)
		document.getElementById('txttransactionType').value = loan.transactionType;
	if (loan.interestAccrualInterval != undefined)
		document.getElementById('cbointerestAccrualInterval').value = loan.interestAccrualInterval;
	if (loan.interestComputationMethod != undefined)
		document.getElementById('cbointerestComputationMethod').value = loan.interestComputationMethod;
	if (loan.interestComputationTerm != undefined)
		document.getElementById('cbointerestComputationTerm').value = loan.interestComputationTerm;
	if (loan.interestApplicationMethod != undefined)
		document.getElementById('cbointerestApplicationMethod').value = loan.interestApplicationMethod;
	if (loan.accrueInSusp != undefined)
		document.getElementById('chkaccrueInSusp').value = loan.accrueInSusp;
	if (loan.partialPay != undefined)
		document.getElementById('chkPartialPay').value = loan.partialPay;
	if (loan.createdDate != undefined)
		document.getElementById('dtpcreatedDate').value = formatDateForControl(loan.createdDate);
	if (loan.maturityDate != undefined)
		document.getElementById('dtpmaturityDate').value = formatDateForControl(loan.maturityDate);
	if (loan.lastIntAccrualDate != undefined)
		document.getElementById('dtplastIntAccrualDate').value = formatDateForControl(loan.lastIntAccrualDate);
	if (loan.nextIntAccrualDate != undefined)
		document.getElementById('dtpnextIntAccrualDate').value = formatDateForControl(loan.nextIntAccrualDate);
	if (loan.lastIntAppDate != undefined)
		document.getElementById('dtplastIntAppDate').value = formatDateForControl(loan.lastIntAppDate);
	if (loan.nextIntAppDate != undefined)
		document.getElementById('dtpnextIntAppDate').value = formatDateForControl(loan.nextIntAppDate);
}

fanikiwa.loanendpoint.editloan.populateInterestComputationMethod = function() {
	var interestComputationMethodarray = [ {
		id : "Simple",
		description : "Simple"
	}, {
		id : "Compound",
		description : "Compound"
	} ];
	var interestComputationMethodoptions = '';
	for (var i = 0; i < interestComputationMethodarray.length; i++) {
		interestComputationMethodoptions += '<option value="'
				+ interestComputationMethodarray[i].id + '">'
				+ interestComputationMethodarray[i].description + '</option>';
	}
	$("#cbointerestComputationMethod").append(interestComputationMethodoptions);
};

fanikiwa.loanendpoint.editloan.populateInterestComputationTerm = function() {
	var interestComputationTermarray = [ {
		id : "Daily",
		description : "Daily"
	}, {
		id : "Monthly",
		description : "Monthly"
	}, {
		id : "Yearly",
		description : "Yearly"
	} ];
	var interestComputationTermoptions = '';
	for (var i = 0; i < interestComputationTermarray.length; i++) {
		interestComputationTermoptions += '<option value="'
				+ interestComputationTermarray[i].id + '">'
				+ interestComputationTermarray[i].description + '</option>';
	}
	$("#cbointerestComputationTerm").append(interestComputationTermoptions);
};

fanikiwa.loanendpoint.editloan.populateInterestAccrualInterval = function() {
	var interestAccrualIntervalarray = [ {
		id : "Daily",
		description : "Daily"
	}, {
		id : "Monthly",
		description : "Monthly"
	}, {
		id : "Yearly",
		description : "Yearly"
	} ];
	var interestAccrualIntervaloptions = '';
	for (var i = 0; i < interestAccrualIntervalarray.length; i++) {
		interestAccrualIntervaloptions += '<option value="'
				+ interestAccrualIntervalarray[i].id + '">'
				+ interestAccrualIntervalarray[i].description + '</option>';
	}
	$("#cbointerestAccrualInterval").append(interestAccrualIntervaloptions);
};

fanikiwa.loanendpoint.editloan.populateInterestApplicationMethod = function() {
	var interestApplicationMethodarray = [ {
		id : "Monthly",
		description : "Monthly"
	}, {
		id : "Installment",
		description : "When Installment Goes Through"
	}, {
		id : "All",
		description : "When Loan is Finally Paid"
	} ];
	var interestApplicationMethodoptions = '';
	for (var i = 0; i < interestApplicationMethodarray.length; i++) {
		interestApplicationMethodoptions += '<option value="'
				+ interestApplicationMethodarray[i].id + '">'
				+ interestApplicationMethodarray[i].description + '</option>';
	}
	$("#cbointerestApplicationMethod").append(interestApplicationMethodoptions);
};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}
