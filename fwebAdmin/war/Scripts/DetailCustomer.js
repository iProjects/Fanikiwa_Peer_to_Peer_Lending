/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.customerendpoint = fanikiwa.customerendpoint || {};
fanikiwa.customerendpoint.customerdetail = fanikiwa.customerendpoint.customerdetail
		|| {};

fanikiwa.customerendpoint.customerdetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('customerdetailsid');
	gapi.client.customerendpoint
			.retrieveCustomer({
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
								fanikiwa.customerendpoint.customerdetail
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

fanikiwa.customerendpoint.customerdetail.populateControls = function(customer) {

	if (customer.customerId != undefined)
		document.getElementById('txtcustomerId').value = customer.customerId;
	if (customer.address != undefined)
		document.getElementById('txtaddress').value = customer.address;
	if (customer.billToAddress != undefined)
		document.getElementById('txtbillToAddress').value = customer.billToAddress;
	if (customer.billToEmail != undefined)
		document.getElementById('txtbillToEmail').value = customer.billToEmail;
	if (customer.billToName != undefined)
		document.getElementById('txtbillToName').value = customer.billToName;
	if (customer.billToTelephone != undefined)
		document.getElementById('txtbillToTelephone').value = customer.billToTelephone;
	if (customer.branch != undefined)
		document.getElementById('txtbranch').value = customer.branch;
	if (customer.createdDate != undefined)
		document.getElementById('dtpcreatedDate').value = formatDateForControl(customer.createdDate);
	if (customer.email != undefined)
		document.getElementById('txtemail').value = customer.email;
	if (customer.memberId != undefined)
		document.getElementById('txtmemberId').value = customer.memberId;
	if (customer.name != undefined)
		document.getElementById('txtname').value = customer.name;
	if (customer.telephone != undefined)
		document.getElementById('txttelephone').value = customer.telephone;
	if (customer.customerNo != undefined)
		document.getElementById('txtcustomerNo').value = customer.customerNo;
	if (customer.organization != undefined)
		document.getElementById('cboorganization').value = customer.organization;

};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.customerendpoint.customerdetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.customerendpoint.customerdetail.populateOrganizations();
			fanikiwa.customerendpoint.customerdetail.initializeControls();
		}
	}

	apisToLoad = 2; // must match number of calls to gapi.client.load()
	gapi.client.load('customerendpoint', 'v1', callback, apiRoot);
	gapi.client.load('organizationendpoint', 'v1', callback, apiRoot);

};

fanikiwa.customerendpoint.customerdetail.populateOrganizations = function() {
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