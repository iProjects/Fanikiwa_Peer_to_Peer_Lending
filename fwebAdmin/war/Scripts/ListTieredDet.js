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

	$('#listTieredDetTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

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
		tieredDetTable += "<th></th>";
		tieredDetTable += "<th></th>";
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
			tieredDetTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].id + ')">Edit</a> </td>';
			tieredDetTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].id + ')">Delete</a> </td>';
			tieredDetTable += "</tr>";
		}

		tieredDetTable += "</tbody>";
		tieredDetTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.edittiereddetid = id;
	window.location.href = "/Views/TieredDet/Edit.html";
}

function Delete(id) {
	// Define the Dialog and its properties.
	$("#div-delete-dialog").dialog({
		autoOpen : false,
		modal : true,
		title : "Confirm Delete",
		resizable : true,
		draggable : true,
		closeOnEscape : true,
		buttons : {
			"Yes" : function() {
				$(this).dialog('close');
				callback(true, id);
			},
			"No" : function() {
				$(this).dialog('close');
				callback(false, id);
			}
		}
	});

	$("#div-delete-dialog").html(
			"Are you sure you want to delete Tiered Table [ " + id + " ]");
	$("#div-delete-dialog").dialog("open");
}

function callback(value, id) {
	if (value) {
		DeleteNow(id);
	} else {
		return;
	}
}

function DeleteNow(id) {

	$('#apiResults').html('processing...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.tiereddetendpoint
			.removeTieredDet({
				'id' : id
			})
			.execute(
					function(resp) {
						if (!resp.code) {
							if (resp.result.success == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								$('#successmessage').html(
										'operation successful... <br/>'
												+ resp.result.resultMessage
														.toString());
								$('#errormessage').html('');
								$('#apiResults').html('');
								window
										.setTimeout(
												'window.location.href = "/Views/TieredDet/List.html";',
												1000);
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message.toString());
							$('#successmessage').html('');
							$('#apiResults').html('');
						}
					});
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/TieredDet/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/List.html" style="cursor: pointer;" >Tiered Tables</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
