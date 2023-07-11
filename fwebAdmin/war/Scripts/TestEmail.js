/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.informdbendpoint = fanikiwa.informdbendpoint || {};
fanikiwa.informdbendpoint.testemail = fanikiwa.informdbendpoint.testemail || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.informdbendpoint.testemail = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Validate the entries
	var _Email = document.getElementById('txtEmail').value;
	var _Subject = document.getElementById('txtSubject').value;
	var _Body = document.getElementById('txtBody').value;

	if (_Email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_Subject.length == 0) {
		errormsg += '<li>' + " Subject cannot be null " + '</li>';
		error_free = false;
	}
	if (_Body.length == 0) {
		errormsg += '<li>' + " Body cannot be null " + '</li>';
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

	$('#apiResults').html('sending email...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	gapi.client.informdbendpoint.sendMail({
		'to' : _Email,
		'subject' : _Subject,
		'body' : _Body
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
fanikiwa.informdbendpoint.testemail.enableButtons = function() {
	$("#btnTest").removeAttr('style');
	$("#btnTest").removeAttr('disabled');
	$("#btnTest").val('Submit');
	var btnUpdate = document.querySelector('#btnTest');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.informdbendpoint.testemail();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.informdbendpoint.testemail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.informdbendpoint.testemail.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('informdbendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}