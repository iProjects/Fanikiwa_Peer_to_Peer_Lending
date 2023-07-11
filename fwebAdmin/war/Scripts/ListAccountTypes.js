/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accounttypeendpoint = fanikiwa.accounttypeendpoint || {};
fanikiwa.accounttypeendpoint.listaccounttypes = fanikiwa.accounttypeendpoint.listaccounttypes
		|| {};

fanikiwa.accounttypeendpoint.listaccounttypes.LoadAccountTypes = function() {

	$('#listAccountTypesResult').html('loading...');

	gapi.client.accounttypeendpoint.listAccountType().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listAccountTypesResult').html(
								'There are no Account Types...');
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
fanikiwa.accounttypeendpoint.listaccounttypes.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accounttypeendpoint.listaccounttypes.LoadAccountTypes();
		}
	}
	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('accounttypeendpoint', 'v1', callback, apiRoot);

};

var accounttypeTable = '';
function buildTable(response) {

	accounttypeTable = '';

	populateAccountTypes(response);

	$("#listAccountTypesResult").html(accounttypeTable);

	$('#listAccountTypesTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateAccountTypes(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		accounttypeTable += '<table id="listAccountTypesTable">';
		accounttypeTable += "<thead>";
		accounttypeTable += "<tr>";
		accounttypeTable += "<th>Id</th>";
		accounttypeTable += "<th>Short Code</th>";
		accounttypeTable += "<th>Description</th>";
		accounttypeTable += "<th></th>";
		accounttypeTable += "<th></th>";
		accounttypeTable += "</tr>";
		accounttypeTable += "</thead>";
		accounttypeTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			accounttypeTable += '<tr>';
			accounttypeTable += '<td>' + resp.result.items[i].id + '</td>';
			accounttypeTable += '<td>' + resp.result.items[i].shortCode
					+ '</td>';
			accounttypeTable += '<td>' + resp.result.items[i].description
					+ '</td>';
			accounttypeTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].id + ')">Edit</a> </td>';
			accounttypeTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].id + ')">Delete</a> </td>';
			accounttypeTable += "</tr>";
		}

		accounttypeTable += "</tbody>";
		accounttypeTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.editaccounttypeid = id;
	window.location.href = "/Views/AccountType/Edit.html";
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
			"Are you sure you want to delete Account Type [ " + id + " ]");
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

	gapi.client.accounttypeendpoint
			.removeAccountType({
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
												'window.location.href = "/Views/AccountType/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/AccountType/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
