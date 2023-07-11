/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.organizationendpoint = fanikiwa.organizationendpoint || {};
fanikiwa.organizationendpoint.listorganizations = fanikiwa.organizationendpoint.listorganizations || {};

fanikiwa.organizationendpoint.listorganizations.LoadOrganizations = function() {

	$('#listOrganizationsResult').html('loading...');

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.organizationendpoint.listOrganization().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listOrganizationsResult').html('There are no Organizations...');
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
}

function populateOrganizations(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		organizationTable += '<table id="listOrganizationsTable">';
		organizationTable += "<thead>";
		organizationTable += "<tr>";
		organizationTable += "<th>Name</th>";
		organizationTable += "<th>Email</th>";
		organizationTable += "<th>Address</th>";  
		organizationTable += "</tr>";
		organizationTable += "</thead>";
		organizationTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			organizationTable += '<tr>';
			organizationTable += '<td>' + resp.result.items[i].name + '</td>';
			organizationTable += '<td>' + resp.result.items[i].email + '</td>';
			organizationTable += '<td>' + resp.result.items[i].address + '</td>';  
			organizationTable += "</tr>";
		}

		organizationTable += "</tbody>";
		organizationTable += "</table>";

	}
}

function OrganizationDetails(id) {
	sessionStorage.organizationdetailsid = id;
	window.location.href = "/Views/Organization/Details.html";
}
