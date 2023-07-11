/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.myinvestments = fanikiwa.loanendpoint.myinvestments || {};

fanikiwa.loanendpoint.myinvestments.LoadLoans = function() {

	$('#listLoansResult').html('loading...');
        $('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;

	gapi.client.loanendpoint.selectDtoMyInvestments({
		'email' : email
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listLoansResult')
								.html('You have no Investments...');
					} else {
						buildTable(resp);
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
fanikiwa.loanendpoint.myinvestments.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.myinvestments.LoadLoans();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

var loanTable = '';
function buildTable(response) {

	loanTable = '';

	populateLoans(response);

	$("#listLoansResult").html(loanTable);

	$('#listLoansTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateLoans(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		loanTable += '<table id="listLoansTable">';
		loanTable += "<thead>";
		loanTable += "<tr>";
		loanTable += "<th>Maturity Date</th>";
		loanTable += "<th>Borrower</th>";
		loanTable += "<th>Term</th>";
		loanTable += "<th>Interest Rate</th>";
		loanTable += "<th>Amount</th>";
		loanTable += "<th>Accrued Interest</th>";
		loanTable += "<th>Total Loan</th>";
		loanTable += "<th></th>";
		loanTable += "<th></th>";
		loanTable += "</tr>";
		loanTable += "</thead>";
		loanTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			if (resp.result.items[i] != null) {
				var totalToPay = resp.result.items[i].amount
						+ resp.result.items[i].accruedInterest;
				loanTable += '<tr>';
				loanTable += '<td>'
						+ formatDate(resp.result.items[i].maturityDate)
						+ '</td>';
				loanTable += '<td>' + resp.result.items[i].borrower + '</td>';
				loanTable += '<td>' + resp.result.items[i].term + '</td>';
				loanTable += '<td>' + resp.result.items[i].interestRate
						+ '</td>';
				loanTable += '<td>'
						+ resp.result.items[i].amount.formatMoney(2) + '</td>';
				loanTable += '<td>'
						+ resp.result.items[i].accruedInterest.formatMoney(2)
						+ '</td>';
				loanTable += '<td>' + totalToPay.formatMoney(2) + '</td>';

				loanTable += '<td><a href="#" onclick="RepaymentSchedule('
						+ resp.result.items[i].id
						+ ')">Repayment Schedule</a> </td>';
				loanTable += '<td><a href="#" onclick="Details('
						+ resp.result.items[i].id + ')">Details</a> </td>';
				loanTable += "</tr>";
			}
		}
		loanTable += "</tbody>";
		loanTable += "</table>";

	}
}

function RepaymentSchedule(id) {
	sessionStorage.repaymentscheduleloanid = id;
	window.location.href = "/Views/Loans/RepaymentSchedule.html";
}
function Details(id) {
	sessionStorage.myinvestmentdetailsid = id;
	window.location.href = "/Views/Loans/MyInvestmentDetails.html";
}

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
