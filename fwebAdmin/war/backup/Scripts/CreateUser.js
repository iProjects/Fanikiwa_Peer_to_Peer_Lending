/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.userprofileendpoint = fanikiwa.userprofileendpoint || {};
fanikiwa.userprofileendpoint.createuser = fanikiwa.userprofileendpoint.createuser
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.userprofileendpoint.createuser = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _email = document.getElementById('txtemail').value;
	var _pwd = document.getElementById('txtpwd').value;
	var _surname = document.getElementById('txtsurname').value;
	var _telephone = document.getElementById('txttelephone').value;
	var _nationalID = document.getElementById('txtnationalID').value;
	var _status = document.getElementById('cbostatus').value;
	var _informBy = document.getElementById('cboinformBy').value;
	var _userType = document.getElementById('cbouserType').value;

	if (_email.length != 0) {
		var validatedemail = gapi.client.memberendpoint.isEmailValid({
			'email' : _email
		}).execute(function(resp) {
			if (!resp.code) {
				if (resp == false || resp.result.result == false) {
					sessionStorage.isemailvalidinregister = false;
				} else {
					sessionStorage.isemailvalidinregister = true;
				}
			} else {
				sessionStorage.isemailvalidinregister = false;
			}
		}, function(reason) {
			console.log('Error: ' + reason.result.error.message);
			sessionStorage.isemailvalidinregister = false;
		});
	}

	if (_email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}

	if (_email.length != 0 && sessionStorage.isemailvalidinregister === "false") {
		errormsg += '<li>' + " Validation failed! Please check Email.<br/> "
				+ "Valid format is user@domain.com" + '</li>';
		error_free = false;
	}
	if (_pwd.length == 0) {
		errormsg += '<li>' + " Password cannot be null " + '</li>';
		error_free = false;
	}
	if (_surname.length == 0) {
		errormsg += '<li>' + " Surname cannot be null " + '</li>';
		error_free = false;
	}
	if (_telephone.length == 0) {
		errormsg += '<li>' + " Telephone cannot be null " + '</li>';
		error_free = false;
	}
	if (_nationalID.length == 0) {
		errormsg += '<li>' + " National ID cannot be null " + '</li>';
		error_free = false;
	}
	if (_status.length == 0 || _status == -1) {
		errormsg += '<li>' + " Select Status " + '</li>';
		error_free = false;
	}
	if (_informBy.length == 0 || _informBy == -1) {
		errormsg += '<li>' + " Select Inform By " + '</li>';
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
	userprofile.email = _email;
	userprofile.pwd = _pwd;
	userprofile.surname = _surname;
	userprofile.telephone = _telephone;
	userprofile.nationalID = _nationalID;
	userprofile.status = _status;
	userprofile.informBy = _informBy;
	userprofile.userType = _userType;

	gapi.client.userprofileendpoint
			.createUserprofile(userprofile)
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
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
							$('#errormessage').html(
									'operation failed! Please try again.');
							$('#successmessage').html('');
							$('#apiResults').html('');
						}

					}, function(reason) {
						console.log('Error: ' + reason.result.error.message);
					});
};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.userprofileendpoint.createuser.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.userprofileendpoint.createuser();
	});
	var txtemail = document.querySelector('#txtemail');
	txtemail.addEventListener('change', function() {
		var email = document.getElementById('txtemail').value;
		fanikiwa.userprofileendpoint.isEmailValid(email);
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.userprofileendpoint.createuser.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.userprofileendpoint.createuser.populateStatus();
			fanikiwa.userprofileendpoint.createuser.populateInformBy();
			fanikiwa.userprofileendpoint.createuser.populateUserType();
			fanikiwa.userprofileendpoint.createuser.enableButtons();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
	gapi.client.load('userprofileendpoint', 'v1', callback, apiRoot);
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

};

fanikiwa.userprofileendpoint.isEmailValid = function(email) {
	$('#apiResults').html('authenticating email...');
	gapi.client.memberendpoint
			.isEmailValid({
				'email' : email
			})
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp.result.result == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
								sessionStorage.isemailvalidinregister = false;
							} else {
								$('#successmessage').html(
										'operation successful... <br/>'
												+ resp.result.resultMessage
														.toString());
								$('#errormessage').html('');
								$('#apiResults').html('');
								sessionStorage.isemailvalidinregister = true;
								window.setTimeout(
										'$("#successmessage").html("");', 1000);
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'operation failed! Error...<br/>'
											+ resp.error.message.toString());
							$('#successmessage').html('');
							$('#apiResults').html('');
							sessionStorage.isemailvalidinregister = false;
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
						sessionStorage.isemailvalidinregister = false;
					});

};

fanikiwa.userprofileendpoint.createuser.populateStatus = function() {
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

fanikiwa.userprofileendpoint.createuser.populateInformBy = function() {
	var informByarray = [ {
		id : "SMS",
		description : "SMS"
	}, {
		id : "EMAIL",
		description : "EMAIL"
	} ];
	var informByoptions = '';
	for (var i = 0; i < informByarray.length; i++) {
		informByoptions += '<option value="' + informByarray[i].id + '">'
				+ informByarray[i].description + '</option>';
	}
	$("#cboinformBy").append(informByoptions);
};

fanikiwa.userprofileendpoint.createuser.populateUserType = function() {
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