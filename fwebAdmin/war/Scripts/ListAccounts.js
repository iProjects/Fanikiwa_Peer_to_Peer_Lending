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

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.listaccounts.LoadAccounts = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');
	$('#listAccountsResult').html('');

	var rdoaccountID = document.querySelector('#rdoaccountID');
	var rdomemberID = document.querySelector('#rdomemberID');

	if (!rdomemberID.checked && !rdoaccountID.checked) {
		errormsg += '<li>'
				+ " You must select whether to search by Account ID or Member ID "
				+ '</li>';
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

	if (rdoaccountID.checked) {

		var accountID = document.getElementById('txtaccountID').value;
		if (accountID.length == 0) {
			errormsg += '<li>' + " Account ID cannot be null " + '</li>';
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

		gapi.client.accountendpoint
				.searchAccountByID({
					'id' : accountID
				})
				.execute(
						function(resp) {
							console.log('response =>> ' + resp);
							if (!resp.code) {
								if (resp.result.success == false) {
									$('#apiResults').html('');
									$('#successmessage').html('');
									$('#listAccountsResult').html('');
									$('#errormessage').html(
											'operation failed! Error...<br/>'
													+ resp.result.resultMessage
															.toString());
								} else {
									resp.result.clientToken.items = resp.result.clientToken.items
											|| [];
									if (resp.result.clientToken.items == undefined
											|| resp.result.clientToken.items == null) {
										$('#apiResults').html('');
										$('#successmessage').html('');
										$('#errormessage').html('');
										$('#listAccountsResult').html(
												'There are no Accounts...');
									} else {
										$('#apiResults').html('');
										$('#successmessage').html('');
										$('#errormessage').html('');
										$('#listAccountsResult').html(
												'loading...');
										buildTable(resp.result.clientToken);
									}
								}
							} else {
								console.log('Error: ' + resp.error.message);
								$('#apiResults').html('');
								$('#successmessage').html('');
								$('#listAccountsResult').html('');
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.error.message);
							}
						},
						function(reason) {
							console
									.log('Error: '
											+ reason.result.error.message);
							$('#apiResults').html('');
							$('#successmessage').html('');
							$('#listAccountsResult').html('');
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ reason.result.error.message);
						});

	}

	if (rdomemberID.checked) {

		var memberID = document.getElementById('txtmemberID').value;
		if (memberID.length == 0) {
			errormsg += '<li>' + " Member ID cannot be null " + '</li>';
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

		gapi.client.memberendpoint
				.selectAccountsByMemberID({
					'id' : memberID
				})
				.execute(
						function(resp) {
							console.log('response =>> ' + resp);
							if (!resp.code) {
								if (resp.result.success == false) {
									$('#errormessage').html(
											'operation failed! Error...<br/>'
													+ resp.result.resultMessage
															.toString());
									$('#apiResults').html('');
									$('#successmessage').html('');
									$('#listAccountsResult').html('');
								} else {
									resp.result.clientToken.items = resp.result.clientToken.items
											|| [];
									if (resp.result.clientToken.items == undefined
											|| resp.result.clientToken.items == null) {
										$('#listAccountsResult').html(
												'There are no Accounts...');
										$('#apiResults').html('');
										$('#successmessage').html('');
										$('#errormessage').html('');
									} else {
										$('#apiResults').html('');
										$('#successmessage').html('');
										$('#errormessage').html('');
										$('#listAccountsResult').html(
												'loading...');
										buildTable(resp.result.clientToken);
									}
								}
							} else {
								console.log('Error: ' + resp.error.message);
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.error.message);
								$('#apiResults').html('');
								$('#successmessage').html('');
								$('#listAccountsResult').html('');
							}
						},
						function(reason) {
							console
									.log('Error: '
											+ reason.result.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ reason.result.error.message);
							$('#apiResults').html('');
							$('#successmessage').html('');
							$('#listAccountsResult').html('');
						});
	}

};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.accountendpoint.listaccounts.enableButtons = function() {
	$("#btnLoad").removeAttr('style');
	$("#btnLoad").removeAttr('disabled');
	$("#btnLoad").val('Search');
	var btnLoad = document.querySelector('#btnLoad');
	btnLoad.addEventListener('click', function() {
		fanikiwa.accountendpoint.listaccounts.LoadAccounts();
	});

	var rdoaccountID = document.querySelector('#rdoaccountID');
	rdoaccountID.addEventListener('change', function() {
		if (this.checked == true) {
			$("#div-accountID").removeClass('displaynone');
			$("#div-accountID").addClass('displayblock');
			$("#div-accountID").show();

			$("#div-memberID").removeClass('displayblock');
			$("#div-memberID").addClass('displaynone');
			$("#div-memberID").hide();
		}
	});

	var rdomemberID = document.querySelector('#rdomemberID');
	rdomemberID.addEventListener('change', function() {
		if (this.checked == true) {
			$("#div-memberID").removeClass('displaynone');
			$("#div-memberID").addClass('displayblock');
			$("#div-memberID").show();

			$("#div-accountID").removeClass('displayblock');
			$("#div-accountID").addClass('displaynone');
			$("#div-accountID").hide();
		}
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
			fanikiwa.accountendpoint.listaccounts.enableButtons();
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

		$(".page-title").html(" Accounts [" + resp.items.length + "] ");

		accountsTable += '<table id="listAccountsTable">';
		accountsTable += "<thead>";
		accountsTable += "<tr>";
		accountsTable += "<th>Account ID</th>";
		accountsTable += "<th>Account Name</th>";
		accountsTable += "<th>Book Balance</th>";
		accountsTable += "<th>Cleared Balance</th>";
		accountsTable += "<th>Limit</th>";
		accountsTable += "<th>Available Balance</th>";
		accountsTable += "<th></th>";
		accountsTable += "<th></th>";
		accountsTable += "<th></th>";
		accountsTable += "</tr>";
		accountsTable += "</thead>";
		accountsTable += "<tbody>";

		for (var i = 0; i < resp.items.length; i++) {
			var availbal = resp.items[i].clearedBalance - resp.items[i].limit;
			accountsTable += '<tr>';
			accountsTable += '<td>' + resp.items[i].accountID + '</td>';
			accountsTable += '<td>' + resp.items[i].accountName + '</td>';
			accountsTable += '<td>' + resp.items[i].bookBalance.formatMoney(2)
					+ '</td>';
			accountsTable += '<td>'
					+ resp.items[i].clearedBalance.formatMoney(2) + '</td>';
			accountsTable += '<td>' + resp.items[i].limit.formatMoney(2)
					+ '</td>';
			accountsTable += '<td>' + availbal.formatMoney(2) + '</td>';

			accountsTable += '<td><a href="#" onclick="MiniStatement('
					+ resp.items[i].accountID + ')">Mini Statement</a> </td>';
			accountsTable += '<td><a href="#" onclick="Edit('
					+ resp.items[i].accountID + ')">Edit</a> </td>';
			accountsTable += '<td><a href="#" onclick="Details('
					+ resp.items[i].accountID + ')">Details</a> </td>';

			accountsTable += "</tr>";
		}

		accountsTable += "</tbody>";
		accountsTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
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
