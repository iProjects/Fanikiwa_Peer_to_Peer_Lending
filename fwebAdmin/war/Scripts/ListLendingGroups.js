/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.lendinggroupendpoint = fanikiwa.lendinggroupendpoint || {};
fanikiwa.lendinggroupendpoint.listlendinggroups = fanikiwa.lendinggroupendpoint.listlendinggroups
		|| {};

fanikiwa.lendinggroupendpoint.listlendinggroups.LoadLendingGroups = function() {

	$('#listLendingGroupsResult').html('loading...');

	gapi.client.lendinggroupendpoint.selectLendinggroups().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listLendingGroupsResult').html(
								'There are no Lending Groups...');
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
fanikiwa.lendinggroupendpoint.listlendinggroups.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.lendinggroupendpoint.listlendinggroups.LoadLendingGroups();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('lendinggroupendpoint', 'v1', callback, apiRoot);

};

var lendinggroupTable = '';
function buildTable(response) {

	lendinggroupTable = '';

	populateLendingGroups(response);

	$("#listLendingGroupsResult").html(lendinggroupTable);

	$('#listLendingGroupsTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateLendingGroups(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		lendinggroupTable += '<table id="listLendingGroupsTable">';
		lendinggroupTable += "<thead>";
		lendinggroupTable += "<tr>";
		lendinggroupTable += "<th>Name Id</th>";
		lendinggroupTable += "<th>Creator Email</th>";
		lendinggroupTable += "<th>Created On</th>";
		lendinggroupTable += "<th>Last Modified</th>";
		lendinggroupTable += "<th>Parent Group</th>";
		lendinggroupTable += "</tr>";
		lendinggroupTable += "</thead>";
		lendinggroupTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			lendinggroupTable += '<tr>';
			lendinggroupTable += '<td>' + resp.result.items[i].groupName
					+ '</td>';
			lendinggroupTable += '<td>' + resp.result.items[i].creatorEmail
					+ '</td>';
			lendinggroupTable += '<td>'
					+ formatDate(resp.result.items[i].createdOn) + '</td>';
			lendinggroupTable += '<td>'
					+ formatDate(resp.result.items[i].lastModified) + '</td>';
			lendinggroupTable += '<td>' + resp.result.items[i].parentGroup
					+ '</td>';
			lendinggroupTable += "</tr>";
		}

		lendinggroupTable += "</tbody>";
		lendinggroupTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}
