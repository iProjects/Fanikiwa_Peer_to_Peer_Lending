/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.post = fanikiwa.accountendpoint.post || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.post = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');
	
	// Validate the entries
	var _transactionType = document.getElementById('cbotransactionType').value;
	var _drAccount = document.getElementById('txtdebitAccount').value;
	var _crAccount = document.getElementById('txtcreditAccount').value;
	var _amount = document.getElementById('txtamount').value;
	var _narrative = document.getElementById('txtnarrative').value;

	if (_transactionType.length == 0) {
		errormsg += '<li>' + " Select Transaction Type " + '</li>';
		error_free = false;
	}
	if (_debitAccount.length == 0) {
		errormsg += '<li>' + " Debit Account cannot be null " + '</li>';
		error_free = false;
	}
	if (_creditAccount.length == 0) {
		errormsg += '<li>' + " Credit Account cannot be null " + '</li>';
		error_free = false;
	}
	if (_amount.length == 0) {
		errormsg += '<li>' + " Amount cannot be null " + '</li>';
		error_free = false;
	}
	if (_amount.length == 0 && _amount < 0) {
		errormsg += '<li>' + " Amount cannot be negative " + '</li>';
		error_free = false;
	}
	if (_narrative.length == 0) {
		errormsg += '<li>' + " Narrative cannot be null " + '</li>';
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

	$('#apiResults').html('posting transaction...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var doubleEntryDTO = {};
	doubleEntryDTO.transactionType = _transactionType;
	doubleEntryDTO.drAccount = _drAccount;
	doubleEntryDTO.crAccount = _crAccount;
	doubleEntryDTO.amount = _amount;
	doubleEntryDTO.narrative = _narrative;

	gapi.client.accountendpoint.postDoubleEntry(doubleEntryDTO)
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
								window.setTimeout(
										'$("#successmessage").html("");',
										120000);
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
fanikiwa.accountendpoint.post.enableButtons = function() {
	$("#btnPost").removeAttr('style');
	$("#btnPost").removeAttr('disabled');
	$("#btnPost").val('Post');
	var btnPost = document.querySelector('#btnPost');
	btnPost.addEventListener('click', function() {
		fanikiwa.accountendpoint.post();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.accountendpoint.post.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.post.populateTransactionTypes();
			fanikiwa.accountendpoint.post.enableButtons();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);
	gapi.client.load('transactiontypeendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accountendpoint.post.populateTransactionTypes = function() {
	var transactiontypeoptions = '';
	gapi.client.transactiontypeendpoint
			.listTransactionType()
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							resp.items = resp.items || [];
							if (resp.result.items == undefined
									|| resp.result.items == null) {

							} else {
								for (var i = 0; i < resp.result.items.length; i++) {
									transactiontypeoptions += '<option value="'
											+ resp.result.items[i].transactionTypeID
											+ '">'
											+ resp.result.items[i].description
											+ '</option>';
								}
								$("#cbotransactionType").append(
										transactiontypeoptions);
							}
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

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}
