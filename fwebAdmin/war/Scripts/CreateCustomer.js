/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.customerendpoint = fanikiwa.customerendpoint || {};
fanikiwa.customerendpoint.createcustomer = fanikiwa.customerendpoint.createcustomer
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.customerendpoint.createcustomer = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _address = document.getElementById('txtaddress').value;
	var _billToAddress = document.getElementById('txtbillToAddress').value;
	var _billToEmail = document.getElementById('txtbillToEmail').value;
	var _billToName = document.getElementById('txtbillToName').value;
	var _billToTelephone = document.getElementById('txtbillToTelephone').value;
	var _branch = document.getElementById('txtbranch').value;
	var _customerNo = document.getElementById('txtcustomerNo').value;
	var _email = document.getElementById('txtemail').value;
	var _name = document.getElementById('txtname').value;
	var _telephone = document.getElementById('txttelephone').value;
	var _organization = document.getElementById('cboorganization').value;

	if (_name.length == 0) {
		errormsg += '<li>' + " Name cannot be null " + '</li>';
		error_free = false;
	}
	if (_email.length == 0) {
		errormsg += '<li>' + " Email cannot be null " + '</li>';
		error_free = false;
	}
	if (_telephone.length == 0) {
		errormsg += '<li>' + " Telephone cannot be null " + '</li>';
		error_free = false;
	}
	if (_address.length == 0) {
		errormsg += '<li>' + " Address cannot be null " + '</li>';
		error_free = false;
	}
	if (_organization.length == 0 || _organization == -1) {
		errormsg += '<li>' + " Select Organization " + '</li>';
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

	$('#apiResults').html('creating customer...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var customerDTO = {}; 
	customerDTO.address = _address;
	customerDTO.billToAddress = _billToAddress;
	customerDTO.billToEmail = _billToEmail;
	customerDTO.billToName = _billToName;
	customerDTO.billToTelephone = _billToTelephone;
	customerDTO.branch = _branch;
	customerDTO.customerNo = _customerNo;
	customerDTO.email = _email;
	customerDTO.name = _name;
	customerDTO.telephone = _telephone;
	customerDTO.organization = _organization;

	gapi.client.customerendpoint
			.createCustomer(customerDTO)
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
												'window.location.href = "/Views/Customer/List.html";',
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
fanikiwa.customerendpoint.createcustomer.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnCreate = document.querySelector('#btnCreate');
	btnCreate.addEventListener('click', function() {
		fanikiwa.customerendpoint.createcustomer();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.customerendpoint.createcustomer.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.customerendpoint.createcustomer.populateOrganizations();
			fanikiwa.customerendpoint.createcustomer.enableButtons();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);
	gapi.client.load('organizationendpoint', 'v1', callback, apiRoot);

};

fanikiwa.customerendpoint.createcustomer.populateOrganizations = function() {
	var organizationoptions = '';
	gapi.client.organizationendpoint.listOrganization().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					resp.items = resp.items || [];
					if (resp.result.items == undefined
							|| resp.result.items == null) {

					} else {
						for (var i = 0; i < resp.result.items.length; i++) {
							organizationoptions += '<option value="'
									+ resp.result.items[i].organizationID
									+ '">' + resp.result.items[i].name
									+ '</option>';
						}
						$("#cboorganization").append(organizationoptions);
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