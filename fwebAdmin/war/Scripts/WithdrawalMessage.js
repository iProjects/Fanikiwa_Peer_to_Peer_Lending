/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.withdrawalmessageendpoint = fanikiwa.withdrawalmessageendpoint || {};
fanikiwa.withdrawalmessageendpoint.listwithdrawalmessages = fanikiwa.withdrawalmessageendpoint.listwithdrawalmessages
		|| {};

fanikiwa.withdrawalmessageendpoint.listwithdrawalmessages.LoadWithdrawalMessages = function() {

	$('#listwithdrawalmessageResult').html('loading...');

	gapi.client.withdrawalmessageendpoint.listWithdrawalMessage().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listwithdrawalmessageResult').html(
								'There are no Withdrawal Messages...');
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
fanikiwa.withdrawalmessageendpoint.listwithdrawalmessages.init = function(
		apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.withdrawalmessageendpoint.listwithdrawalmessages
					.LoadWithdrawalMessages();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('withdrawalmessageendpoint', 'v1', callback, apiRoot);

};

var withdrawalmessageTable = '';
function buildTable(response) {

	withdrawalmessageTable = '';

	PopulateDiaryprogramcontrolTable(response);

	$("#listwithdrawalmessageResult").html(withdrawalmessageTable);
}

function PopulateDiaryprogramcontrolTable(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		withdrawalmessageTable += '<table id="listDiaryprogramcontrolTable">';
		withdrawalmessageTable += "<thead>";
		withdrawalmessageTable += "<tr>";
		withdrawalmessageTable += "<th>Id</th>";
		withdrawalmessageTable += "<th>Account Id</th>";
		withdrawalmessageTable += "<th>Member Id</th>";
		withdrawalmessageTable += "<th>Amount</th>";
		withdrawalmessageTable += "<th>Remission Method</th>";
		withdrawalmessageTable += "<th>Status</th>";
		withdrawalmessageTable += "<th>Remarks</th>";
		withdrawalmessageTable += "</tr>";
		withdrawalmessageTable += "</thead>";
		withdrawalmessageTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			if (resp.result.items[i] != null) {
				withdrawalmessageTable += '<tr>';
				withdrawalmessageTable += '<td>' + resp.result.items[i].id
						+ '</td>';
				withdrawalmessageTable += '<td>'
						+ resp.result.items[i].accountId + '</td>';
				withdrawalmessageTable += '<td>'
						+ resp.result.items[i].memberId + '</td>';
				withdrawalmessageTable += '<td>'
						+ resp.result.items[i].amount.formatMoney(2) + '</td>';
				withdrawalmessageTable += '<td>'
						+ resp.result.items[i].remissionMethod + '</td>';
				withdrawalmessageTable += '<td>' + resp.result.items[i].status
						+ '</td>';
				withdrawalmessageTable += '<td>' + resp.result.items[i].remarks
						+ '</td>';
			}
		}
		withdrawalmessageTable += "</tbody>";
		withdrawalmessageTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}
