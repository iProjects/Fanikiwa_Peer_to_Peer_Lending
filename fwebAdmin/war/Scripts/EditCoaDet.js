/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.coadetendpoint = fanikiwa.coadetendpoint || {};
fanikiwa.coadetendpoint.editcoadet = fanikiwa.coadetendpoint.editcoadet || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.coadetendpoint.editcoadet = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _id = sessionStorage.getItem('editcoadetid');
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
	if (_rorder.length == 0) {
		errormsg += '<li>' + " ROrder cannot be null " + '</li>';
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
	coadetDTO.id = _id;
	coadetDTO.coa = _coa;
	coadetDTO.coaLevel = _coaLevel;
	coadetDTO.description = _description;
	coadetDTO.rorder = _rorder;
	coadetDTO.shortCode = _shortCode;

	gapi.client.coadetendpoint
			.updateCoadet(coadetDTO)
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
fanikiwa.coadetendpoint.editcoadet.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.coadetendpoint.editcoadet();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.coadetendpoint.editcoadet.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.coadetendpoint.editcoadet.enableButtons();
			fanikiwa.coadetendpoint.editcoadet.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('coadetendpoint', 'v1', callback, apiRoot);

};

fanikiwa.coadetendpoint.editcoadet.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editcoadetid');
	gapi.client.coadetendpoint
			.retrieveCoadet({
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
								fanikiwa.coadetendpoint.editcoadet
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

fanikiwa.coadetendpoint.editcoadet.populateControls = function(coadet) {

	if (coadet.shortCode != undefined)
		document.getElementById('txtshortCode').value = coadet.shortCode;
	if (coadet.description != undefined)
		document.getElementById('txtdescription').value = coadet.description;
	if (coadet.coaLevel != undefined)
		document.getElementById('txtcoaLevel').value = coadet.coaLevel;
	if (coadet.rorder != undefined)
		document.getElementById('txtrorder').value = coadet.rorder;

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/CoaDet/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/CoaDet/List.html" style="cursor: pointer;" >Chart of Account Details</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
