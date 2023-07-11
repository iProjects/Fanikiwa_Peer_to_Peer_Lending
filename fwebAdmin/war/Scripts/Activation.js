/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.userprofileendpoint = fanikiwa.userprofileendpoint || {};
fanikiwa.userprofileendpoint.accountactivation = fanikiwa.userprofileendpoint.accountactivation
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';
fanikiwa.userprofileendpoint.requesttoken = function() {
	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Validate the entries
	var _email = document.getElementById('txtemail').value;
	var _telno = document.getElementById('txttelephone').value;
	
	if (_email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_telno.length == 0) {
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
	
	$('#apiResults').html('activating...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var activationDTO = {};
	activationDTO.activatedDate = new Date();
	activationDTO.email = _email;
	activationDTO.telno = _telno;
	activationDTO.activationMethod = 'Web';
	
	gapi.client.userprofileendpoint
	.requestToken(activationDTO)
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
										'window.location.href = "/Views/Account/Login.html";',
										1000);
					}
				} else {
					$('#errormessage').html(
							'operation failed! Please try again.');
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
	
}

fanikiwa.userprofileendpoint.accountactivation = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Validate the entries
	var _email = document.getElementById('txtemail').value;
	var _telno = document.getElementById('txttelephone').value;
	var _activationToken = document.getElementById('txtactivationToken').value;

	if (_email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_telno.length == 0) {
		errormsg += '<li>' + " Telephone cannot be null " + '</li>';
		error_free = false;
	}
	if (_activationToken.length == 0) {
		errormsg += '<li>' + " Activation Token cannot be null " + '</li>';
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

	$('#apiResults').html('activating...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var activationDTO = {};
	activationDTO.activatedDate = new Date();
	activationDTO.email = _email;
	activationDTO.telno = _telno;
	activationDTO.activationMethod = 'Web';
	activationDTO.token = _activationToken;

	gapi.client.userprofileendpoint
			.activate(activationDTO)
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
												'window.location.href = "/Views/Account/Login.html";',
												1000);
							}
						} else {
							$('#errormessage').html(
									'operation failed! Please try again.');
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
fanikiwa.userprofileendpoint.accountactivation.enableButtons = function() {
	$("#btnSubmit").removeAttr('style');
	$("#btnSubmit").removeAttr('disabled');
	$("#btnSubmit").val('Submit');
	var btnSubmit = document.querySelector('#btnSubmit');
	btnSubmit.addEventListener('click', function() {
		fanikiwa.userprofileendpoint.accountactivation();
	});


	$("#btnRequest").removeAttr('style');
	$("#btnRequest").removeAttr('disabled');
	$("#btnRequest").val('Request for a new Token');
	var btnRequest = document.querySelector('#btnRequest');
	btnRequest.addEventListener('click', function() {
		fanikiwa.userprofileendpoint.requesttoken();
	});


};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.userprofileendpoint.accountactivation.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.userprofileendpoint.accountactivation.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('userprofileendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/Index.html" style="cursor: pointer;">Home</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/Help.html" style="cursor: pointer;">Help</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/ContactUs.html" style="cursor: pointer;">Contact Us</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Home/About.html" style="cursor: pointer;">About</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
