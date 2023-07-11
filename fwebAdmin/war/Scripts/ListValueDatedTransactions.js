/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.valuedatedtransactionendpoint = fanikiwa.valuedatedtransactionendpoint
		|| {};
fanikiwa.valuedatedtransactionendpoint.listvaluedatedtransactions = fanikiwa.valuedatedtransactionendpoint.listvaluedatedtransactions
		|| {};

fanikiwa.valuedatedtransactionendpoint.listvaluedatedtransactions.LoadValueDatedTransactions = function() {

	$('#listValueDatedTransactionsResult').html('loading...');

	gapi.client.valuedatedtransactionendpoint.selectDtoTransactions().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listValueDatedTransactionsResult').html(
								'There are no Value Dated Transactions...');
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
fanikiwa.valuedatedtransactionendpoint.listvaluedatedtransactions.init = function(
		apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.valuedatedtransactionendpoint.listvaluedatedtransactions
					.LoadValueDatedTransactions();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('valuedatedtransactionendpoint', 'v1', callback, apiRoot);

};

var valuedatedtransactionTable = '';
function buildTable(response) {

	valuedatedtransactionTable = '';

	populateValueDatedTransactions(response);

	$("#listValueDatedTransactionsResult").html(valuedatedtransactionTable);

	$('#listValueDatedTransactionTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateValueDatedTransactions(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		valuedatedtransactionTable += '<table id="listValueDatedTransactionTable">';
		valuedatedtransactionTable += "<thead>";
		valuedatedtransactionTable += "<tr>";
		valuedatedtransactionTable += "<th>Id</th>";
		valuedatedtransactionTable += "<th>Amount</th>";
		valuedatedtransactionTable += "<th>Authorizer</th>";
		valuedatedtransactionTable += "<th>Contra Reference</th>";
		valuedatedtransactionTable += "<th>Force Post Flag</th>";
		valuedatedtransactionTable += "<th>Narrative</th>";
		valuedatedtransactionTable += "<th>Post Date</th>";
		valuedatedtransactionTable += "<th>Record Date</th>";
		valuedatedtransactionTable += "<th>Reference</th>";
		valuedatedtransactionTable += "<th>Statement Flag</th>";
		valuedatedtransactionTable += "<th>User ID</th>";
		valuedatedtransactionTable += "<th>Value Date</th>";
		valuedatedtransactionTable += "<th>Account</th>";
		valuedatedtransactionTable += "<th>Transaction Type</th>";
		valuedatedtransactionTable += "<th></th>";
		valuedatedtransactionTable += "</tr>";
		valuedatedtransactionTable += "</thead>";
		valuedatedtransactionTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			if (resp.result.items[i] != null) {
				valuedatedtransactionTable += '<tr>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].transactionID + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].amount.formatMoney(2) + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].authorizer + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].contraReference + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].forcePostFlag + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].narrative + '</td>';
				valuedatedtransactionTable += '<td>'
						+ formatDate(resp.result.items[i].postDate) + '</td>';
				valuedatedtransactionTable += '<td>'
						+ formatDate(resp.result.items[i].recordDate) + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].reference + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].statementFlag + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].userID + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].valueDate + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].account + '</td>';
				valuedatedtransactionTable += '<td>'
						+ resp.result.items[i].transactionType + '</td>';
				valuedatedtransactionTable += '<td><a href="#" onclick="Details('
						+ resp.result.items[i].id + ')">Details</a> </td>';
				valuedatedtransactionTable += "</tr>";
			}
		}
		valuedatedtransactionTable += "</tbody>";
		valuedatedtransactionTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Details(id) {
	sessionStorage.valuedatedtransactiondetailsid = id;
	window.location.href = "/Views/ValueDatedTransaction/Details.html";
}
