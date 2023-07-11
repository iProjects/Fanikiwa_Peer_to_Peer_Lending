/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.coaendpoint = fanikiwa.coaendpoint || {};
fanikiwa.coaendpoint.listcoa = fanikiwa.coaendpoint.listcoa || {};

fanikiwa.coaendpoint.listcoa.LoadCoa = function() {

	$('#listCoaResult').html('loading...');

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.coaendpoint.listCoa().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listCoaResult').html('There are no Chart Of Accounts...');
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
fanikiwa.coaendpoint.listcoa.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.coaendpoint.listcoa.LoadCoa();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('coaendpoint', 'v1', callback, apiRoot);

};

var coaTable = '';
function buildTable(response) {

	coaTable = '';

	populateCoa(response);

	$("#listCoaResult").html(coaTable);
}

function populateCoa(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		coaTable += '<table id="listCoaTable">';
		coaTable += "<thead>";
		coaTable += "<tr>";
		coaTable += "<th>Id</th>";
		coaTable += "<th>Description</th>";
		coaTable += "<th></th>";
		coaTable += "</tr>";
		coaTable += "</thead>";
		coaTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			coaTable += '<tr>';
			coaTable += '<td>' + resp.result.items[i].id + '</td>';
			coaTable += '<td>' + resp.result.items[i].description + '</td>';
			coaTable += '<td><a href="#" onclick="Edit('
				+ resp.result.items[i].id + ')">Edit</a> </td>';
			coaTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
			coaTable += "</tr>";
		}

		coaTable += "</tbody>";
		coaTable += "</table>";

	}
}

function Edit(id) {
	sessionStorage.editcoaid = id;
	window.location.href = "/Views/Coa/Edit.html";
}

function Details(id) {
	sessionStorage.coaid = id;
	window.location.href = "/Views/CoaDet/List.html";
}
