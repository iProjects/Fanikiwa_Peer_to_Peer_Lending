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

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.customerendpoint.listCustomer().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listCustomersResult').html('There are no Customers...');
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
}

function populateCustomers(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		customerTable += '<table id="listCustomersTable">';
		customerTable += "<thead>";
		customerTable += "<tr>";
		customerTable += "<th>Customer Id</th>";
		customerTable += "<th>Name</th>";
		customerTable += "<th>Telephone</th>";
		customerTable += "<th>Email</th>";
		customerTable += "<th>Address</th>";
		customerTable += "<th>Created Date</th>";
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
			customerTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
			customerTable += "</tr>";
		}

		customerTable += "</tbody>";
		customerTable += "</table>";

	}
}

function Details(id) {
	sessionStorage.customerdetailsid = id;
	window.location.href = "/Views/Customer/Details.html";
}
