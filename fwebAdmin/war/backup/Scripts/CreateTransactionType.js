/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.transactiontypeendpoint = fanikiwa.transactiontypeendpoint || {};
fanikiwa.transactiontypeendpoint.createtransactiontype = fanikiwa.transactiontypeendpoint.createtransactiontype
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.transactiontypeendpoint.createtransactiontype = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries 
	var _absolute = document.getElementById('chkabsolute').checked;
	var _amountExpression = document.getElementById('txtamountExpression').value;
	var _canSuspend = document.getElementById('chkcanSuspend').checked;
	var _chargeCommission = document.getElementById('chkchargeCommission').checked;
	var _chargeCommissionToTransaction = document
			.getElementById('chkchargeCommissionToTransaction').checked;
	var _chargeWho = document.getElementById('cbochargeWho').value;
	var _commComputationMethod = document
			.getElementById('cbocommComputationMethod').value;
	var _commissionAmount = document.getElementById('txtcommissionAmount').value;
	var _commissionAmountExpression = document
			.getElementById('txtcommissionAmountExpression').value;
	var _commissionContraNarrative = document
			.getElementById('txtcommissionContraNarrative').value;
	var _commissionCrAccount = document
			.getElementById('cbocommissionCrAccount').value;
	var _commissionDrAccount = document
			.getElementById('cbocommissionDrAccount').value;
	var _commissionDrAnotherAccount = document
			.getElementById('chkcommissionDrAnotherAccount').checked;
	var _commissionMainNarrative = document
			.getElementById('txtcommissionMainNarrative').value;
	var _commissionNarrativeFlag = document
			.getElementById('cbocommissionNarrativeFlag').value;
	var _commissionTransactionType = document
			.getElementById('cbocommissionTransactionType').value;
	var _crCommCalcMethod = document.getElementById('cbocrCommCalcMethod').value;
	var _debitCredit = document.getElementById('cbodebitCredit').value;
	var _defaultAmount = document.getElementById('txtdefaultAmount').value;
	var _defaultContraAccount = document
			.getElementById('cbodefaultContraAccount').value;
	var _defaultContraNarrative = document
			.getElementById('txtdefaultContraNarrative').value;
	var _defaultMainAccount = document.getElementById('cbodefaultMainAccount').value;
	var _defaultMainNarrative = document
			.getElementById('txtdefaultMainNarrative').value;
	var _description = document.getElementById('txtdescription').value;
	var _dialogFlag = document.getElementById('cbodialogFlag').value;
	var _drCommCalcMethod = document.getElementById('cbodrCommCalcMethod').value;
	var _forcePost = document.getElementById('chkforcePost').checked;
	var _narrativeFlag = document.getElementById('cbonarrativeFlag').value;
	var _shortCode = document.getElementById('txtshortCode').value;
	var _statFlag = document.getElementById('cbostatFlag').value;
	var _suspenseCrAccount = document.getElementById('cbosuspenseCrAccount').value;
	var _suspenseDrAccount = document.getElementById('cbosuspenseDrAccount').value;
	var _tieredTableId = document.getElementById('cbotieredTableId').value;
	var _txnClass = document.getElementById('cbotxnClass').value;
	var _txnTypeView = document.getElementById('cbotxnTypeView').value;
	var _valueDateOffset = document.getElementById('txtvalueDateOffset').value;

	if (_shortCode.length == 0) {
		errormsg += '<li>' + " Short Code cannot be null " + '</li>';
		error_free = false;
	}
	if (_description.length == 0) {
		errormsg += '<li>' + " Description cannot be null " + '</li>';
		error_free = false;
	}
	if (_debitCredit.length == 0 || _debitCredit == -1) {
		errormsg += '<li>' + " Select DebitCredit " + '</li>';
		error_free = false;
	}
	if (_tieredTableId.length == 0 || _tieredTableId == -1) {
		errormsg += '<li>' + " Select Tiered Table " + '</li>';
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

	$('#apiResults').html('creating transactiontype...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var transactiontype = {}; 
	transactiontype.absolute = _absolute;
	transactiontype.amountExpression = _amountExpression;
	transactiontype.canSuspend = _canSuspend;
	transactiontype.chargeCommission = _chargeCommission;
	transactiontype.chargeCommissionToTransaction = _chargeCommissionToTransaction;
	transactiontype.chargeWho = _chargeWho;
	transactiontype.commComputationMethod = _commComputationMethod;
	transactiontype.commissionAmount = _commissionAmount;
	transactiontype.commissionAmountExpression = _commissionAmountExpression;
	transactiontype.commissionContraNarrative = _commissionContraNarrative;
	transactiontype.commissionCrAccount = _commissionCrAccount;
	transactiontype.commissionDrAccount = _commissionDrAccount;
	transactiontype.commissionDrAnotherAccount = _commissionDrAnotherAccount;
	transactiontype.commissionMainNarrative = _commissionMainNarrative;
	transactiontype.commissionNarrativeFlag = _commissionNarrativeFlag;
	transactiontype.commissionTransactionType = _commissionTransactionType;
	transactiontype.crCommCalcMethod = _crCommCalcMethod;
	transactiontype.debitCredit = _debitCredit;
	transactiontype.defaultAmount = _defaultAmount;
	transactiontype.defaultContraAccount = _defaultContraAccount;
	transactiontype.defaultContraNarrative = _defaultContraNarrative;
	transactiontype.defaultMainAccount = _defaultMainAccount;
	transactiontype.defaultMainNarrative = _defaultMainNarrative;
	transactiontype.description = _description;
	transactiontype.dialogFlag = _dialogFlag;
	transactiontype.drCommCalcMethod = _drCommCalcMethod;
	transactiontype.forcePost = _forcePost;
	transactiontype.narrativeFlag = _narrativeFlag;
	transactiontype.shortCode = _shortCode;
	transactiontype.statFlag = _statFlag;
	transactiontype.suspenseCrAccount = _suspenseCrAccount;
	transactiontype.suspenseDrAccount = _suspenseDrAccount;
	transactiontype.tieredTableId = _tieredTableId;
	transactiontype.txnClass = _txnClass;
	transactiontype.txnTypeView = _txnTypeView;
	transactiontype.valueDateOffset = _valueDateOffset;

	gapi.client.transactiontypeendpoint
			.insertTransactionType(transactiontype)
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
												'window.location.href = "/Views/TransactionType/List.html";',
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
fanikiwa.transactiontypeendpoint.createtransactiontype.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.transactiontypeendpoint.createtransactiontype();
	});

	document.getElementById('txtamountExpression').value = 0;
	document.getElementById('txtvalueDateOffset').value = 0;
	document.getElementById('txtcommissionAmountExpression').value = 0;
	document.getElementById('txtcommissionAmount').value = 0;
	document.getElementById('txtdefaultAmount').value = 0;

};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.transactiontypeendpoint.createtransactiontype.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {

			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateDebitCredit();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateTieredTables();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateTransactionTypeViews();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateTransactionClass();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateDialogFlags();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateNarrativeFlags();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateChargeWho();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateStatementFlags();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateScreen();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateSuspenseCrAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateSuspenseDrAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCommissionNarrativeFlag();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCommissionComputationMethod();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateDebitCommissionCalculationMethod();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCreditCommissionCalculationMethod();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCommissionDebitAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCommissionCreditAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateCommissionTransactionTypes();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateDefaultMainAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.populateDefaultContraAccounts();
			fanikiwa.transactiontypeendpoint.createtransactiontype
					.enableButtons();
		}
	}

	apisToLoad = 3; // must match number of calls to gapi.client.load()
	gapi.client.load('transactiontypeendpoint', 'v1', callback, apiRoot);
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('tieredtableendpoint', 'v1', callback, apiRoot);

};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateDebitCredit = function() {
	var debitCreditarray = [ {
		id : "D",
		description : "Debit"
	}, {
		id : "C",
		description : "Credit"
	} ];
	var debitCreditoptions = '';
	for (var i = 0; i < debitCreditarray.length; i++) {
		debitCreditoptions += '<option value="' + debitCreditarray[i].id + '">'
				+ debitCreditarray[i].description + '</option>';
	}
	$("#cbodebitCredit").append(debitCreditoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateTieredTables = function() {
	var tieredTableoptions = '';
	gapi.client.tieredtableendpoint.listTieredtable().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							tieredTableoptions += '<option value="'
									+ resp.result.items[i].id + '">'
									+ resp.result.items[i].description
									+ '</option>';
						}
						$("#cbotieredTableId").append(tieredTableoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateTransactionTypeViews = function() {
	var txnTypeViewarray = [ {
		id : "1",
		description : "Single Post"
	}, {
		id : "2",
		description : "Double Post"
	}, {
		id : "3",
		description : "Multiple Post"
	} ];
	var txnTypeViewoptions = '';
	for (var i = 0; i < txnTypeViewarray.length; i++) {
		txnTypeViewoptions += '<option value="' + txnTypeViewarray[i].id + '">'
				+ txnTypeViewarray[i].description + '</option>';
	}
	$("#cbotxnTypeView").append(txnTypeViewoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateTransactionClass = function() {
	var txnClassarray = [ {
		id : "1",
		description : "class 1"
	}, {
		id : "2",
		description : "class 2"
	}, {
		id : "3",
		description : "class 3"
	} ];
	var txnClassoptions = '';
	for (var i = 0; i < txnClassarray.length; i++) {
		txnClassoptions += '<option value="' + txnClassarray[i].id + '">'
				+ txnClassarray[i].description + '</option>';
	}
	$("#cbotxnClass").append(txnClassoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateDialogFlags = function() {
	var dialogFlagarray = [ {
		id : "1",
		description : "dialog flag 1"
	}, {
		id : "2",
		description : "dialog flag 2"
	}, {
		id : "3",
		description : "dialog flag 3"
	} ];
	var dialogFlagoptions = '';
	for (var i = 0; i < dialogFlagarray.length; i++) {
		dialogFlagoptions += '<option value="' + dialogFlagarray[i].id + '">'
				+ dialogFlagarray[i].description + '</option>';
	}
	$("#cbodialogFlag").append(dialogFlagoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateNarrativeFlags = function() {
	var narrativeFlagarray = [ {
		id : "1",
		description : "narrative flag 1"
	}, {
		id : "2",
		description : "narrative flag 2"
	}, {
		id : "3",
		description : "narrative flag 3"
	} ];
	var narrativeFlagoptions = '';
	for (var i = 0; i < narrativeFlagarray.length; i++) {
		narrativeFlagoptions += '<option value="' + narrativeFlagarray[i].id
				+ '">' + narrativeFlagarray[i].description + '</option>';
	}
	$("#cbonarrativeFlag").append(narrativeFlagoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateChargeWho = function() {
	var chargeWhoarray = [ {
		id : "D",
		description : "Debit"
	}, {
		id : "C",
		description : "Credit"
	} ];
	var chargeWhooptions = '';
	for (var i = 0; i < chargeWhoarray.length; i++) {
		chargeWhooptions += '<option value="' + chargeWhoarray[i].id + '">'
				+ chargeWhoarray[i].description + '</option>';
	}
	$("#cbochargeWho").append(chargeWhooptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateStatementFlags = function() {
	var statFlagarray = [ {
		id : "1",
		description : "Statement Flag 1"
	}, {
		id : "2",
		description : "Statement Flag 2"
	} ];
	var statFlagoptions = '';
	for (var i = 0; i < statFlagarray.length; i++) {
		statFlagoptions += '<option value="' + statFlagarray[i].id + '">'
				+ statFlagarray[i].description + '</option>';
	}
	$("#cbostatFlag").append(statFlagoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateScreen = function() {
	var Screenarray = [ {
		id : "1",
		description : "Single Entry"
	}, {
		id : "2",
		description : "Double Entry"
	}, {
		id : "3",
		description : "Multiple Entry"
	} ];
	var Screenoptions = '';
	for (var i = 0; i < Screenarray.length; i++) {
		Screenoptions += '<option value="' + Screenarray[i].id + '">'
				+ Screenarray[i].description + '</option>';
	}
	$("#cboScreen").append(Screenoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateSuspenseCrAccounts = function() {
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
						$("#cbosuspenseCrAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateSuspenseDrAccounts = function() {
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
						$("#cbosuspenseDrAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCommissionNarrativeFlag = function() {
	var commissionNarrativeFlagarray = [ {
		id : "1",
		description : "Commission Narrative Flag 1"
	}, {
		id : "2",
		description : "Commission Narrative Flag 2"
	}, {
		id : "3",
		description : "Commission Narrative Flag 3"
	} ];
	var commissionNarrativeFlagoptions = '';
	for (var i = 0; i < commissionNarrativeFlagarray.length; i++) {
		commissionNarrativeFlagoptions += '<option value="'
				+ commissionNarrativeFlagarray[i].id + '">'
				+ commissionNarrativeFlagarray[i].description + '</option>';
	}
	$("#cbocommissionNarrativeFlag").append(commissionNarrativeFlagoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCommissionComputationMethod = function() {
	var commissionComputationMethodarray = [ {
		id : "1",
		description : "Commission Computation method 1"
	}, {
		id : "2",
		description : "Commission Computation method 2"
	}, {
		id : "3",
		description : "Commission Computation method 3"
	} ];
	var commissionComputationMethodoptions = '';
	for (var i = 0; i < commissionComputationMethodarray.length; i++) {
		commissionComputationMethodoptions += '<option value="'
				+ commissionComputationMethodarray[i].id + '">'
				+ commissionComputationMethodarray[i].description + '</option>';
	}
	$("#cbocommComputationMethod").append(commissionComputationMethodoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateDebitCommissionCalculationMethod = function() {
	var commissionCalculationMethodarray = [ {
		id : "1",
		description : "Commission Computation method 1"
	}, {
		id : "2",
		description : "Commission Computation method 2"
	}, {
		id : "3",
		description : "Commission Computation method 3"
	} ];
	var commissionCalculationMethodoptions = '';
	for (var i = 0; i < commissionCalculationMethodarray.length; i++) {
		commissionCalculationMethodoptions += '<option value="'
				+ commissionCalculationMethodarray[i].id + '">'
				+ commissionCalculationMethodarray[i].description + '</option>';
	}
	$("#cbodrCommCalcMethod").append(commissionCalculationMethodoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCreditCommissionCalculationMethod = function() {
	var commissionCalculationMethodarray = [ {
		id : "1",
		description : "Commission Computation method 1"
	}, {
		id : "2",
		description : "Commission Computation method 2"
	}, {
		id : "3",
		description : "Commission Computation method 3"
	} ];
	var commissionCalculationMethodoptions = '';
	for (var i = 0; i < commissionCalculationMethodarray.length; i++) {
		commissionCalculationMethodoptions += '<option value="'
				+ commissionCalculationMethodarray[i].id + '">'
				+ commissionCalculationMethodarray[i].description + '</option>';
	}
	$("#cbocrCommCalcMethod").append(commissionCalculationMethodoptions);
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCommissionDebitAccounts = function() {
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
						$("#cbocommissionDrAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCommissionCreditAccounts = function() {
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
						$("#cbocommissionCrAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateCommissionTransactionTypes = function() {
	var transactiontypeoptions = '';
	gapi.client.transactiontypeendpoint.listTransactionType().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							transactiontypeoptions += '<option value="'
									+ resp.result.items[i].transactionTypeID
									+ '">' + resp.result.items[i].description
									+ '</option>';
						}
						$("#cbocommissionTransactionType").append(
								transactiontypeoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateDefaultMainAccounts = function() {
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
						$("#cbodefaultMainAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

fanikiwa.transactiontypeendpoint.createtransactiontype.populateDefaultContraAccounts = function() {
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
						$("#cbodefaultContraAccount").append(accountoptions);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}