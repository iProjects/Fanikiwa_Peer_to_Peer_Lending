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

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.ministatement.LoadMiniStatement = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var accountID = document.getElementById('txtaccountID').value;

	if (accountID.length == 0) {
		errormsg += '<li>' + " Account ID cannot be null. " + '</li>';
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

	$('#listAccountsResult').html('loading...');
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.accountendpoint.miniStatement({
		'accountID' : accountID
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listAccountsResult').html(
								'You have no Transactions...');
					} else {
						buildTable(resp);
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

fanikiwa.accountendpoint.ministatement.enableButtons = function() {
	$("#btnLoad").removeAttr('style');
	$("#btnLoad").removeAttr('disabled');
	$("#btnLoad").val('Load Statement');
	var btnLoad = document.querySelector('#btnLoad');
	btnLoad.addEventListener('click', function() {
		fanikiwa.accountendpoint.ministatement.LoadMiniStatement();
	});
	document.getElementById('txtaccountID').value = sessionStorage
			.getItem('ministatementaccountid');
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
			fanikiwa.accountendpoint.ministatement.enableButtons();
			fanikiwa.accountendpoint.ministatement.LoadMiniStatement();
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

	$('#listAccountsTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateAccounts(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").html(
				" Mini Statement Details.<br/>Transactions ["
						+ resp.result.items.length + "] ");

		accountsTable += '<table id="listAccountsTable">';
		accountsTable += "<thead>";
		accountsTable += "<tr>";
		accountsTable += "<th>Post Date</th>";
		accountsTable += "<th>Narrative</th>";
		accountsTable += "<th>Debit</th>";
		accountsTable += "<th>Credit</th>";
		accountsTable += "<th>Balance</th>";
		accountsTable += "<th></th>";
		accountsTable += "</tr>";
		accountsTable += "</thead>";
		accountsTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			accountsTable += '<tr>';
			accountsTable += '<td>' + formatDate(resp.result.items[i].postDate)
					+ '</td>';
			accountsTable += '<td>' + resp.result.items[i].narrative + '</td>';
			accountsTable += '<td>' + resp.result.items[i].debit.formatMoney(2)
					+ '</td>';
			accountsTable += '<td>'
					+ resp.result.items[i].credit.formatMoney(2) + '</td>';
			accountsTable += '<td>'
					+ resp.result.items[i].balance.formatMoney(2) + '</td>';
			accountsTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].transactionID
					+ ')">Details</a> </td>';
			accountsTable += "</tr>";
		}

		accountsTable += "</tbody>";
		accountsTable += "</table>";

	}
}

function Details(id) {
	sessionStorage.transactiondetailsid = id;
	window.location.href = "/Views/Account/TransactionDetails.html";
}

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
