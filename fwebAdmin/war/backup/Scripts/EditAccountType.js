/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accounttypeendpoint = fanikiwa.accounttypeendpoint || {};
fanikiwa.accounttypeendpoint.editaccounttype = fanikiwa.accounttypeendpoint.editaccounttype
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accounttypeendpoint.editaccounttype = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _id = sessionStorage.getItem('editaccounttypeid');
	var _description = document.getElementById('txtdescription').value;
	var _shortCode = document.getElementById('txtshortCode').value;

	if (_shortCode.length == 0) {
		errormsg += '<li>' + " ShortCode cannot be null " + '</li>';
		error_free = false;
	}
	if (_description.length == 0) {
		errormsg += '<li>' + " Description cannot be null " + '</li>';
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

	$('#apiResults').html('updating account type...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var accounttype = {};
	accounttype.id = _id;
	accounttype.description = _description;
	accounttype.shortCode = _shortCode;

	gapi.client.accounttypeendpoint
			.updateAccountType(accounttype)
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
												'window.location.href = "/Views/AccountType/List.html";',
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
fanikiwa.accounttypeendpoint.editaccounttype.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnUpdate = document.querySelector('#btnUpdate');
	btnUpdate.addEventListener('click', function() {
		fanikiwa.accounttypeendpoint.editaccounttype();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.accounttypeendpoint.editaccounttype.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accounttypeendpoint.editaccounttype.enableButtons();
			fanikiwa.accounttypeendpoint.editaccounttype.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('accounttypeendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accounttypeendpoint.editaccounttype.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('editaccounttypeid');
	gapi.client.accounttypeendpoint.retrieveAccountType({
		'id' : id
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
								fanikiwa.accounttypeendpoint.editaccounttype
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

fanikiwa.accounttypeendpoint.editaccounttype.populateControls = function(
		accounttype) {

	if (accounttype.shortCode != undefined)
		document.getElementById('txtshortCode').value = accounttype.shortCode;
	if (accounttype.description != undefined)
		document.getElementById('txtdescription').value = accounttype.description;

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}