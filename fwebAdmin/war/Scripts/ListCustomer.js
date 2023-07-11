/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.customerendpoint = fanikiwa.customerendpoint || {};
fanikiwa.customerendpoint.listcustomers = fanikiwa.customerendpoint.listcustomers
		|| {};

fanikiwa.customerendpoint.listcustomers.LoadCustomers = function() {

	$('#listCustomersResult').html('loading...');

	gapi.client.customerendpoint.listCustomer().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listCustomersResult').html(
								'There are no Customers...');
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
fanikiwa.customerendpoint.listcustomers.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.customerendpoint.listcustomers.LoadCustomers();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);

};

var customerTable = '';
function buildTable(response) {

	customerTable = '';

	populateCustomers(response);

	$("#listCustomersResult").html(customerTable);

	$('#listCustomersTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateCustomers(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		customerTable += '<table id="listCustomersTable">';
		customerTable += "<thead>";
		customerTable += "<tr>";
		customerTable += "<th>Id</th>";
		customerTable += "<th>Name</th>";
		customerTable += "<th>Telephone</th>";
		customerTable += "<th>Email</th>";
		customerTable += "<th>Address</th>";
		customerTable += "<th>Created Date</th>";
		customerTable += "<th></th>";
		customerTable += "<th></th>";
		customerTable += "<th></th>";
		customerTable += "</tr>";
		customerTable += "</thead>";
		customerTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			customerTable += '<tr>';
			customerTable += '<td>' + resp.result.items[i].customerId + '</td>';
			customerTable += '<td>' + resp.result.items[i].name + '</td>';
			customerTable += '<td>' + resp.result.items[i].telephone + '</td>';
			customerTable += '<td>' + resp.result.items[i].email + '</td>';
			customerTable += '<td>' + resp.result.items[i].address + '</td>';
			customerTable += '<td>'
					+ formatDate(resp.result.items[i].createdDate) + '</td>';
			customerTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].customerId + ')">Edit</a> </td>';
			customerTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].customerId + ')">Details</a> </td>';
			customerTable += '<td><a href="#" onclick="Delete('
					+ resp.result.items[i].customerId + ')">Delete</a> </td>';
			customerTable += "</tr>";
		}

		customerTable += "</tbody>";
		customerTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Edit(id) {
	sessionStorage.editcustomerid = id;
	window.location.href = "/Views/Customer/Edit.html";
}

function Details(id) {
	sessionStorage.customerdetailsid = id;
	window.location.href = "/Views/Customer/Details.html";
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
			"Are you sure you want to delete Customer [ " + id + " ]");
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

	gapi.client.customerendpoint
			.removeCustomer({
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
												'window.location.href = "/Views/Customer/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/Customer/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
