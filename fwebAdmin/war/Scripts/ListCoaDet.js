/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.coadetendpoint = fanikiwa.coadetendpoint || {};
fanikiwa.coadetendpoint.listcoadet = fanikiwa.coadetendpoint.listcoadet || {};

fanikiwa.coadetendpoint.listcoadet.LoadCoaDet = function() {

	$('#listCoaDetResult').html('loading...');

	var coaid = sessionStorage.getItem('coaid');

	gapi.client.coadetendpoint.retrieveCoadetsDTO({
		coaid : coaid
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listCoaDetResult').html(
								'There are no Chart Of Accounts Details...');
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
fanikiwa.coadetendpoint.listcoadet.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.coadetendpoint.listcoadet.LoadCoaDet();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);

};

var coadetTable = '';
function buildTable(response) {

	coadetTable = '';

	populateCoaDet(response);

	$("#listCoaDetResult").html(coadetTable);

	$('#listCoaDetTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateCoaDet(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		coadetTable += '<table id="listCoaDetTable">';
		coadetTable += "<thead>";
		coadetTable += "<tr>";
		coadetTable += "<th>Id</th>";
		coadetTable += "<th>Short Code</th>";
		coadetTable += "<th>Description</th>";
		coadetTable += "<th>Coa Id</th>";
		coadetTable += "<th>ROrder</th>";
		coadetTable += "<th>Coa Level</th>";
		coadetTable += "<th></th>";
		coadetTable += "<th></th>";
		coadetTable += "</tr>";
		coadetTable += "</thead>";
		coadetTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			coadetTable += '<tr>';
			coadetTable += '<td>' + resp.result.items[i].id + '</td>';
			coadetTable += '<td>' + resp.result.items[i].shortCode + '</td>';
			coadetTable += '<td>' + resp.result.items[i].description + '</td>';
			coadetTable += '<td>' + resp.result.items[i].coa + '</td>';
			coadetTable += '<td>' + resp.result.items[i].rorder + '</td>';
			coadetTable += '<td>' + resp.result.items[i].coaLevel + '</td>';
			coadetTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].id + ')">Edit</a> </td>';
			coadetTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].id + ')">Delete</a> </td>';
			coadetTable += "</tr>";
		}

		coadetTable += "</tbody>";
		coadetTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.editcoadetid = id;
	window.location.href = "/Views/CoaDet/Edit.html";
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
			"Are you sure you want to delete Coa Detail [ " + id + " ]");
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

	gapi.client.coadetendpoint
			.removeCoadet({
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
												'window.location.href = "/Views/CoaDet/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/CoaDet/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/CoaDet/List.html" style="cursor: pointer;" >Chart of Account Details</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
