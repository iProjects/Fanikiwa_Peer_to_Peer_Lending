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

	gapi.client.coaendpoint.listCoa().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listCoaResult').html(
								'There are no Chart Of Accounts...');
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

	$('#listCoaTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

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
		coaTable += "<th></th>";
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
			coaTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].id + ')">Delete</a> </td>';
			coaTable += "</tr>";
		}

		coaTable += "</tbody>";
		coaTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
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
			"Are you sure you want to delete Chart of Account [ " + id + " ]");
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

	gapi.client.coaendpoint
			.removeCoa({
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
												'window.location.href = "/Views/Coa/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/Coa/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
