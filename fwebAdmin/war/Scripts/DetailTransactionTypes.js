/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.transactiontypeendpoint = fanikiwa.transactiontypeendpoint || {};
fanikiwa.transactiontypeendpoint.transactiontypedetails = fanikiwa.transactiontypeendpoint.transactiontypedetails
		|| {};

fanikiwa.transactiontypeendpoint.transactiontypedetails.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('transactiontypedetailsid');
	gapi.client.transactiontypeendpoint
			.retrieveTransactionType({
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
								fanikiwa.transactiontypeendpoint.transactiontypedetails
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

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateControls = function(
		transactiontype) {

	if (transactiontype.transactionTypeID != undefined)
		document.getElementById('txttransactionTypeID').value = transactiontype.transactionTypeID;
	if (transactiontype.absolute != undefined)
		document.getElementById('chkabsolute').checked = transactiontype.absolute;
	if (transactiontype.amountExpression != undefined)
		document.getElementById('txtamountExpression').value = transactiontype.amountExpression;
	if (transactiontype.canSuspend != undefined)
		document.getElementById('chkcanSuspend').checked = transactiontype.canSuspend;
	if (transactiontype.chargeCommissionToTransaction != undefined)
		document.getElementById('chkchargeCommission').checked = transactiontype.chargeCommission;
	if (transactiontype.chargeCommissionToTransaction != undefined)
		document.getElementById('chkchargeCommissionToTransaction').checked = transactiontype.chargeCommissionToTransaction;
	if (transactiontype.chargeWho != undefined)
		document.getElementById('cbochargeWho').value = transactiontype.chargeWho;
	if (transactiontype.commComputationMethod != undefined)
		document.getElementById('cbocommComputationMethod').value = transactiontype.commComputationMethod;
	if (transactiontype.commissionAmount != undefined)
		document.getElementById('txtcommissionAmount').value = transactiontype.commissionAmount;
	if (transactiontype.commissionAmountExpression != undefined)
		document.getElementById('txtcommissionAmountExpression').value = transactiontype.commissionAmountExpression;
	if (transactiontype.commissionContraNarrative != undefined)
		document.getElementById('txtcommissionContraNarrative').value = transactiontype.commissionContraNarrative;
	if (transactiontype.commissionCrAccount != undefined)
		document.getElementById('txtcommissionCrAccount').value = transactiontype.commissionCrAccount;
	if (transactiontype.commissionDrAccount != undefined)
		document.getElementById('txtcommissionDrAccount').value = transactiontype.commissionDrAccount;
	if (transactiontype.commissionDrAnotherAccount != undefined)
		document.getElementById('chkcommissionDrAnotherAccount').checked = transactiontype.commissionDrAnotherAccount;
	if (transactiontype.commissionMainNarrative != undefined)
		document.getElementById('txtcommissionMainNarrative').value = transactiontype.commissionMainNarrative;
	if (transactiontype.commissionNarrativeFlag != undefined)
		document.getElementById('cbocommissionNarrativeFlag').value = transactiontype.commissionNarrativeFlag;
	if (transactiontype.commissionTransactionType != undefined)
		document.getElementById('cbocommissionTransactionType').value = transactiontype.commissionTransactionType;
	if (transactiontype.crCommCalcMethod != undefined)
		document.getElementById('cbocrCommCalcMethod').value = transactiontype.crCommCalcMethod;
	if (transactiontype.debitCredit != undefined)
		document.getElementById('cbodebitCredit').value = transactiontype.debitCredit;
	if (transactiontype.defaultAmount != undefined)
		document.getElementById('txtdefaultAmount').value = transactiontype.defaultAmount;
	if (transactiontype.defaultContraAccount != undefined)
		document.getElementById('txtdefaultContraAccount').value = transactiontype.defaultContraAccount;
	if (transactiontype.defaultContraNarrative != undefined)
		document.getElementById('txtdefaultContraNarrative').value = transactiontype.defaultContraNarrative;
	if (transactiontype.defaultMainAccount != undefined)
		document.getElementById('txtdefaultMainAccount').value = transactiontype.defaultMainAccount;
	if (transactiontype.defaultMainNarrative != undefined)
		document.getElementById('txtdefaultMainNarrative').value = transactiontype.defaultMainNarrative;
	if (transactiontype.description != undefined)
		document.getElementById('txtdescription').value = transactiontype.description;
	if (transactiontype.dialogFlag != undefined)
		document.getElementById('cbodialogFlag').value = transactiontype.dialogFlag;
	if (transactiontype.drCommCalcMethod != undefined)
		document.getElementById('cbodrCommCalcMethod').value = transactiontype.drCommCalcMethod;
	if (transactiontype.forcePost != undefined)
		document.getElementById('chkforcePost').checked = transactiontype.forcePost;
	if (transactiontype.narrativeFlag != undefined)
		document.getElementById('cbonarrativeFlag').value = transactiontype.narrativeFlag;
	if (transactiontype.shortCode != undefined)
		document.getElementById('txtshortCode').value = transactiontype.shortCode;
	if (transactiontype.statFlag != undefined)
		document.getElementById('chkstatFlag').checked = transactiontype.statFlag;
	if (transactiontype.suspenseCrAccount != undefined)
		document.getElementById('txtsuspenseCrAccount').value = transactiontype.suspenseCrAccount;
	if (transactiontype.suspenseDrAccount != undefined)
		document.getElementById('txtsuspenseDrAccount').value = transactiontype.suspenseDrAccount;
	if (transactiontype.tieredTableId != undefined)
		document.getElementById('cbotieredTableId').value = transactiontype.tieredTableId;
	if (transactiontype.txnClass != undefined)
		document.getElementById('cbotxnClass').value = transactiontype.txnClass;
	if (transactiontype.txnTypeView != undefined)
		document.getElementById('cbotxnTypeView').value = transactiontype.txnTypeView;
	if (transactiontype.valueDateOffset != undefined)
		document.getElementById('txtvalueDateOffset').value = transactiontype.valueDateOffset;

};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.transactiontypeendpoint.transactiontypedetails.enableButtons = function() {

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
fanikiwa.transactiontypeendpoint.transactiontypedetails.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {

			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateDebitCredit();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateTieredTables();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateTransactionTypeViews();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateTransactionClass();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateDialogFlags();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateNarrativeFlags();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateChargeWho();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateCommissionNarrativeFlag();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateCommissionComputationMethod();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateDebitCommissionCalculationMethod();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateCreditCommissionCalculationMethod();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.populateCommissionTransactionTypes();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.enableButtons();
			fanikiwa.transactiontypeendpoint.transactiontypedetails
					.initializeControls();
		}
	}

	apisToLoad = 3; // must match number of calls to gapi.client.load()
	gapi.client.load('transactiontypeendpoint', 'v1', callback, apiRoot);
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('tieredtableendpoint', 'v1', callback, apiRoot);

};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateDebitCredit = function() {
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

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateTieredTables = function() {
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

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateTransactionTypeViews = function() {
	var txnTypeViewarray = [ {
		id : "0",
		description : "Single Entry View"
	}, {
		id : "1",
		description : "Double Entry View"
	}, {
		id : "2",
		description : "Multi Entry View"
	} ];
	var txnTypeViewoptions = '';
	for (var i = 0; i < txnTypeViewarray.length; i++) {
		txnTypeViewoptions += '<option value="' + txnTypeViewarray[i].id + '">'
				+ txnTypeViewarray[i].description + '</option>';
	}
	$("#cbotxnTypeView").append(txnTypeViewoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateTransactionClass = function() {
	var txnClassarray = [];
	var txnClassoptions = '';
	for (var i = 0; i < txnClassarray.length; i++) {
		txnClassoptions += '<option value="' + txnClassarray[i].id + '">'
				+ txnClassarray[i].description + '</option>';
	}
	$("#cbotxnClass").append(txnClassoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateDialogFlags = function() {
	var dialogFlagarray = [
			{
				id : "0",
				description : "Transaction can be used both in background(System) or Dialog(user)"
			}, {
				id : "1",
				description : "Dialog only"
			}, {
				id : "2",
				description : "System only"
			} ];
	var dialogFlagoptions = '';
	for (var i = 0; i < dialogFlagarray.length; i++) {
		dialogFlagoptions += '<option value="' + dialogFlagarray[i].id + '">'
				+ dialogFlagarray[i].description + '</option>';
	}
	$("#cbodialogFlag").append(dialogFlagoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateNarrativeFlags = function() {
	var narrativeFlagarray = [];
	var narrativeFlagoptions = '';
	for (var i = 0; i < narrativeFlagarray.length; i++) {
		narrativeFlagoptions += '<option value="' + narrativeFlagarray[i].id
				+ '">' + narrativeFlagarray[i].description + '</option>';
	}
	$("#cbonarrativeFlag").append(narrativeFlagoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateChargeWho = function() {
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

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateCommissionNarrativeFlag = function() {
	var commissionNarrativeFlagarray = [];
	var commissionNarrativeFlagoptions = '';
	for (var i = 0; i < commissionNarrativeFlagarray.length; i++) {
		commissionNarrativeFlagoptions += '<option value="'
				+ commissionNarrativeFlagarray[i].id + '">'
				+ commissionNarrativeFlagarray[i].description + '</option>';
	}
	$("#cbocommissionNarrativeFlag").append(commissionNarrativeFlagoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateCommissionComputationMethod = function() {
	var commissionComputationMethodarray = [ {
		id : "L",
		description : "Lookup commission from a table"
	}, {
		id : "T",
		description : "Compute tiered value from a Tieredtable"
	}, {
		id : "F",
		description : "Flate rate"
	} ];
	var commissionComputationMethodoptions = '';
	for (var i = 0; i < commissionComputationMethodarray.length; i++) {
		commissionComputationMethodoptions += '<option value="'
				+ commissionComputationMethodarray[i].id + '">'
				+ commissionComputationMethodarray[i].description + '</option>';
	}
	$("#cbocommComputationMethod").append(commissionComputationMethodoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateDebitCommissionCalculationMethod = function() {
	var commissionCalculationMethodarray = [];
	var commissionCalculationMethodoptions = '';
	for (var i = 0; i < commissionCalculationMethodarray.length; i++) {
		commissionCalculationMethodoptions += '<option value="'
				+ commissionCalculationMethodarray[i].id + '">'
				+ commissionCalculationMethodarray[i].description + '</option>';
	}
	$("#cbodrCommCalcMethod").append(commissionCalculationMethodoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateCreditCommissionCalculationMethod = function() {
	var commissionCalculationMethodarray = [];
	var commissionCalculationMethodoptions = '';
	for (var i = 0; i < commissionCalculationMethodarray.length; i++) {
		commissionCalculationMethodoptions += '<option value="'
				+ commissionCalculationMethodarray[i].id + '">'
				+ commissionCalculationMethodarray[i].description + '</option>';
	}
	$("#cbocrCommCalcMethod").append(commissionCalculationMethodoptions);
};

fanikiwa.transactiontypeendpoint.transactiontypedetails.populateCommissionTransactionTypes = function() {
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

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TransactionType/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
