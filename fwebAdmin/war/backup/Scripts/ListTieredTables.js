/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.tieredtableendpoint = fanikiwa.tieredtableendpoint || {};
fanikiwa.tieredtableendpoint.listtieredtables = fanikiwa.tieredtableendpoint.listtieredtables
		|| {};

fanikiwa.tieredtableendpoint.listtieredtables.LoadTieredTables = function() {

	$('#listTieredTablesResult').html('loading...');

	gapi.client.tieredtableendpoint.listTieredtable().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listTieredTablesResult').html(
								'There are no Tiered Tables...');
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
fanikiwa.tieredtableendpoint.listtieredtables.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.tieredtableendpoint.listtieredtables.LoadTieredTables();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('tieredtableendpoint', 'v1', callback, apiRoot);

};

var tieredTable = '';
function buildTable(response) {

	tieredTable = '';

	populateTieredTables(response);

	$("#listTieredTablesResult").html(tieredTable);
}

function populateTieredTables(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		tieredTable += '<table id="listTieredTable">';
		tieredTable += "<thead>";
		tieredTable += "<tr>";
		tieredTable += "<th>Id</th>";
		tieredTable += "<th>Description</th>";
		tieredTable += "<th></th>";
		tieredTable += "<th></th>";
		tieredTable += "</tr>";
		tieredTable += "</thead>";
		tieredTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			tieredTable += '<tr>';
			tieredTable += '<td>' + resp.result.items[i].id + '</td>';
			tieredTable += '<td>' + resp.result.items[i].description + '</td>';
			tieredTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].id + ')">Edit</a> </td>';
			tieredTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
			tieredTable += "</tr>";
		}

		tieredTable += "</tbody>";
		tieredTable += "</table>";

	}
}

function Details(id) {
	sessionStorage.tieredtableid = id;
	window.location.href = "/Views/TieredDet/List.html";
}

function Edit(id) {
	sessionStorage.edittieredtableid = id;
	window.location.href = "/Views/Tieredtable/Edit.html";
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
