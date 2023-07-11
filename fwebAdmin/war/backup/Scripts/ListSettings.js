/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.settingsendpoint = fanikiwa.settingsendpoint || {};
fanikiwa.settingsendpoint.listsettings = fanikiwa.settingsendpoint.listsettings
		|| {};

fanikiwa.settingsendpoint.listsettings.LoadSettings = function() {

	$('#listSettingsResult').html('loading...');

	gapi.client.settingsendpoint.listSettings().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listSettingsResult').html('There are no Settings...');
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
fanikiwa.settingsendpoint.listsettings.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.settingsendpoint.listsettings.LoadSettings();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('settingsendpoint', 'v1', callback, apiRoot);

};

var settingsTable = '';
function buildTable(response) {

	settingsTable = '';

	populateSettings(response);

	$("#listSettingsResult").html(settingsTable);
}

function populateSettings(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		settingsTable += '<table id="listSettingsTable">';
		settingsTable += "<thead>";
		settingsTable += "<tr>";
		settingsTable += "<th>Property</th>";
		settingsTable += "<th>Value</th>";
		settingsTable += "<th>Group Name</th>";
		settingsTable += "<th></th>";
		settingsTable += "</tr>";
		settingsTable += "</thead>";
		settingsTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			settingsTable += '<tr>';
			settingsTable += '<td>' + resp.result.items[i].property + '</td>';
			settingsTable += '<td>' + resp.result.items[i].value + '</td>';
			settingsTable += '<td>' + resp.result.items[i].groupName + '</td>';
			settingsTable += '<td><a href="#" onclick="Edit('
					+ "'"+ resp.result.items[i].property +"'"+ ')">Edit</a> </td>'; 
			settingsTable += "</tr>";
		}

		settingsTable += "</tbody>";
		settingsTable += "</table>";

	}
}

function Edit(id) {
	sessionStorage.editsettingid = id;
	window.location.href = "/Views/Setting/Edit.html";
}
 
function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Setting/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
