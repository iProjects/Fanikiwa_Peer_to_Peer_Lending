/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.repaymentschedule = fanikiwa.loanendpoint.repaymentschedule
		|| {};

fanikiwa.loanendpoint.repaymentschedule.LoadRepaymentSchedule = function() {

	$('#listRepaymentScheduleResult').html('loading...');
	var id = sessionStorage.getItem('repaymentscheduleloanid');

	gapi.client.loanendpoint
			.loanRepaymentSchedule({
				"id" : id
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
									$('#listAccountsResult')
											.html(
													'There is no Repayment Schedule...');
								} else {
									$('#apiResults').html('');
									$('#successmessage').html('');
									$('#errormessage').html('');
									$('#listAccountsResult').html('loading...');
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
						console.log('Error: ' + reason.result.error.message);
						$('#apiResults').html('');
						$('#successmessage').html('');
						$('#listAccountsResult').html('');
						$('#errormessage').html(
								'operation failed! Error...<br/>'
										+ reason.result.error.message);
					});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.loanendpoint.repaymentschedule.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.repaymentschedule.LoadRepaymentSchedule();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

var repaymentscheduleTable = '';
function buildTable(response) {

	repaymentscheduleTable = '';

	populateRepaymentSchedule(response);

	$("#listRepaymentScheduleResult").html(repaymentscheduleTable);

	$('#listRepaymentScheduleTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateRepaymentSchedule(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.items.length + "] ");

		repaymentscheduleTable += '<table id="listRepaymentScheduleTable">';
		repaymentscheduleTable += "<thead>";
		repaymentscheduleTable += "<tr>";
		repaymentscheduleTable += "<th>Installment</th>";
		repaymentscheduleTable += "<th>Repayment Date</th>";
		repaymentscheduleTable += "<th>Amount</th>";
		repaymentscheduleTable += "<th>Balance</th>";
		repaymentscheduleTable += "</tr>";
		repaymentscheduleTable += "</thead>";
		repaymentscheduleTable += "<tbody>";

		for (var i = 0; i < resp.items.length; i++) {
			if (resp.items[i] != null) {

				repaymentscheduleTable += '<tr>';
				repaymentscheduleTable += '<td>' + (i + 1) + '</td>';
				repaymentscheduleTable += '<td>'
						+ formatDate(resp.items[i].repaymentDate) + '</td>';
				repaymentscheduleTable += '<td>'
						+ resp.items[i].amount.formatMoney(2) + '</td>';
				repaymentscheduleTable += '<td>'
						+ resp.items[i].balance.formatMoney(2) + '</td>';
				repaymentscheduleTable += "</tr>";
			}
		}
		repaymentscheduleTable += "</tbody>";
		repaymentscheduleTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}
