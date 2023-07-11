/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.lendinggroupendpoint = fanikiwa.lendinggroupendpoint || {};
fanikiwa.lendinggroupendpoint.lendinggroups = fanikiwa.lendinggroupendpoint.lendinggroups
		|| {};

fanikiwa.lendinggroupendpoint.lendinggroups.LoadLendingGroups = function() {

	$('#listLendingGroupsResult').html('loading...');

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;

	gapi.client.lendinggroupendpoint.retrieveLendinggroupsByCreator({
		'email' : email
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp == false || resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listLendingGroupsResult').html(
								'You have no LendingGroups...');
					} else {
						buildTable(resp);
					}
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
fanikiwa.lendinggroupendpoint.lendinggroups.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.lendinggroupendpoint.lendinggroups.LoadLendingGroups();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('lendinggroupendpoint', 'v1', callback, apiRoot);

};

var lendinggroupTable = '';
var lendinggroupTree = '';
function buildTable(response) {

	lendinggroupTable = '';
	lendinggroupTree = '';

	populateLendingGroups(response);

	$("#listLendingGroupsResult").html(lendinggroupTable);

	$('#listLendingGroupsTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

	$('#treeLendingGroupsResult').jstree({
		"core" : {
			"themes" : {
				"variant" : "large"
			}
		},
		"checkbox" : {
			"keep_selected_style" : false
		},
		"plugins" : [ "themes", "html_data", "ui", "crrm", "sort" ],
		"plugins" : [ "themes", "json_data", "ui" ],
		"plugins" : [ "wholerow", "checkbox" ]
	}).on('loaded.jstree', function() {
		$(this).jstree('open_all');
	}).on('changed.jstree', function(e, data) {
		var i, j, r = [];
		for (i = 0, j = data.selected.length; i < j; i++) {
			r.push(data.instance.get_node(data.selected[i]).text);
		}
		$('#apiResults').html('Selected: ' + r.join(', '));
	});

	// populateLendingGroupsTree(response);
	createLendingGroupsTree(response);

	$("#treeLendingGroupsResult").html(lendinggroupTree);

}
function tree(data) {
	if (typeof (data) == 'object') {
		var ul = $('<ul>');
		for ( var i in data) {
			ul.append($('<li>').text(i).append(tree(data[i])));
		}
		return ul;
	} else {
		var textNode = document.createTextNode(' => ' + data);
		return textNode;
	}
}
function json_tree(data) {
	var json = "<ul>";

	for (var i = 0; i < data.length; ++i) {
		json = json + "<li>" + data[i].text;
		if (data[i].data.length) {
			json = json + json_tree(data[i].data);
		}
		json = json + "</li>";
	}
	return json + "</ul>";
}
// var data = [ {
// "id" : "1",
// "name" : "name_1",
// "parent_id" : "0",
// "depth" : "0"
// }, {
// "id" : "2",
// "name" : "name_2",
// "parent_id" : "0",
// "depth" : "0"
// }, {
// "id" : "3",
// "name" : "name_3",
// "parent_id" : "1",
// "depth" : "1"
// }, {
// "id" : "4",
// "name" : "name_4",
// "parent_id" : "3",
// "depth" : "2"
// } ];

function createLendingGroupsTree(resp) {
	if (!resp.code) {
		resp.items = resp.items || [];

		var data = resp.result.items;

		var initLevel = 0;
		var endMenu = getTreeNode("0");
		function getTreeNode(parentGroup) {
			return data.filter(function(node) {
				return (node.parentGroup === parentGroup);
			}).map(
					function(node) {
						var exists = data.some(function(childNode) {
							return childNode.parentGroup === node.groupName;
						});
						var subNode = (exists) ? '<ul>'
								+ getTreeNode(node.groupName).join('')
								+ '</ul>' : "";
						return '<li>' + node.groupName + subNode + '</li>';
					});
		}
		console.log('<ul>' + endMenu.join('') + '</ul>');
	}
}

function populateLendingGroups(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		lendinggroupTable += '<table id="listLendingGroupsTable">';
		lendinggroupTable += "<thead>";
		lendinggroupTable += "<tr>";
		lendinggroupTable += "<th>Name</th>";
		lendinggroupTable += "<th>Created On</th>";
		lendinggroupTable += "<th>Last Modified</th>";
		lendinggroupTable += "<th>Parent Group</th>";
		lendinggroupTable += "<th></th>";
		lendinggroupTable += "<th></th>";
		lendinggroupTable += "</tr>";
		lendinggroupTable += "</thead>";
		lendinggroupTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			lendinggroupTable += '<tr>';
			lendinggroupTable += '<td>' + resp.result.items[i].groupName
					+ '</td>';
			lendinggroupTable += '<td>' + resp.result.items[i].createdOn
					+ '</td>';
			lendinggroupTable += '<td>' + resp.result.items[i].lastModified
					+ '</td>';
			lendinggroupTable += '<td>' + resp.result.items[i].parentGroup
					+ '</td>';
			lendinggroupTable += '<td><a href="#" onclick="Create(' + "'"
					+ resp.result.items[i].groupName + "'"
					+ ')">Create</a> </td>';
			lendinggroupTable += '<td><a href="#" onclick="Delete(' + "'"
					+ resp.result.items[i].groupName + "'"
					+ ')">Delete</a> </td>';
			lendinggroupTable += "</tr>";
		}

		lendinggroupTable += "</tbody>";
		lendinggroupTable += "</table>";

	}
}

function populateLendingGroupsTree(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		lendinggroupTree += '<ul id="treelendinggroups">';

		for (var i = 0; i < resp.result.items.length; i++) {
			lendinggroupTree += '<li id="roottreenode">';
			lendinggroupTree += '<div id="div-texttreenode"><span id="texttreenode">'
					+ resp.result.items[i].groupName + '</span></div>';
			lendinggroupTree += '<div id="div-createtreenode"><span id="createtreenode"><a href="#" onclick="Create('
					+ "'"
					+ resp.result.items[i].groupName
					+ "'"
					+ ')">Create</a></span></div>';
			lendinggroupTree += '<div id="div-deletetreenode"><span id="deletetreenode"><a href="#" onclick="Delete('
					+ "'"
					+ resp.result.items[i].groupName
					+ "'"
					+ ')">Delete</a></span></div>';
			lendinggroupTree += "</li>";
		}

		lendinggroupTree += "</ul>";
	}

}

function Create(id) {
	sessionStorage.lendinggroupparentid = id;
	window.location.href = "/Views/LendingGroups/Create.html";
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
			"Are you sure you want to delete Lending Group [ " + id + " ]");
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

	gapi.client.lendinggroupendpoint
			.removeLendinggroup({
				'id' : id
			})
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
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
												'window.location.href = "/Views/LendingGroups/List.html";',
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
			.push('<li><div class="floatleft"><div><a href="/Views/LendingGroups/Create.html" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/LendingGroups/List.html" style="cursor: pointer;">My groups</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/ChangePassword.html" style="cursor: pointer;">Change Password</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/DeRegister.html" style="cursor: pointer;">Deregister</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
