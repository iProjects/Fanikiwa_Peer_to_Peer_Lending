/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.coaendpoint = fanikiwa.coaendpoint || {};
fanikiwa.coaendpoint.createcoa = fanikiwa.coaendpoint.createcoa
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.coaendpoint.createcoa = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _description = document.getElementById('txtdescription').value;
  
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

	$('#apiResults').html('creating chart of account...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var coa = {};
	coa.description = _description;	 

	gapi.client.coaendpoint
			.insertCoa(coa)
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
												'window.location.href = "/Views/Coa/List.html";',
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

					}, function(reason) {
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
fanikiwa.coaendpoint.createcoa.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.coaendpoint.createcoa();
	}); 
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.coaendpoint.createcoa.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.coaendpoint.createcoa.enableButtons(); 
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('coaendpoint', 'v1', callback, apiRoot); 

};
 
function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}