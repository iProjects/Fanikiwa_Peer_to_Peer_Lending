/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.tiereddetendpoint = fanikiwa.tiereddetendpoint || {};
fanikiwa.tiereddetendpoint.listtiereddet = fanikiwa.tiereddetendpoint.listtiereddet
		|| {};

fanikiwa.tiereddetendpoint.listtiereddet.LoadTieredDet = function() {

	$('#listTieredDetResult').html('loading...');

	var tieredtableid = sessionStorage.getItem('tieredtableid');

	gapi.client.tiereddetendpoint.selectTieredtableDets({
		tieredtableid : tieredtableid
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listTieredDetResult').html(
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
fanikiwa.tiereddetendpoint.listtiereddet.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.tiereddetendpoint.listtiereddet.LoadTieredDet();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('tiereddetendpoint', 'v1', callback, apiRoot);

};

var tieredDetTable = '';
function buildTable(response) {

	tieredDetTable = '';

	populateTieredDetTable(response);

	$("#listTieredDetResult").html(tieredDetTable);
}

function populateTieredDetTable(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		tieredDetTable += '<table id="listTieredDetTable">';
		tieredDetTable += "<thead>";
		tieredDetTable += "<tr>";
		tieredDetTable += "<th>Id</th>";
		tieredDetTable += "<th>Max</th>";
		tieredDetTable += "<th>Min</th>";
		tieredDetTable += "<th>Rate</th>";
		tieredDetTable += "<th>Absolute</th>";
		tieredDetTable += "<th>Tiered Table Id</th>";
		tieredDetTable += "</tr>";
		tieredDetTable += "</thead>";
		tieredDetTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			tieredDetTable += '<tr>';
			tieredDetTable += '<td>' + resp.result.items[i].id + '</td>';
			tieredDetTable += '<td>' + resp.result.items[i].max + '</td>';
			tieredDetTable += '<td>' + resp.result.items[i].min + '</td>';
			tieredDetTable += '<td>' + resp.result.items[i].rate + '</td>';
			tieredDetTable += '<td>' + resp.result.items[i].absolute + '</td>';
			tieredDetTable += '<td>' + resp.result.items[i].tieredID + '</td>';
			tieredDetTable += "</tr>";
		}

		tieredDetTable += "</tbody>";
		tieredDetTable += "</table>";

	}
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TieredDet/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
