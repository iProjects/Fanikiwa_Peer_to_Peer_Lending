/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.tiereddetendpoint = fanikiwa.tiereddetendpoint || {};
fanikiwa.tiereddetendpoint.edittiereddet = fanikiwa.tiereddetendpoint.edittiereddet
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.tiereddetendpoint.edittiereddet = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _id = sessionStorage.getItem('edittiereddetid');
	var _absolute = document.getElementById('chkabsolute').checked;
	var _max = document.getElementById('txtmax').value;
	var _min = document.getElementById('txtmin').value;
	var _rate = document.getElementById('txtrate').value;
	var _tieredID = sessionStorage.getItem('tieredtableid');

	if (_max.length == 0) {
		errormsg += '<li>' + " Max cannot be null " + '</li>';
		error_free = false;
	}
	if (_max.length != 0 && !isNumber(_max)) {
		errormsg += '<li>' + " Max must be a number " + '</li>';
		error_free = false;
	}
	if (_min.length == 0) {
		errormsg += '<li>' + " Min cannot be null " + '</li>';
		error_free = false;
	}
	if (_min.length != 0 && !isNumber(_min)) {
		errormsg += '<li>' + " Min must be a number " + '</li>';
		error_free = false;
	}
	if (_rate.length == 0) {
		errormsg += '<li>' + " Rate cannot be null " + '</li>';
		error_free = false;
	}
	if (_rate.length != 0 && !isNumber(_rate)) {
		errormsg += '<li>' + " Rate must be a number " + '</li>';
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

	$('#apiResults').html('updating tiered detail...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var tiereddet = {};
	tiereddet.id = _id;
	tiereddet.absolute = _absolute;
	tiereddet.max = _max;
	tiereddet.min = _min;
	tiereddet.rate = _rate;
	tiereddet.tieredID = _tieredID;

	gapi.client.tiereddetendpoint
			.updateTieredDet(tiereddet)
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
												'window.location.href = "/Views/TieredDet/List.html";',
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
fanikiwa.tiereddetendpoint.edittiereddet.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.tiereddetendpoint.edittiereddet();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.tiereddetendpoint.edittiereddet.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.tiereddetendpoint.edittiereddet.enableButtons();
			fanikiwa.tiereddetendpoint.edittiereddet.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('tiereddetendpoint', 'v1', callback, apiRoot);

};

fanikiwa.tiereddetendpoint.edittiereddet.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('edittiereddetid');
	gapi.client.tiereddetendpoint
			.retrieveTieredDet({
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
								fanikiwa.tiereddetendpoint.edittiereddet
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

fanikiwa.tiereddetendpoint.edittiereddet.populateControls = function(
		tieredtable) {

	if (tieredtable.id != undefined)
		document.getElementById('txtid').value = tieredtable.id;
	if (tieredtable.absolute != undefined)
		document.getElementById('chkabsolute').checked = tieredtable.absolute;
	if (tieredtable.max != undefined)
		document.getElementById('txtmax').value = tieredtable.max;
	if (tieredtable.min != undefined)
		document.getElementById('txtmin').value = tieredtable.min;
	if (tieredtable.rate != undefined)
		document.getElementById('txtrate').value = tieredtable.rate;

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/Create.html" style="cursor: pointer;" >Create</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Tieredtable/List.html" style="cursor: pointer;" >Tiered Tables</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
