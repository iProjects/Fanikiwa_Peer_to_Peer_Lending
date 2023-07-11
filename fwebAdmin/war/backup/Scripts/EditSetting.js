/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.settingsendpoint = fanikiwa.settingsendpoint || {};
fanikiwa.settingsendpoint.editsetting = fanikiwa.settingsendpoint.editsetting
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.settingsendpoint.editsetting = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _property = sessionStorage.getItem('editsettingid');
	var _value = document.getElementById('txtvalue').value;
	var _groupName = document.getElementById('txtgroupName').value;

	if (_property.length == 0) {
		errormsg += '<li>' + " Account Name cannot be null " + '</li>';
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

	$('#apiResults').html('updating setting...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var setting = {};
	setting.property = _property;
	setting.value = _value;
	setting.groupName = _groupName;

	gapi.client.settingsendpoint
			.updateSettings(setting)
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
fanikiwa.settingsendpoint.editsetting.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.settingsendpoint.editsetting();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.settingsendpoint.editsetting.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.settingsendpoint.editsetting.enableButtons();
			fanikiwa.settingsendpoint.editsetting.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('settingsendpoint', 'v1', callback, apiRoot);

};

fanikiwa.settingsendpoint.editsetting.initializeControls = function() {
	$('#apiResults').html('loading...');
	var key = sessionStorage.getItem('editsettingid');
	gapi.client.settingsendpoint
			.retrieveSettingsByKey({
				'key' : key
			})
			.execute(
					function(resp) {
						console.log(resp);
						if (!resp.code) {
							if (resp.result.result == false) {
								$('#errormessage').html(
										'operation failed! Error...<br/>'
												+ resp.result.resultMessage
														.toString());
								$('#successmessage').html('');
								$('#apiResults').html('');
							} else {
								fanikiwa.settingsendpoint.editsetting
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

fanikiwa.settingsendpoint.editsetting.populateControls = function(setting) {

	if (setting.property != undefined)
		document.getElementById('txtproperty').value = setting.property;
	if (setting.value != undefined)
		document.getElementById('txtvalue').value = setting.value;
	if (setting.groupName != undefined)
		document.getElementById('txtgroupName').value = setting.groupName;
};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}