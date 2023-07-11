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
	var _accountName = document.getElementById('txtaccountName').value;
	var _accountNo = document.getElementById('txtaccountNo').value;
	var _bookBalance = document.getElementById('txtbookBalance').value;
	var _clearedBalance = document.getElementById('txtclearedBalance').value;
	var _customer = document.getElementById('cbocustomer').value;
	var _coadet = document.getElementById('cbocoadet').value;
	var _accounttype = document.getElementById('cboaccounttype').value;
	var _limitCheckFlag = document.getElementById('chklimitCheckFlag').value;
	var _limitFlag = document.getElementById('cbolimitFlag').value;
	var _passFlag = document.getElementById('cbopassFlag').value;
	var _accruedInt = document.getElementById('txtaccruedInt').value;
	var _limit = document.getElementById('txtlimit').value;
	var _interestRate = document.getElementById('txtinterestRate').value;
	var _closed = document.getElementById('chkclosed').value;

	if (_accountName.length == 0) {
		errormsg += '<li>' + " Account Name cannot be null " + '</li>';
		error_free = false;
	}
	if (_customer.length == 0 || _customer == -1) {
		errormsg += '<li>' + " Select Customer " + '</li>';
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

	$('#apiResults').html('creating offer...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var account = {};
	account.accountName = _accountName;
	account.accountNo = _accountNo;
	account.bookBalance = _bookBalance;
	account.clearedBalance = _clearedBalance;
	account.customer = _customer;
	account.coadet = _coadet;
	account.accounttype = _accounttype;
	account.limitCheckFlag = _limitCheckFlag;
	account.limitFlag = _limitFlag;
	account.passFlag = _passFlag;
	account.accruedInt = _accruedInt;
	account.limit = _limit;
	account.interestRate = _interestRate;
	account.closed = _closed;

	gapi.client.accountendpoint
			.updateAccount(account)
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp.result.result == false) {
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
							$('#errormessage').html(
									'operation failed! Please try again.');
							$('#successmessage').html('');
							$('#apiResults').html('');
						}

					}, function(reason) {
						console.log('Error: ' + reason.result.error.message);
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
	$("#chklimitCheckFlag").attr('checked', false);
	$("#chkclosed").attr('checked', false);
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
			fanikiwa.accountendpoint.editaccount.enableButtons();
			fanikiwa.accountendpoint.editaccount.populatePassFlag();
			fanikiwa.accountendpoint.editaccount.populateLimitFlag();
			fanikiwa.accountendpoint.editaccount.populateCoa();
			fanikiwa.accountendpoint.editaccount.populateAccountTypes();
			fanikiwa.accountendpoint.editaccount.populateCustomers();
			fanikiwa.accountendpoint.editaccount.initializeControls();
		}
	}

	apisToLoad = 4; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', null, apiRoot);
	gapi.client.load('coadetendpoint', 'v1', null, apiRoot);
	gapi.client.load('accounttypeendpoint', 'v1', null, apiRoot);
	gapi.client.load('customerendpoint', 'v1', null, apiRoot);

};

fanikiwa.accountendpoint.editaccount.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editaccountid');
	gapi.client.accountendpoint.getAccount({
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

	if (account.accountName != undefined)
		document.getElementById('txtaccountName').value = account.accountName;
	else {
		document.getElementById('txtaccountName').value = "";
	}
	if (account.accountNo != undefined)
		document.getElementById('txtaccountNo').value = account.accountNo;
	else {
		document.getElementById('txtaccountNo').value = "";
	}
	if (account.bookBalance != undefined)
		document.getElementById('txtbookBalance').value = account.bookBalance;
	else {
		document.getElementById('txtbookBalance').value = "";
	}
	if (account.clearedBalance != undefined)
		document.getElementById('txtclearedBalance').value = account.clearedBalance;
	else {
		document.getElementById('txtclearedBalance').value = "";
	}
	if (account.customer != undefined)
		document.getElementById('cbocustomer').value = account.customer;
	else
		document.getElementById('cbocustomer').value = "";
	if (account.coadet != undefined)
		document.getElementById('cbocoadet').value = account.coadet;
	else
		document.getElementById('cbocoadet').value = "";
	if (account.accounttype != undefined)
		document.getElementById('cboaccounttype').value = account.accounttype;
	else
		document.getElementById('cboaccounttype').value = "";
	document.getElementById('chklimitCheckFlag').value = account.limitCheckFlag;
	if (account.limitFlag != undefined)
		document.getElementById('cbolimitFlag').value = account.limitFlag;
	else
		document.getElementById('cbolimitFlag').value = "";
	if (account.passFlag != undefined)
		document.getElementById('cbopassFlag').value = account.passFlag;
	else
		document.getElementById('cbopassFlag').value = "";
	document.getElementById('txtaccruedInt').value = account.accruedInt;
	document.getElementById('txtlimit').value = account.limit;
	document.getElementById('txtinterestRate').value = account.interestRate;
	document.getElementById('chkclosed').checked = account.closed;
};

fanikiwa.accountendpoint.editaccount.populatePassFlag = function() {
	var passflagarray = [ {
		id : "0",
		description : "Ok"
	}, {
		id : "1",
		description : "DebitPostingProhibited"
	}, {
		id : "2",
		description : "CreditPostingProhibited"
	}, {
		id : "3",
		description : "AllPostingProhibited"
	}, {
		id : "4",
		description : "Locked"
	}, {
		id : "-1",
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
		id : "0",
		description : "Ok"
	}, {
		id : "5",
		description : "PostingNoLimitChecking"
	}, {
		id : "6",
		description : "PostingOverDrawingProhibited"
	}, {
		id : "7",
		description : "PostingDrawingOnUnclearedEffectsAllowed"
	}, {
		id : "8",
		description : "LimitsAllowed"
	}, {
		id : "9",
		description : "LimitForAdvanceProhibited"
	}, {
		id : "10",
		description : "LimitForBlockingProhibited"
	}, {
		id : "11",
		description : "AllLimitsProhibited"
	}, {
		id : "-1",
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
	gapi.client.coadetendpoint.listCoadet().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.length; i++) {
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
						for (var i = 0; i < resp.length; i++) {
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

fanikiwa.accountendpoint.editaccount.populateCustomers = function() {
	var customeroptions = '';
	gapi.client.customerendpoint.listCustomer().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.length; i++) {
							customeroptions += '<option value="'
									+ resp.result.items[i].id + '">'
									+ resp.result.items[i].description
									+ '</option>';
						}
						$("#cbocustomer").append(customeroptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

function Clear() {
	$("#txtaccountName").val("");
	$("#txtaccountNo").val("");
	$("#txtbookBalance").val("");
	$("#txtclearedBalance").val("");
	$("#cbocustomer").val("-1");
	$("#cbocoadet").val("-1");
	$("#cboaccounttype").val("-1");
	$('#chklimitCheckFlag').attr('checked', false);
	$("#cbolimitFlag").val("0");
	$("#cbopassFlag").val("0");
	$("#txtaccruedInt").val("");
	$("#txtlimit").val("");
	$("#txtinterestRate").val("");
	$('#chkclosed').attr('checked', false);
}

function DisplayException(errormsg) {

	errormsg += "</ul>";

	$("#error-display-div").html(errormsg);
	$("#error-display-div").removeClass('displaynone');
	$("#error-display-div").addClass('displayblock');
	$("#error-display-div").show();
}

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}