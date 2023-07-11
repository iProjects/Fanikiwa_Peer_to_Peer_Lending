/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.statement = fanikiwa.accountendpoint.statement || {};

fanikiwa.accountendpoint.statement.enableButtons = function() {
	$("#btnLoad").removeAttr('style');
	$("#btnLoad").removeAttr('disabled');
	$("#btnLoad").val('Load Statement');
	var btnLoad = document.querySelector('#btnLoad');
	btnLoad.addEventListener('click', function() {
		fanikiwa.accountendpoint.statement.LoadStatement();
	});
	document.getElementById('dtpstartdate').value = decrementDateByMonth(
			new Date(), -3, 'm');
	document.getElementById('dtpenddate').value = offsetDate(new Date(), 1, 'd');
};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.statement.LoadStatement = function() {

	errormsg = '';
	$('#errorList').remove();
	$('#error-display-div').empty();
	errormsg += '<ul id="errorList">';
	var error_free = true;

	var accountID = document.getElementById('cboAccount').value;
	var sdate = document.getElementById('dtpstartdate').value;
	var edate = document.getElementById('dtpenddate').value;

	if (accountID.length == 0) {
		errormsg += '<li>' + " Account ID cannot be null" + '</li>';
		error_free = false;
	}
	if (sdate.length == 0) {
		errormsg += '<li>' + " Select start Date " + '</li>';
		error_free = false;
	}
	if (edate.length == 0) {
		errormsg += '<li>' + " Select End Date " + '</li>';
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
		$('#errorList').remove();
		$('#error-display-div').empty();
	}

	$('#apiResults').html('loading...');
	$('#successmessage').html('');
	$('#apiResults').html('');
	$('#errormessage').html('');

	gapi.client.accountendpoint.statement({
		'accountID' : accountID,
		'sdate' : sdate,
		'edate' : edate
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {

					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listAccountsResult').html(
								'There are no Transactions...');
						$('#apiResults').html('');
					} else {
						$('#apiResults').html('');
						$('#listAccountsResult').html('loading...');
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

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.accountendpoint.statement.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.statement.LoadAccounts();
			fanikiwa.accountendpoint.statement.enableButtons();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

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
				" Full Statement Details.<br/>Transactions ["
						+ resp.result.items.length + "] ");

		accountsTable += '<table id="listAccountsTable">';
		accountsTable += "<thead>";
		accountsTable += "<tr>";
		accountsTable += "<th>Post Date</th>";
		accountsTable += "<th>Narrative</th>";
		accountsTable += "<th>Debit</th>";
		accountsTable += "<th>Credit</th>";
		accountsTable += "<th>Book Balance</th>";
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
			accountsTable += "</tr>";
		}

		accountsTable += "</tbody>";
		accountsTable += "</table>";

	}
}

fanikiwa.accountendpoint.statement.LoadAccounts = function() {
	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;
	var accountsOptions = '';
	gapi.client.memberendpoint
			.listMemberAccountWeb({
				'email' : email
			})
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp.result.items == undefined
									|| resp.result.items == null) {
								accountsOptions += '<option value="">Select Account</option>';
								$('#cboAccount').append(accountsOptions);
							} else {
								for (var i = 0; i < resp.result.items.length; i++) {
									accountsOptions += '<option value="'
											+ resp.result.items[i].accountID
											+ '">'
											+ resp.result.items[i].accountName
											+ '</option>';
									console.log('<option value="'
											+ resp.result.items[i].accountID
											+ '">'
											+ resp.result.items[i].accountName
											+ '</option>');
								}
								accountsOptions += '<option value="">Select Account</option>';
								$('#cboAccount').append(accountsOptions);
							}
						}

					}, function(reason) {
						console.log('Error: ' + reason.result.error.message);
					});
};

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Statement.html" style="cursor: pointer;">Statement</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Withdraw.html" style="cursor: pointer;">Withdraw</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Loans/ListMyLoans.html" style="cursor: pointer;">My loans</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Loans/MyInvestMentsList.html" style="cursor: pointer;">My investments</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
