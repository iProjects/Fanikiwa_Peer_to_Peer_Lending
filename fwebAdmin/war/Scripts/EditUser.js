/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.userprofileendpoint = fanikiwa.userprofileendpoint || {};
fanikiwa.userprofileendpoint.edituser = fanikiwa.userprofileendpoint.edituser
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.userprofileendpoint.edituser = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _userId = sessionStorage.getItem('edituserprofileid');
	var _pwd = document.getElementById('txtpwd').value;
	var _telephone = document.getElementById('txttelephone').value;
	var _status = document.getElementById('cbostatus').value;
	var _userType = document.getElementById('cbouserType').value;

	if (_pwd.length == 0) {
		errormsg += '<li>' + " Password cannot be null " + '</li>';
		error_free = false;
	}
	if (_telephone.length == 0) {
		errormsg += '<li>' + " Telephone cannot be null " + '</li>';
		error_free = false;
	}
	if (_status.length == 0 || _status == -1) {
		errormsg += '<li>' + " Select Status " + '</li>';
		error_free = false;
	}
	if (_userType.length == 0 || _userType == -1) {
		errormsg += '<li>' + " Select User Type " + '</li>';
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

	$('#apiResults').html('creating user...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var userprofile = {};
	userprofile.userId = _userId;
	userprofile.pwd = _pwd;
	userprofile.telephone = _telephone;
	userprofile.status = _status;
	userprofile.userType = _userType;
	userprofile.role = _userType;

	gapi.client.userprofileendpoint
			.updateUserprofile(userprofile)
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
												'window.location.href = "/Views/Userprofile/List.html";',
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
fanikiwa.userprofileendpoint.edituser.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.userprofileendpoint.edituser();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.userprofileendpoint.edituser.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.userprofileendpoint.edituser.populateStatus();
			fanikiwa.userprofileendpoint.edituser.populateUserType();
			fanikiwa.userprofileendpoint.edituser.enableButtons();
			fanikiwa.userprofileendpoint.edituser.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('userprofileendpoint', 'v1', callback, apiRoot);

};

fanikiwa.userprofileendpoint.edituser.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('edituserprofileid');
	gapi.client.userprofileendpoint
			.retrieveUser({
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
								fanikiwa.userprofileendpoint.edituser
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

fanikiwa.userprofileendpoint.edituser.populateControls = function(userprofile) {

	if (userprofile.userId != undefined)
		document.getElementById('txtuserId').value = userprofile.userId;
	if (userprofile.pwd != undefined)
		document.getElementById('txtpwd').value = userprofile.pwd;
	if (userprofile.telephone != undefined)
		document.getElementById('txttelephone').value = userprofile.telephone;
	if (userprofile.status != undefined)
		document.getElementById('cbostatus').value = userprofile.status;
	if (userprofile.userType != undefined) {
		document.getElementById('cbouserType').value = userprofile.userType;
		if (userprofile.userType == 'Member') {
			document.getElementById('cbouserType').disabled = true;
		}
	}
};

fanikiwa.userprofileendpoint.edituser.populateStatus = function() {
	var statusarray = [ {
		id : "Active",
		description : "Active"
	}, {
		id : "Open",
		description : "Open"
	}, {
		id : "Disabled",
		description : "Disabled"
	}, {
		id : "Closed",
		description : "Closed"
	} ];
	var statusoptions = '';
	for (var i = 0; i < statusarray.length; i++) {
		statusoptions += '<option value="' + statusarray[i].id + '">'
				+ statusarray[i].description + '</option>';
	}
	$("#cbostatus").append(statusoptions);
};

fanikiwa.userprofileendpoint.edituser.populateUserType = function() {
	var userTypearray = [ {
		id : "Admin",
		description : "Admin"
	}, {
		id : "System",
		description : "System"
	}, {
		id : "FrontEndUser",
		description : "FrontEndUser"
	}, {
		id : "BackEndUser",
		description : "BackEndUser"
	}, {
		id : "Member",
		description : "Member"
	} ];
	var userTypeoptions = '';
	for (var i = 0; i < userTypearray.length; i++) {
		userTypeoptions += '<option value="' + userTypearray[i].id + '">'
				+ userTypearray[i].description + '</option>';
	}
	$("#cbouserType").append(userTypeoptions);
};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}