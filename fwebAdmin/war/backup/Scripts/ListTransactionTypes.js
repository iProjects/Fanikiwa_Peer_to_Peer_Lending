/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.transactiontypeendpoint = fanikiwa.transactiontypeendpoint || {};
fanikiwa.transactiontypeendpoint.listtransactiontypes = fanikiwa.transactiontypeendpoint.listtransactiontypes
		|| {};

fanikiwa.transactiontypeendpoint.listtransactiontypes.LoadTransactionTypes = function() {

	$('#listTransactionTypesResult').html('loading...');

	gapi.client.transactiontypeendpoint.listTransactionType().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listTransactionTypesResult').html(
								'There are no Transaction Types...');
					} else {
						buildTable(resp);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.transactiontypeendpoint.listtransactiontypes.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.transactiontypeendpoint.listtransactiontypes
					.LoadTransactionTypes();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('transactiontypeendpoint', 'v1', callback, apiRoot);

};

var transactiontypeTable = '';
function buildTable(response) {

	transactiontypeTable = '';

	populateTransactionTypes(response);

	$("#listTransactionTypesResult").html(transactiontypeTable);
}

function populateTransactionTypes(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		transactiontypeTable += '<table id="listTransactionTypesTable">';
		transactiontypeTable += "<thead>";
		transactiontypeTable += "<tr>";
		transactiontypeTable += "<th>Id</th>";
		transactiontypeTable += "<th>Short Code</th>";
		transactiontypeTable += "<th>Description</th>";
		transactiontypeTable += "<th>Debit Credit</th>";
		transactiontypeTable += "<th>Charge Who</th>";
		transactiontypeTable += "<th>Charge Commission</th>";
		transactiontypeTable += "<th>Commission Amount</th>";
		transactiontypeTable += "<th>Absolute</th>";
		transactiontypeTable += "<th></th>";
		transactiontypeTable += "<th></th>";
		transactiontypeTable += "<th></th>";
		transactiontypeTable += "</tr>";
		transactiontypeTable += "</thead>";
		transactiontypeTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			transactiontypeTable += '<tr>';
			transactiontypeTable += '<td>'
					+ resp.result.items[i].transactionTypeID + '</td>';
			transactiontypeTable += '<td>' + resp.result.items[i].shortCode
					+ '</td>';
			transactiontypeTable += '<td>' + resp.result.items[i].description
					+ '</td>';
			transactiontypeTable += '<td>' + resp.result.items[i].debitCredit
					+ '</td>';
			transactiontypeTable += '<td>' + resp.result.items[i].chargeWho
					+ '</td>';
			transactiontypeTable += '<td>'
					+ resp.result.items[i].chargeCommission + '</td>';
			transactiontypeTable += '<td>'
					+ resp.result.items[i].commissionAmount.formatMoney(2)
					+ '</td>';
			transactiontypeTable += '<td>' + resp.result.items[i].absolute
					+ '</td>';
			transactiontypeTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].transactionTypeID
					+ ')">Edit</a> </td>';
			transactiontypeTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].transactionTypeID
					+ ')">Details</a> </td>';
			transactiontypeTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].transactionTypeID
					+ ')">Delete</a> </td>';
			transactiontypeTable += "</tr>";
		}

		transactiontypeTable += "</tbody>";
		transactiontypeTable += "</table>";

	}
}

function Edit(id) {
	sessionStorage.edittransactiontypeid = id;
	window.location.href = "/Views/TransactionType/Edit.html";
}

function Details(id) {
	sessionStorage.transactiontypedetailsid = id;
	window.location.href = "/Views/TransactionType/Details.html";
}

function Delete(id) {

	$('#apiResults').html('processing...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.transactiontypeendpoint
			.removeTransactionType({
				'id' : id
			})
			.execute(
					function(resp) {
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
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message.toString());
							$('#successmessage').html('');
							$('#apiResults').html('');
						}
					});
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TransactionType/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/List.html" style="cursor: pointer;" >Tiered Tables</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
