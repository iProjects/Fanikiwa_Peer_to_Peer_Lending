/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.accountdetails = fanikiwa.accountendpoint.accountdetails
		|| {};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.accountendpoint.accountdetails.enableButtons = function() {

	document.getElementById('txtaccruedInt').value = 0;
	document.getElementById('txtinterestRateSusp').value = 0;
	document.getElementById('txtaccruedIntInSusp').value = 0;
	document.getElementById('txtlimitCheckFlag').value = 0;
	document.getElementById('txtinterestRate').value = 0;

	document.getElementById('dtpmaturityDate').value = formatDateForControl(new Date());
	document.getElementById('dtplastIntAccrualDate').value = formatDateForControl(new Date());
	document.getElementById('dtpnextIntAccrualDate').value = formatDateForControl(new Date());
	document.getElementById('dtplastIntAppDate').value = formatDateForControl(new Date());
	document.getElementById('dtpnextIntAppDate').value = formatDateForControl(new Date());

};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.accountendpoint.accountdetails.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.accountdetails.populatePassFlag();
			fanikiwa.accountendpoint.accountdetails.populateLimitFlag();
			fanikiwa.accountendpoint.accountdetails.populateCoa();
			fanikiwa.accountendpoint.accountdetails.populateAccountTypes();
			fanikiwa.accountendpoint.accountdetails.populateCustomers();
			fanikiwa.accountendpoint.accountdetails.populatePayAccounts();
			fanikiwa.accountendpoint.accountdetails
					.populateInterestComputationMethod();
			fanikiwa.accountendpoint.accountdetails
					.populateInterestComputationTerm();
			fanikiwa.accountendpoint.accountdetails
					.populateInterestAccrualInterval();
			fanikiwa.accountendpoint.accountdetails
					.populateInterestApplicationMethod();
			fanikiwa.accountendpoint.accountdetails.enableButtons();
			fanikiwa.accountendpoint.accountdetails.initializeControls();
		}
	}

	apisToLoad = 4; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);
	gapi.client.load('accounttypeendpoint', 'v1', callback, apiRoot);
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accountendpoint.accountdetails.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('accountdetailsid');
	gapi.client.accountendpoint
			.retrieveAccount({
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
								fanikiwa.accountendpoint.accountdetails
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

fanikiwa.accountendpoint.accountdetails.populateControls = function(account) {

	if (account.accountID != undefined)
		document.getElementById('txtaccountID').value = account.accountID;
	if (account.accountName != undefined)
		document.getElementById('txtaccountName').value = account.accountName;
	if (account.accountNo != undefined)
		document.getElementById('txtaccountNo').value = account.accountNo;
	if (account.bookBalance != undefined)
		document.getElementById('txtbookBalance').value = account.bookBalance;
	if (account.clearedBalance != undefined)
		document.getElementById('txtclearedBalance').value = account.clearedBalance;
	if (account.customer != undefined)
		document.getElementById('cbocustomer').value = account.customer;
	if (account.coadet != undefined)
		document.getElementById('cbocoadet').value = account.coadet;
	if (account.accounttype != undefined)
		document.getElementById('cboaccounttype').value = account.accounttype;
	if (account.accounttype != undefined)
		document.getElementById('txtlimitCheckFlag').value = account.limitCheckFlag;
	if (account.limitFlag != undefined)
		document.getElementById('cbolimitFlag').value = account.limitFlag;
	if (account.passFlag != undefined)
		document.getElementById('cbopassFlag').value = account.passFlag;
	if (account.accruedInt != undefined)
		document.getElementById('txtaccruedInt').value = account.accruedInt;
	if (account.limit != undefined)
		document.getElementById('txtlimit').value = account.limit;
	if (account.interestRate != undefined)
		document.getElementById('txtinterestRate').value = account.interestRate;
	if (account.closed != undefined)
		document.getElementById('chkclosed').checked = account.closed;
	if (account.intPayAccount != undefined)
		document.getElementById('cbointPayAccount').value = account.intPayAccount;
	if (account.interestComputationMethod != undefined)
		document.getElementById('cbointerestComputationMethod').value = account.interestComputationMethod;
	if (account.interestComputationTerm != undefined)
		document.getElementById('cbointerestComputationTerm').value = account.interestComputationTerm;
	if (account.interestAccrualInterval != undefined)
		document.getElementById('cbointerestAccrualInterval').value = account.interestAccrualInterval;
	if (account.interestApplicationMethod != undefined)
		document.getElementById('cbointerestApplicationMethod').value = account.interestApplicationMethod;
	if (account.interestRateSusp != undefined)
		document.getElementById('txtinterestRateSusp').value = account.interestRateSusp;
	if (account.accruedIntInSusp != undefined)
		document.getElementById('txtaccruedIntInSusp').value = account.accruedIntInSusp;
	if (account.maturityDate != undefined)
		document.getElementById('dtpmaturityDate').value = formatDateForControl(account.maturityDate);
	if (account.lastIntAccrualDate != undefined)
		document.getElementById('dtplastIntAccrualDate').value = formatDateForControl(account.lastIntAccrualDate);
	if (account.nextIntAccrualDate != undefined)
		document.getElementById('dtpnextIntAccrualDate').value = formatDateForControl(account.nextIntAccrualDate);
	if (account.lastIntAppDate != undefined)
		document.getElementById('dtplastIntAppDate').value = formatDateForControl(account.lastIntAppDate);
	if (account.nextIntAppDate != undefined)
		document.getElementById('dtpnextIntAppDate').value = formatDateForControl(account.nextIntAppDate);
	if (account.accrueInSusp != undefined)
		document.getElementById('chkaccrueInSusp').checked = account.accrueInSusp;

};

fanikiwa.accountendpoint.accountdetails.populatePassFlag = function() {
	var passflagarray = [ {
		id : "Ok",
		description : "Ok"
	}, {
		id : "DebitPostingProhibited",
		description : "DebitPostingProhibited"
	}, {
		id : "CreditPostingProhibited",
		description : "CreditPostingProhibited"
	}, {
		id : "AllPostingProhibited",
		description : "AllPostingProhibited"
	}, {
		id : "Locked",
		description : "Locked"
	}, {
		id : "Unknown",
		description : "Unknown"
	} ];
	var passflagoptions = '';
	for (var i = 0; i < passflagarray.length; i++) {
		passflagoptions += '<option value="' + passflagarray[i].id + '">'
				+ passflagarray[i].description + '</option>';
	}
	$("#cbopassFlag").append(passflagoptions);
};

fanikiwa.accountendpoint.accountdetails.populateLimitFlag = function() {
	var limitFlagarray = [ {
		id : "Ok",
		description : "Ok"
	}, {
		id : "PostingNoLimitChecking",
		description : "PostingNoLimitChecking"
	}, {
		id : "PostingOverDrawingProhibited",
		description : "PostingOverDrawingProhibited"
	}, {
		id : "PostingDrawingOnUnclearedEffectsAllowed",
		description : "PostingDrawingOnUnclearedEffectsAllowed"
	}, {
		id : "LimitsAllowed",
		description : "LimitsAllowed"
	}, {
		id : "LimitForAdvanceProhibited",
		description : "LimitForAdvanceProhibited"
	}, {
		id : "LimitForBlockingProhibited",
		description : "LimitForBlockingProhibited"
	}, {
		id : "AllLimitsProhibited",
		description : "AllLimitsProhibited"
	}, {
		id : "Unknown",
		description : "Unknown"
	} ];
	var limitFlagoptions = '';
	for (var i = 0; i < limitFlagarray.length; i++) {
		limitFlagoptions += '<option value="' + limitFlagarray[i].id + '">'
				+ limitFlagarray[i].description + '</option>';
	}
	$("#cbolimitFlag").append(limitFlagoptions);
};

fanikiwa.accountendpoint.accountdetails.populateCoa = function() {
	var coadetoptions = '';
	gapi.client.coadetendpoint.selectCoadet().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							coadetoptions += '<option value="'
									+ resp.result.items[i].id + '">'
									+ resp.result.items[i].description
									+ '</option>';
						}
						$("#cbocoadet").append(coadetoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.accountendpoint.accountdetails.populateAccountTypes = function() {
	var accounttypesoptions = '';
	gapi.client.accounttypeendpoint.listAccountType().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							accounttypesoptions += '<option value="'
									+ resp.result.items[i].id + '">'
									+ resp.result.items[i].description
									+ '</option>';
						}
						$("#cboaccounttype").append(accounttypesoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.accountendpoint.accountdetails.populateCustomers = function() {
	var customeroptions = '';
	gapi.client.customerendpoint.listCustomer().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							customeroptions += '<option value="'
									+ resp.result.items[i].customerId + '">'
									+ resp.result.items[i].name + '</option>';
						}
						$("#cbocustomer").append(customeroptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.accountendpoint.accountdetails.populatePayAccounts = function() {
	var accountoptions = '';
	gapi.client.accountendpoint.listAccount().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							accountoptions += '<option value="'
									+ resp.result.items[i].accountID + '">'
									+ resp.result.items[i].accountName
									+ '</option>';
						}
						$("#cbointPayAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.accountendpoint.accountdetails.populateInterestComputationMethod = function() {
	var interestComputationMethodarray = [ {
		id : "S",
		description : "Simple"
	}, {
		id : "C",
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

fanikiwa.accountendpoint.accountdetails.populateInterestComputationTerm = function() {
	var interestComputationTermarray = [ {
		id : "D1",
		description : "D1"
	}, {
		id : "D360",
		description : "D360"
	}, {
		id : "D365",
		description : "D365"
	}, {
		id : "M1",
		description : "M1"
	}, {
		id : "M30",
		description : "M30"
	}, {
		id : "Y",
		description : "Y"
	} ];
	var interestComputationTermoptions = '';
	for (var i = 0; i < interestComputationTermarray.length; i++) {
		interestComputationTermoptions += '<option value="'
				+ interestComputationTermarray[i].id + '">'
				+ interestComputationTermarray[i].description + '</option>';
	}
	$("#cbointerestComputationTerm").append(interestComputationTermoptions);
};

fanikiwa.accountendpoint.accountdetails.populateInterestAccrualInterval = function() {
	var interestAccrualIntervalarray = [ {
		id : "D",
		description : "Daily"
	}, {
		id : "M",
		description : "Monthly"
	}, {
		id : "Y",
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

fanikiwa.accountendpoint.accountdetails.populateInterestApplicationMethod = function() {
	var interestApplicationMethodarray = [ {
		id : "M",
		description : "Monthly"
	}, {
		id : "Inst",
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