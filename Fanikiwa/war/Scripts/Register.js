/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.memberendpoint = fanikiwa.memberendpoint || {};
fanikiwa.memberendpoint.register = fanikiwa.memberendpoint.register || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.memberendpoint.register = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _Email = document.getElementById('txtEmail').value;
	var _Pwd = document.getElementById('txtPassword').value;
	var _Surname = document.getElementById('txtSurname').value;
	var _Telephone = document.getElementById('txtTelephone').value;

	if (_Email.length != 0) {
		var validatedemail = gapi.client.memberendpoint.isEmailValid({
			'email' : _Email
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

	if (_Email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_Email.length != 0 && sessionStorage.isemailvalidinregister === "false") {
		errormsg += '<li>' + " Validation failed! Please check Email.<br/> "
				+ "Valid format is user@domain.com" + '</li>';
		error_free = false;
	}
	if (_Pwd.length == 0) {
		errormsg += '<li>' + " Password cannot be null " + '</li>';
		error_free = false;
	}
	if (_Surname.length == 0) {
		errormsg += '<li>' + " Surname cannot be null " + '</li>';
		error_free = false;
	}
	if (_Telephone.length == 0) {
		errormsg += '<li>' + " Telephone cannot be null " + '</li>';
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

	$('#apiResults').html('registering...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var userDTO = {};
	userDTO.email = _Email;
	userDTO.pwd = _Pwd;
	userDTO.surname = _Surname;
	userDTO.telephone = _Telephone;
	userDTO.userType = 'Member';
	userDTO.status = 'New';
	userDTO.role = userDTO.userType;
	userDTO.registrationMethod = 'Web';

	gapi.client.memberendpoint
			.register(userDTO)
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp.result.success == false) {
								$('#errormessage').html(
										'Operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								$('#successmessage').html(
										'Operation successful... <br/>'
												+ resp.result.resultMessage
														.toString());
								$('#errormessage').html('');
								$('#apiResults').html('');
								sessionStorage.clear();
								window
										.setTimeout(
												'window.location.href = "/Views/Account/Login.html";',
												0);
							}
						} else {
							console.log('Error: ' + resp.error.message);
							$('#errormessage').html(
									'Operation failed! Error...<br/>'
											+ resp.error.message.toString());
							$('#successmessage').html('');
							$('#apiResults').html('');
						}

					},
					function(reason) {
						console.log('Error: ' + reason.result.error.message);
						$('#errormessage').html(
								'Operation failed! Error...<br/>'
										+ reason.result.error.message
												.toString());
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
fanikiwa.memberendpoint.register.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.memberendpoint.register.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.memberendpoint.register.enableButtons = function() {
	$("#btnRegister").removeAttr('style');
	$("#btnRegister").removeAttr('disabled');
	$("#btnRegister").val('Register');
	var btnRegister = document.querySelector('#btnRegister');
	btnRegister.addEventListener('click', function() {
		fanikiwa.memberendpoint.register();
	});
	var txtEmail = document.querySelector('#txtEmail');
	txtEmail.addEventListener('change', function() {
		var email = document.getElementById('txtEmail').value;
		fanikiwa.memberendpoint.isEmailValid(email);
	});
};

fanikiwa.memberendpoint.isEmailValid = function(email) {
	$('#apiResults').html('authenticating email...');
	gapi.client.memberendpoint
			.isEmailValid({
				'email' : email
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

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}
