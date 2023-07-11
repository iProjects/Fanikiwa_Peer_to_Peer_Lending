/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.ministatement = fanikiwa.accountendpoint.ministatement
		|| {};

fanikiwa.accountendpoint.ministatement.LoadStatement = function() {

	$('#listAccountsResult').html('loading...');

	var accountID = sessionStorage.getItem('ministatementaccountid');

	gapi.client.accountendpoint.miniStatement({
		'accountID' : accountID
	}).execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listAccountsResult').html('You have no Transactions...');
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
fanikiwa.accountendpoint.ministatement.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.ministatement.LoadStatement();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);

};

var accountsTable = '';
function buildTable(response) {

	accountsTable = '';

	populateAccounts(response);

	$("#listAccountsResult").html(accountsTable);
}

function populateAccounts(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		accountsTable += '<table id="listAccountsTable">';
		accountsTable += "<thead>";
		accountsTable += "<tr>";
		accountsTable += "<th>Post Date</th>";
		accountsTable += "<th>Narrative</th>";
		accountsTable += "<th>Debit</th>";
		accountsTable += "<th>Credit</th>";
		accountsTable += "<th>Balance</th>";
		accountsTable += "</tr>";
		accountsTable += "</thead>";
		accountsTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			accountsTable += '<tr>';
			accountsTable += '<td>'
					+ formatDate(resp.result.items[i].postDate)
					+ '</td>';
			accountsTable += '<td>' + resp.result.items[i].narrative + '</td>';
			accountsTable += '<td>' + resp.result.items[i].debit.formatMoney(2)
					+ '</td>';
			accountsTable += '<td>'
					+ resp.result.items[i].credit.formatMoney(2) + '</td>';
			accountsTable += '<td>'
					+ resp.result.items[i].balance.formatMoney(2) + '</td>';
			accountsTable += "</tr>";
		}

		accountsTable += "</tbody>";
		accountsTable += "</table>";

	}
}
