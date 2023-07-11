/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.settingsendpoint = fanikiwa.settingsendpoint || {};
fanikiwa.settingsendpoint.createsetting = fanikiwa.settingsendpoint.createsetting
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.settingsendpoint.createsetting = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _property = document.getElementById('txtproperty').value;
	var _value = document.getElementById('txtvalue').value;
	var _groupName = document.getElementById('txtgroupName').value;

	if (_property.length == 0) {
		errormsg += '<li>' + " Property cannot be null " + '</li>';
		error_free = false;
	}
	if (_value.length == 0) {
		errormsg += '<li>' + " Value cannot be null " + '</li>';
		error_free = false;
	}
	if (_groupName.length == 0) {
		errormsg += '<li>' + " Group Name cannot be null " + '</li>';
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

	$('#apiResults').html('creating setting...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var setting = {};
	setting.property = _property;
	setting.groupName = _groupName;
	setting.value = _value;

	gapi.client.settingsendpoint
			.insertSettings(setting)
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
												'window.location.href = "/Views/Setting/List.html";',
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
fanikiwa.settingsendpoint.createsetting.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.settingsendpoint.createsetting();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.settingsendpoint.createsetting.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.settingsendpoint.createsetting.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('settingsendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}