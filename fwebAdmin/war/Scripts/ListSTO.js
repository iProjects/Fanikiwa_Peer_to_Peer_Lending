/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.stoendpoint = fanikiwa.stoendpoint || {};
fanikiwa.stoendpoint.liststo = fanikiwa.stoendpoint.liststo || {};

fanikiwa.stoendpoint.liststo.LoadSto = function() {

	$('#listStoResult').html('loading...');

	gapi.client.stoendpoint.listSTO().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listStoResult').html(
								'There are no Standing Orders...');
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
fanikiwa.stoendpoint.liststo.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.stoendpoint.liststo.LoadSto();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('stoendpoint', 'v1', callback, apiRoot);

};

var stoTable = '';
function buildTable(response) {

	stoTable = '';

	populateSto(response);

	$("#listStoResult").html(stoTable);

	$('#listStoTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateSto(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		stoTable += '<table id="listStoTable">';
		stoTable += "<thead>";
		stoTable += "<tr>";
		stoTable += "<th>Id</th>";
		stoTable += "<th>Loan Id</th>";
		stoTable += "<th>Create Date</th>";
		stoTable += "<th>End Date</th>";
		stoTable += "<th>Next Pay Date</th>";
		stoTable += "<th>Pay Amount</th>";
		stoTable += "<th>Interest Amount</th>";
		stoTable += "<th>Total To Pay</th>";
		stoTable += "<th>Amount Paid</th>";
		stoTable += "<th>No Of Payments</th>";
		stoTable += "<th>No Of Payments Made</th>";
		stoTable += "<th></th>";
		stoTable += "<th></th>";
		stoTable += "</tr>";
		stoTable += "</thead>";
		stoTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			if (resp.result.items[i] != null) {
				stoTable += '<tr>';
				stoTable += '<td>' + resp.result.items[i].id + '</td>';
				stoTable += '<td>' + resp.result.items[i].loanId + '</td>';
				stoTable += '<td>'
						+ formatDate(resp.result.items[i].createDate) + '</td>';
				stoTable += '<td>' + formatDate(resp.result.items[i].endDate)
						+ '</td>';
				stoTable += '<td>'
						+ formatDate(resp.result.items[i].nextPayDate)
						+ '</td>';
				stoTable += '<td>'
						+ resp.result.items[i].payAmount.formatMoney(2)
						+ '</td>';
				stoTable += '<td>'
						+ resp.result.items[i].interestAmount.formatMoney(2)
						+ '</td>';
				stoTable += '<td>'
						+ resp.result.items[i].totalToPay.formatMoney(2)
						+ '</td>';
				stoTable += '<td>'
						+ resp.result.items[i].amountPaid.formatMoney(2)
						+ '</td>';
				stoTable += '<td>' + resp.result.items[i].noOfPayments
						+ '</td>';
				stoTable += '<td>' + resp.result.items[i].noOfPaymentsMade
						+ '</td>';
				stoTable += '<td><a href="#" onclick="Edit('
						+ resp.result.items[i].id + ')">Edit</a> </td>';
				stoTable += '<td><a href="#" onclick="Details('
						+ resp.result.items[i].id + ')">Details</a> </td>';
				stoTable += "</tr>";
			}
		}
		stoTable += "</tbody>";
		stoTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.editstoid = id;
	window.location.href = "/Views/STO/Edit.html";
}

function Details(id) {
	sessionStorage.stodetailsid = id;
	window.location.href = "/Views/STO/Details.html";
}
