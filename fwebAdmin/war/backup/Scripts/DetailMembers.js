/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.memberendpoint = fanikiwa.memberendpoint || {};
fanikiwa.memberendpoint.memberdetail = fanikiwa.memberendpoint.memberdetail
		|| {};

fanikiwa.memberendpoint.memberdetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('memberdetailsid');
	gapi.client.memberendpoint
			.retrieveMember({
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
								fanikiwa.memberendpoint.memberdetail
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
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.memberendpoint.memberdetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.memberendpoint.memberdetail.populateGender();
			fanikiwa.memberendpoint.memberdetail.populateInformBy();
			fanikiwa.memberendpoint.memberdetail.populateStatus();
			fanikiwa.memberendpoint.memberdetail.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

};

fanikiwa.memberendpoint.memberdetail.populateControls = function(member) {

	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
	if (member.dateActivated != undefined)
		document.getElementById('dtpdateActivated').value = formatDateForControl(member.dateActivated);
	if (member.dateJoined != undefined)
		document.getElementById('dtpdateJoined').value = formatDateForControl(member.dateJoined);
	if (member.dateOfBirth != undefined)
		document.getElementById('dtpdateOfBirth').value = formatDateForControl(member.dateOfBirth);
	if (member.email != undefined)
		document.getElementById('txtemail').value = member.email;
	if (member.gender != undefined)
		document.getElementById('cbogender').value = member.gender;
	if (member.informBy != undefined)
		document.getElementById('cboinformBy').value = member.informBy;
	if (member.maxRecordsToDisplay != undefined)
		document.getElementById('txtmaxRecordsToDisplay').value = member.maxRecordsToDisplay;
	if (member.nationalID != undefined)
		document.getElementById('txtnationalID').value = member.nationalID;
	if (member.otherNames != undefined)
		document.getElementById('txtotherNames').value = member.otherNames;
	if (member.photo != undefined)
		document.getElementById('txtphoto').value = member.photo;
	if (member.refferedBy != undefined)
		document.getElementById('txtrefferedBy').value = member.refferedBy;
	if (member.status != undefined)
		document.getElementById('cbostatus').value = member.status;
	if (member.surname != undefined)
		document.getElementById('txtsurname').value = member.surname;
	if (member.telephone != undefined)
		document.getElementById('txttelephone').value = member.telephone;
	if (member.investmentAccount != undefined)
		document.getElementById('txtinvestmentAccount').value = member.investmentAccount;
	if (member.loanAccount != undefined)
		document.getElementById('txtloanAccount').value = member.loanAccount;
	if (member.currentAccount != undefined)
		document.getElementById('txtcurrentAccount').value = member.currentAccount;
	if (member.interestIncAccount != undefined)
		document.getElementById('txtinterestIncAccount').value = member.interestIncAccount;
	if (member.interestExpAccount != undefined)
		document.getElementById('txtinterestExpAccount').value = member.interestExpAccount;
	if (member.customer != undefined)
		document.getElementById('txtcustomer').value = member.customer;

}

fanikiwa.memberendpoint.memberdetail.populateGender = function() {
	var genderarray = [ {
		id : "M",
		description : "Male"
	}, {
		id : "F",
		description : "Female"
	} ];
	var genderoptions = '';
	for (var i = 0; i < genderarray.length; i++) {
		genderoptions += '<option value="' + genderarray[i].id + '">'
				+ genderarray[i].description + '</option>';
	}
	$("#cbogender").append(genderoptions);
};

fanikiwa.memberendpoint.memberdetail.populateInformBy = function() {
	var informByarray = [ {
		id : "SMS",
		description : "SMS"
	}, {
		id : "EMAIL",
		description : "EMAIL"
	} ];
	var informByoptions = '';
	for (var i = 0; i < informByarray.length; i++) {
		informByoptions += '<option value="' + informByarray[i].id + '">'
				+ informByarray[i].description + '</option>';
	}
	$("#cboinformBy").append(informByoptions);
};

fanikiwa.memberendpoint.memberdetail.populateStatus = function() {
	var statusarray = [ {
		id : "Active",
		description : "Active"
	}, {
		id : "Open",
		description : "Open"
	}, {
		id : "Disabled",
		description : "Disabled"
	}, {
		id : "Closed",
		description : "Closed"
	} ];
	var statusoptions = '';
	for (var i = 0; i < statusarray.length; i++) {
		statusoptions += '<option value="' + statusarray[i].id + '">'
				+ statusarray[i].description + '</option>';
	}
	$("#cbostatus").append(statusoptions);
};
