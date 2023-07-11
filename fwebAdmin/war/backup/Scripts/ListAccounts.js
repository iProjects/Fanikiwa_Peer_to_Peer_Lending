/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.listaccounts = fanikiwa.accountendpoint.listaccounts
		|| {};

fanikiwa.accountendpoint.listaccounts.LoadAccounts = function() {

	$('#listAccountsResult').html('loading...');

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.accountendpoint.listAccount().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			resp.items = resp.items || [];
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listAccountsResult').html('There are no Accounts...');
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
fanikiwa.accountendpoint.listaccounts.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.listaccounts.LoadAccounts();
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
		accountsTable += "<th>Account ID</th>";
		accountsTable += "<th>Account Name</th>";
		accountsTable += "<th>Book Balance</th>";
		accountsTable += "<th>Cleared Balance</th>";
		accountsTable += "<th>limit</th>";
		accountsTable += "<th>Available Balance</th>";
		accountsTable += "<th></th>";
		accountsTable += "<th></th>";
		accountsTable += "<th></th>";
		accountsTable += "<th></th>";
		accountsTable += "</tr>";
		accountsTable += "</thead>";
		accountsTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			var availbal = resp.result.items[i].clearedBalance
					- resp.result.items[i].limit;
			accountsTable += '<tr>';
			accountsTable += '<td>' + resp.result.items[i].accountID + '</td>';
			accountsTable += '<td>' + resp.result.items[i].accountName
					+ '</td>';
			accountsTable += '<td style="text-align:right">'
					+ resp.result.items[i].bookBalance.formatMoney(2) + '</td>';
			accountsTable += '<td style="text-align:right">'
					+ resp.result.items[i].clearedBalance.formatMoney(2)
					+ '</td>';
			accountsTable += '<td style="text-align:right">'
					+ resp.result.items[i].limit.formatMoney(2) + '</td>';
			accountsTable += '<td style="text-align:right">'
					+ availbal.formatMoney(2) + '</td>';

			accountsTable += '<td><a href="#" onclick="MiniStatement('
					+ resp.result.items[i].accountID
					+ ')">Mini Statement</a> </td>';
			accountsTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].accountID + ')">Edit</a> </td>';
			accountsTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].accountID + ')">Details</a> </td>';
			accountsTable += '<td><a href="#" onclick="Close('
					+ resp.result.items[i].accountID + ')">Close</a> </td>';

			accountsTable += "</tr>";
		}

		accountsTable += "</tbody>";
		accountsTable += "</table>";

	}
}

function MiniStatement(id) {
	sessionStorage.ministatementaccountid = id;
	window.location.href = "/Views/Account/MiniStatement.html";
}

function Edit(id) {
	sessionStorage.editaccountid = id;
	window.location.href = "/Views/Account/Edit.html";
}

function Details(id) {
	sessionStorage.accountdetailsid = id;
	window.location.href = "/Views/Account/Details.html";
}

function Close(id) {

	$('#apiResults').html('processing...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.accountendpoint
			.closeAccount({
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
												'window.location.href = "/Views/Account/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Create.html" style="cursor: pointer;">Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Statement.html" style="cursor: pointer;">Statement</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/WithDraw/Withdraw.html" style="cursor: pointer;">Withdraw</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Deposit/Deposit.html" style="cursor: pointer;">Deposit</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Coa/List.html" style="cursor: pointer;" >Coa</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/AccountType/List.html" style="cursor: pointer;" >Account Types</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
