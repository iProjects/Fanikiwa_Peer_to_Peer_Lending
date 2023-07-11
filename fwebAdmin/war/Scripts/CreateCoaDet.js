/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.coadetendpoint = fanikiwa.coadetendpoint || {};
fanikiwa.coadetendpoint.createcoadet = fanikiwa.coadetendpoint.createcoadet
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.coadetendpoint.createcoadet = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _coaid = sessionStorage.getItem('coaid');
	var _shortCode = document.getElementById('txtshortCode').value;
	var _description = document.getElementById('txtdescription').value;
	var _coaLevel = document.getElementById('txtcoaLevel').value;
	var _rorder = document.getElementById('txtrorder').value;

	if (_shortCode.length == 0) {
		errormsg += '<li>' + " Short Code cannot be null " + '</li>';
		error_free = false;
	}
	if (_description.length == 0) {
		errormsg += '<li>' + " Description cannot be null " + '</li>';
		error_free = false;
	}
	if (_coaLevel.length == 0) {
		errormsg += '<li>' + " Coa Level cannot be null " + '</li>';
		error_free = false;
	}
	if (_coaLevel.length != 0 && !isNumber(_coaLevel)) {
		errormsg += '<li>' + " Coa Level must be a number " + '</li>';
		error_free = false;
	}
	if (_rorder.length == 0) {
		errormsg += '<li>' + " ROrder cannot be null " + '</li>';
		error_free = false;
	}
	if (_rorder.length != 0 && !isNumber(_rorder)) {
		errormsg += '<li>' + " ROrder must be a number " + '</li>';
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

	$('#apiResults').html('creating chart of account detail...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var coadetDTO = {};
	coadetDTO.coa = _coaid; 
	coadetDTO.description = _description;
	coadetDTO.rorder = _rorder;
	coadetDTO.shortCode = _shortCode;
	coadetDTO.coaLevel = _coaLevel;

	gapi.client.coadetendpoint
			.createCoadet(coadetDTO)
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
												'window.location.href = "/Views/CoaDet/List.html";',
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
fanikiwa.coadetendpoint.createcoadet.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.coadetendpoint.createcoadet();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.coadetendpoint.createcoadet.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.coadetendpoint.createcoadet.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/CoaDet/List.html" style="cursor: pointer;" >Chart of Account Details</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
