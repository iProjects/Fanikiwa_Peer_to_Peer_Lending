/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.createaccount = fanikiwa.accountendpoint.createaccount
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.createaccount = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _accountName = document.getElementById('txtaccountName').value;
	var _accountNo = document.getElementById('txtaccountNo').value;
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

	$('#apiResults').html('creating account...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var accountDto = {};
	accountDto.accountName = _accountName;
	accountDto.accountNo = _accountNo;
	accountDto.customer = _customer;
	accountDto.coadet = _coadet;
	accountDto.accounttype = _accounttype;
	accountDto.limitCheckFlag = _limitCheckFlag;
	accountDto.limitFlag = _limitFlag;
	accountDto.passFlag = _passFlag;
	accountDto.accruedInt = _accruedInt;
	accountDto.interestRate = _interestRate;
	accountDto.closed = _closed;
	accountDto.intPayAccount = _intPayAccount;
	accountDto.interestComputationMethod = _interestComputationMethod;
	accountDto.interestComputationTerm = _interestComputationTerm;
	accountDto.interestAccrualInterval = _interestAccrualInterval;
	accountDto.interestApplicationMethod = _interestApplicationMethod;
	accountDto.interestRateSusp = _interestRateSusp;
	accountDto.accruedIntInSusp = _accruedIntInSusp;
	accountDto.maturityDate = _maturityDate;
	accountDto.lastIntAccrualDate = _lastIntAccrualDate;
	accountDto.nextIntAccrualDate = _nextIntAccrualDate;
	accountDto.lastIntAppDate = _lastIntAppDate;
	accountDto.nextIntAppDate = _nextIntAppDate;
	accountDto.accrueInSusp = _accrueInSusp;
	accountDto.maturityDate = _maturityDate;
	accountDto.lastIntAccrualDate = _lastIntAccrualDate;
	accountDto.nextIntAccrualDate = _nextIntAccrualDate;
	accountDto.branch = _branch;

	gapi.client.accountendpoint
			.createAccount(accountDto)
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
fanikiwa.accountendpoint.createaccount.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.accountendpoint.createaccount();
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
fanikiwa.accountendpoint.createaccount.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.createaccount.populatePassFlag();
			fanikiwa.accountendpoint.createaccount.populateLimitFlag();
			fanikiwa.accountendpoint.createaccount.populateCoa();
			fanikiwa.accountendpoint.createaccount.populateAccountTypes();
			fanikiwa.accountendpoint.createaccount
					.populateInterestComputationMethod();
			fanikiwa.accountendpoint.createaccount
					.populateInterestComputationTerm();
			fanikiwa.accountendpoint.createaccount
					.populateInterestAccrualInterval();
			fanikiwa.accountendpoint.createaccount
					.populateInterestApplicationMethod();
			fanikiwa.accountendpoint.createaccount.enableButtons();
		}
	}

	apisToLoad = 4; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);
	gapi.client.load('accounttypeendpoint', 'v1', callback, apiRoot);
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accountendpoint.createaccount.populatePassFlag = function() {
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

fanikiwa.accountendpoint.createaccount.populateLimitFlag = function() {
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

fanikiwa.accountendpoint.createaccount.populateCoa = function() {
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

fanikiwa.accountendpoint.createaccount.populateAccountTypes = function() {
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

fanikiwa.accountendpoint.createaccount.populateInterestComputationMethod = function() {
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

fanikiwa.accountendpoint.createaccount.populateInterestComputationTerm = function() {
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

fanikiwa.accountendpoint.createaccount.populateInterestAccrualInterval = function() {
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

fanikiwa.accountendpoint.createaccount.populateInterestApplicationMethod = function() {
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
