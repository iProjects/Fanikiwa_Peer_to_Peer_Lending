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
	document.getElementById('dtpenddate').value = formatDateForControl(new Date());
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
		errormsg += '<li>' + " Select Account " + '</li>';
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

	$('#listAccountsResult').html('loading...');

	gapi.client.accountendpoint.statement({
		'accountID' : accountID,
		'sdate' : sdate,
		'edate' : edate
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
fanikiwa.accountendpoint.statement.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.statement.enableButtons();
			fanikiwa.accountendpoint.statement.LoadAccounts();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
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
	var email = sessionStorage.getItem('loggedinuser');
	var accountsOptions = '';
	gapi.client.accountendpoint
			.listAccount()
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