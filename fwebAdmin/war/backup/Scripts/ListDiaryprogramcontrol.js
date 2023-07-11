/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.diaryprogramcontrolendpoint = fanikiwa.diaryprogramcontrolendpoint || {};
fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables = fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables
		|| {};

fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.LoadDiaryprogramcontrolTables = function() {

	$('#listdiaryprogramcontrolResult').html('loading...');

	gapi.client.diaryprogramcontrolendpoint.listDiaryprogramcontrol().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listdiaryprogramcontrolResult').html(
								'There are no diary program control...');
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
fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.diaryprogramcontrolendpoint.listdiaryprogramcontroltables.LoadDiaryprogramcontrolTables();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('diaryprogramcontrolendpoint', 'v1', callback, apiRoot);

};

var diaryprogramcontrolTable = '';
function buildTable(response) {

	diaryprogramcontrolTable = '';

	PopulateDiaryprogramcontrolTable(response);

	$("#listdiaryprogramcontrolResult").html(diaryprogramcontrolTable);
}

function PopulateDiaryprogramcontrolTable(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		diaryprogramcontrolTable += '<table id="listDiaryprogramcontrolTable">';
		diaryprogramcontrolTable += "<thead>";
		diaryprogramcontrolTable += "<tr>";
		diaryprogramcontrolTable += "<th>Id</th>";
		diaryprogramcontrolTable += "<th>Description</th>";  
		diaryprogramcontrolTable += "</tr>";
		diaryprogramcontrolTable += "</thead>";
		diaryprogramcontrolTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			diaryprogramcontrolTable += '<tr>';
			diaryprogramcontrolTable += '<td>' + resp.result.items[i].id + '</td>';
			diaryprogramcontrolTable += '<td>' + resp.result.items[i].description + '</td>';  
		}

		diaryprogramcontrolTable += "</tbody>";
		diaryprogramcontrolTable += "</table>";

	}
}
 