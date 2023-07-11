/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.mpesaipnmessageendpoint = fanikiwa.mpesaipnmessageendpoint || {};
fanikiwa.mpesaipnmessageendpoint.listmpesaipnmessages = fanikiwa.mpesaipnmessageendpoint.listmpesaipnmessages
		|| {};

fanikiwa.mpesaipnmessageendpoint.listmpesaipnmessages.LoadMpesaIPNMessage = function() {

	$('#listmpesaipnmessageResult').html('loading...');

	gapi.client.mpesaipnmessageendpoint.listMpesaIPNMessage().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listmpesaipnmessageResult').html(
								'There are no mpesa ipn messages...');
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
fanikiwa.mpesaipnmessageendpoint.listmpesaipnmessages.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.mpesaipnmessageendpoint.listmpesaipnmessages
					.LoadMpesaIPNMessage();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('mpesaipnmessageendpoint', 'v1', callback, apiRoot);

};

var mpesaipnmessageTable = '';
function buildTable(response) {

	mpesaipnmessageTable = '';

	PopulateMpesaIPNMessageTable(response);

	$("#listmpesaipnmessageResult").html(mpesaipnmessageTable);

	$('#listMpesaIPNMessageTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function PopulateMpesaIPNMessageTable(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		mpesaipnmessageTable += '<table id="listMpesaIPNMessageTable">';
		mpesaipnmessageTable += "<thead>";
		mpesaipnmessageTable += "<tr>";
		mpesaipnmessageTable += "<th>Id</th>";
		mpesaipnmessageTable += "<th>IPN Id</th>";
		mpesaipnmessageTable += "<th>Code</th>";
		mpesaipnmessageTable += "<th>Acccont</th>";
		mpesaipnmessageTable += "<th>Sender</th>";
		mpesaipnmessageTable += "<th>Amount</th>";
		mpesaipnmessageTable += "<th>Status</th>";
		mpesaipnmessageTable += "<th>Transaction Date</th>";
		mpesaipnmessageTable += "<th></th>";
		mpesaipnmessageTable += "</tr>";
		mpesaipnmessageTable += "</thead>";
		mpesaipnmessageTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			mpesaipnmessageTable += '<tr>';
			mpesaipnmessageTable += '<td>' + resp.result.items[i].id + '</td>';
			mpesaipnmessageTable += '<td>'
					+ resp.result.items[i].mpesaIPNMessageID + '</td>';
			mpesaipnmessageTable += '<td>' + resp.result.items[i].mpesa_code
					+ '</td>';
			mpesaipnmessageTable += '<td>' + resp.result.items[i].mpesa_acc
					+ '</td>';
			mpesaipnmessageTable += '<td>' + resp.result.items[i].mpesa_sender
					+ '</td>';
			mpesaipnmessageTable += '<td>'
					+ resp.result.items[i].mpesa_amt.formatMoney(2) + '</td>';
			mpesaipnmessageTable += '<td>' + resp.result.items[i].status
					+ '</td>';
			mpesaipnmessageTable += '<td>'
					+ resp.result.items[i].mpesa_trx_date + '</td>';
			mpesaipnmessageTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
		}

		mpesaipnmessageTable += "</tbody>";
		mpesaipnmessageTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Details(id) {
	sessionStorage.mpesaipnmessagedetailsid = id;
	window.location.href = "/Views/MpesaIPNMessage/Details.html";
}
