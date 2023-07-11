/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.userprofileendpoint = fanikiwa.userprofileendpoint || {};
fanikiwa.userprofileendpoint.listuserprofiles = fanikiwa.userprofileendpoint.listuserprofiles
		|| {};

fanikiwa.userprofileendpoint.listuserprofiles.LoadUserprofiles = function() {

	$('#listUserprofilesResult').html('loading...');

	gapi.client.userprofileendpoint.listUserprofile().execute(function(resp) {
		console.log('response =>> ' + resp);
		if (!resp.code) {
			if (resp.result.items == undefined || resp.result.items == null) {
				$('#listUserprofilesResult').html('There are no Users...');
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
fanikiwa.userprofileendpoint.listuserprofiles.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.userprofileendpoint.listuserprofiles.LoadUserprofiles();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('userprofileendpoint', 'v1', callback, apiRoot);

};

var userprofileTable = '';
function buildTable(response) {

	userprofileTable = '';

	populateUserprofiles(response);

	$("#listUserprofilesResult").html(userprofileTable);
}

function populateUserprofiles(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		userprofileTable += '<table id="listUserprofilesTable">';
		userprofileTable += "<thead>";
		userprofileTable += "<tr>";
		userprofileTable += "<th>User Id</th>";
		userprofileTable += "<th>Telephone</th>";
		userprofileTable += "<th>Status</th>";
		userprofileTable += "<th>User Type</th>";
		userprofileTable += "<th>Created Date</th>";
		userprofileTable += "<th>Last Login Date</th>";
		userprofileTable += "<th></th>";
		userprofileTable += "<th></th>";
		userprofileTable += "</tr>";
		userprofileTable += "</thead>";
		userprofileTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			userprofileTable += '<tr>';
			userprofileTable += '<td>' + resp.result.items[i].userId + '</td>';
			userprofileTable += '<td>' + resp.result.items[i].telephone
					+ '</td>';
			userprofileTable += '<td>' + resp.result.items[i].status + '</td>';
			userprofileTable += '<td>' + resp.result.items[i].userType
					+ '</td>';
			userprofileTable += '<td>'
					+ formatDate(resp.result.items[i].createDate) + '</td>';
			userprofileTable += '<td>'
					+ formatDate(resp.result.items[i].lastLoginDate) + '</td>';
			userprofileTable += '<td><a href="#" onclick="Edit('
					+ resp.result.items[i].userId + ')">Edit</a> </td>';
			userprofileTable += '<td><a href="#" onclick="Delete('
				+ resp.result.items[i].userId + ')">Delete</a> </td>';
			userprofileTable += "</tr>";
		}

		userprofileTable += "</tbody>";
		userprofileTable += "</table>";

	}
}

function Edit(id) {
	sessionStorage.edituserprofileid = id;
	window.location.href = "/Views/Userprofile/Edit.html";
}

function Delete(id) {

	$('#apiResults').html('processing...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.userprofileendpoint
			.removeUserprofile({
				'id' : id
			})
			.execute(
					function(resp) {
						if (!resp.code) {
							if (resp.result.result == false) {
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
												'window.location.href = "/Views/Userprofile/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/Userprofile/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
