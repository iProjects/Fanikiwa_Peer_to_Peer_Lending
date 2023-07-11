/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.editaccount = fanikiwa.accountendpoint.editaccount
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.editaccount = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _accountID = sessionStorage.getItem('editaccountid');
	var _accountName = document.getElementById('txtaccountName').value;
	var _accountNo = document.getElementById('txtaccountNo').value;
	var _bookBalance = document.getElementById('txtbookBalance').value;
	var _clearedBalance = document.getElementById('txtclearedBalance').value;
	var _limit = document.getElementById('txtlimit').value;
	var _customer = document.getElementById('txtcustomer').value;
	var _coadet = document.getElementById('cbocoadet').value;
	var _accounttype = document.getElementById('cboaccounttype').value;
	var _limitCheckFlag = document.getElementById('txtlimitCheckFlag').value;
	var _limitFlag = document.getElementById('cbolimitFlag').value;
	var _passFlag = document.getElementById('cbopassFlag').value;
	var _accruedInt = document.getElementById('txtaccruedInt').value;
	var _interestRate = document.getElementById('txtinterestRate').value;
	var _closed = document.getElementById('chkclosed').checked;
	var _intPayAccount = document.getElementById('txtintPayAccount').value;
	var _interestComputationMethod = document
			.getElementById('cbointerestComputationMethod').value;
	var _interestComputationTerm = document
			.getElementById('cbointerestComputationTerm').value;
	var _interestAccrualInterval = document
			.getElementById('cbointerestAccrualInterval').value;
	var _interestApplicationMethod = document
			.getElementById('cbointerestApplicationMethod').value;
	var _interestRateSusp = document.getElementById('txtinterestRateSusp').value;
	var _accruedIntInSusp = document.getElementById('txtaccruedIntInSusp').value;
	var _maturityDate = document.getElementById('dtpmaturityDate').value;
	var _lastIntAccrualDate = document.getElementById('dtplastIntAccrualDate').value;
	var _nextIntAccrualDate = document.getElementById('dtpnextIntAccrualDate').value;
	var _lastIntAppDate = document.getElementById('dtplastIntAppDate').value;
	var _nextIntAppDate = document.getElementById('dtpnextIntAppDate').value;
	var _accrueInSusp = document.getElementById('chkaccrueInSusp').checked;
	var _maturityDate = document.getElementById('dtpmaturityDate').value;
	var _lastIntAccrualDate = document.getElementById('dtplastIntAccrualDate').value;
	var _nextIntAccrualDate = document.getElementById('dtpnextIntAccrualDate').value;
	var _branch = document.getElementById('txtbranch').value;

	if (_accountName.length == 0) {
		errormsg += '<li>' + " Account Name cannot be null " + '</li>';
		error_free = false;
	}
	if (_customer.length == 0) {
		errormsg += '<li>' + " Customer ID cannot be null " + '</li>';
		error_free = false;
	}
	if (_coadet.length == 0 || _coadet == -1) {
		errormsg += '<li>' + " Select Chart Of Account " + '</li>';
		error_free = false;
	}
	if (_accounttype.length == 0 || _coadet == -1) {
		errormsg += '<li>' + " Select Account Type " + '</li>';
		error_free = false;
	}
	if (_limitFlag.length == 0) {
		errormsg += '<li>' + " Select Limit Flag " + '</li>';
		error_free = false;
	}
	if (_passFlag.length == 0) {
		errormsg += '<li>' + " Select Pass Flag " + '</li>';
		error_free = false;
	}

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

	$('#apiResults').html('updating account...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var accountDTO = {};
	accountDTO.accountID = _accountID;
	accountDTO.accountName = _accountName;
	accountDTO.accountNo = _accountNo;
	accountDTO.bookBalance = _bookBalance;
	accountDTO.clearedBalance = _clearedBalance;
	accountDTO.limit = _limit;
	accountDTO.customer = _customer;
	accountDTO.coadet = _coadet;
	accountDTO.accounttype = _accounttype;
	accountDTO.limitCheckFlag = _limitCheckFlag;
	accountDTO.limitFlag = _limitFlag;
	accountDTO.passFlag = _passFlag;
	accountDTO.accruedInt = _accruedInt;
	accountDTO.interestRate = _interestRate;
	accountDTO.closed = _closed;
	accountDTO.intPayAccount = _intPayAccount;
	accountDTO.interestComputationMethod = _interestComputationMethod;
	accountDTO.interestComputationTerm = _interestComputationTerm;
	accountDTO.interestAccrualInterval = _interestAccrualInterval;
	accountDTO.interestApplicationMethod = _interestApplicationMethod;
	accountDTO.interestRateSusp = _interestRateSusp;
	accountDTO.accruedIntInSusp = _accruedIntInSusp;
	accountDTO.maturityDate = _maturityDate;
	accountDTO.lastIntAccrualDate = _lastIntAccrualDate;
	accountDTO.nextIntAccrualDate = _nextIntAccrualDate;
	accountDTO.lastIntAppDate = _lastIntAppDate;
	accountDTO.nextIntAppDate = _nextIntAppDate;
	accountDTO.accrueInSusp = _accrueInSusp;
	accountDTO.maturityDate = _maturityDate;
	accountDTO.lastIntAccrualDate = _lastIntAccrualDate;
	accountDTO.nextIntAccrualDate = _nextIntAccrualDate;
	accountDTO.branch = _branch;

	gapi.client.accountendpoint
			.editAccount(accountDTO)
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
												'window.location.href = "/Views/Account/List.html";',
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
fanikiwa.accountendpoint.editaccount.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.accountendpoint.editaccount();
	});

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
fanikiwa.accountendpoint.editaccount.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.editaccount.populatePassFlag();
			fanikiwa.accountendpoint.editaccount.populateLimitFlag();
			fanikiwa.accountendpoint.editaccount.populateCoa();
			fanikiwa.accountendpoint.editaccount.populateAccountTypes();
			fanikiwa.accountendpoint.editaccount
					.populateInterestComputationMethod();
			fanikiwa.accountendpoint.editaccount
					.populateInterestComputationTerm();
			fanikiwa.accountendpoint.editaccount
					.populateInterestAccrualInterval();
			fanikiwa.accountendpoint.editaccount
					.populateInterestApplicationMethod();
			fanikiwa.accountendpoint.editaccount.enableButtons();
			fanikiwa.accountendpoint.editaccount.initializeControls();
		}
	}

	apisToLoad = 4; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);
	gapi.client.load('accounttypeendpoint', 'v1', callback, apiRoot);
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accountendpoint.editaccount.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editaccountid');
	gapi.client.accountendpoint
			.retrieveAccount({
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
								fanikiwa.accountendpoint.editaccount
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

fanikiwa.accountendpoint.editaccount.populateControls = function(account) {

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
	if (account.limit != undefined)
		document.getElementById('txtlimit').value = account.limit;
	if (account.customer != undefined)
		document.getElementById('txtcustomer').value = account.customer;
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
	if (account.interestRate != undefined)
		document.getElementById('txtinterestRate').value = account.interestRate;
	if (account.closed != undefined)
		document.getElementById('chkclosed').checked = account.closed;
	if (account.intPayAccount != undefined)
		document.getElementById('txtintPayAccount').value = account.intPayAccount;
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
	if (account.branch != undefined)
		document.getElementById('txtbranch').value = account.branch;

};

fanikiwa.accountendpoint.editaccount.populatePassFlag = function() {
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

fanikiwa.accountendpoint.editaccount.populateLimitFlag = function() {
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

fanikiwa.accountendpoint.editaccount.populateCoa = function() {
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

fanikiwa.accountendpoint.editaccount.populateAccountTypes = function() {
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

fanikiwa.accountendpoint.editaccount.populateInterestComputationMethod = function() {
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

fanikiwa.accountendpoint.editaccount.populateInterestComputationTerm = function() {
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

fanikiwa.accountendpoint.editaccount.populateInterestAccrualInterval = function() {
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

fanikiwa.accountendpoint.editaccount.populateInterestApplicationMethod = function() {
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

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Create.html" style="cursor: pointer;">Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Statement.html" style="cursor: pointer;">Statement</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
