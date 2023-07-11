/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.memberendpoint = fanikiwa.memberendpoint || {};
fanikiwa.memberendpoint.listmembers = fanikiwa.memberendpoint.listmembers || {};

fanikiwa.memberendpoint.listmembers.LoadMembers = function() {

	$('#listMembersResult').html('loading...');

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.memberendpoint.listMember().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listMembersResult').html('There are no Members...');
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
fanikiwa.memberendpoint.listmembers.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.memberendpoint.listmembers.LoadMembers();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

};

var memberTable = '';
function buildTable(response) {

	memberTable = '';

	populateMembers(response);

	$("#listMembersResult").html(memberTable);
}

function populateMembers(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		memberTable += '<table id="listMembersTable">';
		memberTable += "<thead>";
		memberTable += "<tr>";
		memberTable += "<th>Member Id</th>";
		memberTable += "<th>Email</th>";
		memberTable += "<th>Telephone</th>";
		memberTable += "<th>Surname</th>";
		memberTable += "<th>Other Names</th>";
		memberTable += "<th>National ID</th>";
		memberTable += "<th></th>";
		memberTable += "</tr>";
		memberTable += "</thead>";
		memberTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			memberTable += '<tr>';
			memberTable += '<td>' + resp.result.items[i].memberId + '</td>';
			memberTable += '<td>' + resp.result.items[i].email + '</td>';
			memberTable += '<td>' + resp.result.items[i].telephone + '</td>';
			memberTable += '<td>' + resp.result.items[i].surname + '</td>';
			memberTable += '<td>' + resp.result.items[i].otherNames + '</td>';
			memberTable += '<td>' + resp.result.items[i].nationalID + '</td>';
			memberTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
			memberTable += "</tr>";
		}

		memberTable += "</tbody>";
		memberTable += "</table>";

	}
}

function Details(id) {
	sessionStorage.memberdetailsid = id;
	window.location.href = "/Views/Member/Details.html";
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Customer/List.html" style="cursor: pointer;" >Customers</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Userprofile/List.html"style="cursor: pointer;" >Users</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/MailingGroup/List.html"style="cursor: pointer;" >Mailing Groups</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
