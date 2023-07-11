/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.loandetail = fanikiwa.loanendpoint.loandetail || {};

fanikiwa.loanendpoint.loandetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('loandetailsid');
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
								fanikiwa.loanendpoint.loandetail
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
fanikiwa.loanendpoint.loandetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.loandetail.populateInterestComputationMethod();
			fanikiwa.loanendpoint.loandetail.populateInterestComputationTerm();
			fanikiwa.loanendpoint.loandetail.populateInterestAccrualInterval();
			fanikiwa.loanendpoint.loandetail.populateInterestApplicationMethod();
			fanikiwa.loanendpoint.loandetail.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

fanikiwa.loanendpoint.loandetail.populateControls = function(loan) {
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

fanikiwa.loanendpoint.loandetail.populateInterestComputationMethod = function() {
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

fanikiwa.loanendpoint.loandetail.populateInterestComputationTerm = function() {
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

fanikiwa.loanendpoint.loandetail.populateInterestAccrualInterval = function() {
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

fanikiwa.loanendpoint.loandetail.populateInterestApplicationMethod = function() {
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
