/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.organizationendpoint = fanikiwa.organizationendpoint || {};
fanikiwa.organizationendpoint.listorganizations = fanikiwa.organizationendpoint.listorganizations
		|| {};

fanikiwa.organizationendpoint.listorganizations.LoadOrganizations = function() {

	$('#listOrganizationsResult').html('loading...');

	gapi.client.organizationendpoint.listOrganization().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listOrganizationsResult').html(
								'There are no Organizations...');
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
fanikiwa.organizationendpoint.listorganizations.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.organizationendpoint.listorganizations.LoadOrganizations();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('organizationendpoint', 'v1', callback, apiRoot);

};

var organizationTable = '';
function buildTable(response) {

	organizationTable = '';

	populateOrganizations(response);

	$("#listOrganizationsResult").html(organizationTable);

	$('#listOrganizationsTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateOrganizations(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		organizationTable += '<table id="listOrganizationsTable">';
		organizationTable += "<thead>";
		organizationTable += "<tr>";
		organizationTable += "<th>ID</th>";
		organizationTable += "<th>Name</th>";
		organizationTable += "<th>Email</th>";
		organizationTable += "<th>Address</th>";
		organizationTable += "<th></th>";
		organizationTable += "<th></th>";
		organizationTable += "</tr>";
		organizationTable += "</thead>";
		organizationTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			organizationTable += '<tr>';
			organizationTable += '<td>' + resp.result.items[i].organizationID
					+ '</td>';
			organizationTable += '<td>' + resp.result.items[i].name + '</td>';
			organizationTable += '<td>' + resp.result.items[i].email + '</td>';
			organizationTable += '<td>' + resp.result.items[i].address
					+ '</td>';
			organizationTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].organizationID + ')">Edit</a> </td>';
			organizationTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].organizationID
					+ ')">Delete</a> </td>';
			organizationTable += "</tr>";
		}

		organizationTable += "</tbody>";
		organizationTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.editorganizationid = id;
	window.location.href = "/Views/Organization/Edit.html";
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
			"Are you sure you want to delete Organization [ " + id + " ]");
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

	gapi.client.organizationendpoint
			.removeOrganization({
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
												'window.location.href = "/Views/Organization/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/Organization/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
