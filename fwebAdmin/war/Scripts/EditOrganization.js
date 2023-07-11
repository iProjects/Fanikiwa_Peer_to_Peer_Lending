/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.organizationendpoint = fanikiwa.organizationendpoint || {};
fanikiwa.organizationendpoint.editorganization = fanikiwa.organizationendpoint.editorganization
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.organizationendpoint.editorganization = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _organizationID = sessionStorage.getItem('editorganizationid');
	var _address = document.getElementById('txtaddress').value;
	var _email = document.getElementById('txtemail').value;
	var _name = document.getElementById('txtname').value;

	if (_name.length == 0) {
		errormsg += '<li>' + " Name cannot be null " + '</li>';
		error_free = false;
	}
	if (_email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_address.length == 0) {
		errormsg += '<li>' + " Address cannot be null " + '</li>';
		error_free = false;
	}

	if (!error_free) {
		errormsg += "</ul>";
		$("#error-display-div").html(errormsg);
		$("#error-display-div").removeClass('displaynone');
		$("#error-display-div").addClass('displayblock');
		$("#error-display-div").show();
		return;
	} else {
		ClearException();
	}

	$('#apiResults').html('updating organization...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var organization = {};
	organization.organizationID = _organizationID;
	organization.address = _address;
	organization.email = _email;
	organization.name = _name;

	gapi.client.organizationendpoint
			.updateOrganization(organization)
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
												'window.location.href = "/Views/Organization/List.html";',
												1000);
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message);
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
 * Enables the button callbacks in the UI.
 */
fanikiwa.organizationendpoint.editorganization.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.organizationendpoint.editorganization();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.organizationendpoint.editorganization.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.organizationendpoint.editorganization.enableButtons();
			fanikiwa.organizationendpoint.editorganization.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('organizationendpoint', 'v1', callback, apiRoot);

};

fanikiwa.organizationendpoint.editorganization.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editorganizationid');
	gapi.client.organizationendpoint
			.retrieveOrganization({
				'id' : id
			})
			.execute(
					function(resp) {
						console.log(resp);
						if (!resp.code) {
							if (resp.result.success == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								fanikiwa.organizationendpoint.editorganization
										.populateControls(resp.result.clientToken);
								$('#successmessage').html('');
								$('#errormessage').html('');
								$('#apiResults').html('');
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
										+ reason.result.error.message
												.toString());
						$('#successmessage').html('');
						$('#apiResults').html('');
					});
}

fanikiwa.organizationendpoint.editorganization.populateControls = function(
		organization) {

	if (organization.organizationID != undefined)
		document.getElementById('txtorganizationID').value = organization.organizationID;
	if (organization.name != undefined)
		document.getElementById('txtname').value = organization.name;
	if (organization.email != undefined)
		document.getElementById('txtemail').value = organization.email;
	if (organization.address != undefined)
		document.getElementById('txtaddress').value = organization.address;

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
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
