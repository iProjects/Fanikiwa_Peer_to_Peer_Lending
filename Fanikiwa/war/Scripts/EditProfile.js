/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.memberendpoint = fanikiwa.memberendpoint || {};
fanikiwa.memberendpoint.profile = fanikiwa.memberendpoint.profile || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.memberendpoint.updateprofile = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _memberId = document.getElementById('txtmemberId').value;
	var _dateActivated = document.getElementById('dtpdateActivated').value;
	var _dateJoined = document.getElementById('dtpdateJoined').value;
	var _dateOfBirth = document.getElementById('dtpdateOfBirth').value;
	var _email = document.getElementById('txtemail').value;
	var _gender = document.getElementById('cbogender').value;
	var _informBy = document.getElementById('cboinformBy').value;
	var _maxRecordsToDisplay = document
			.getElementById('txtmaxRecordsToDisplay').value;
	var _nationalID = document.getElementById('txtnationalID').value;
	var _otherNames = document.getElementById('txtotherNames').value;
	var _photo = document.getElementById('txtphoto').value;
	var _refferedBy = document.getElementById('txtrefferedBy').value;
	var _status = document.getElementById('cbostatus').value;
	var _surname = document.getElementById('txtsurname').value;
	var _telephone = document.getElementById('txttelephone').value;
	var _investmentAccount = document.getElementById('txtinvestmentAccount').value;
	var _loanAccount = document.getElementById('txtloanAccount').value;
	var _currentAccount = document.getElementById('txtcurrentAccount').value;
	var _interestIncAccount = document.getElementById('txtinterestIncAccount').value;
	var _interestExpAccount = document.getElementById('txtinterestExpAccount').value;
	var _customer = document.getElementById('txtcustomer').value;

	if (_surname.length == 0) {
		errormsg += '<li>' + " Surname cannot be null " + '</li>';
		error_free = false;
	}
	if (_otherNames.length == 0) {
		errormsg += '<li>' + " OtherNames cannot be null " + '</li>';
		error_free = false;
	}
	if (_nationalID.length == 0) {
		errormsg += '<li>' + " NationalID cannot be null " + '</li>';
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

	$('#apiResults').html('updating profile...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var member = {};
	member.memberId = _memberId;
	member.dateActivated = _dateActivated;
	member.dateJoined = _dateJoined;
	member.dateOfBirth = _dateOfBirth;
	member.email = _email;
	member.gender = _gender;
	member.informBy = _informBy;
	member.maxRecordsToDisplay = _maxRecordsToDisplay;
	member.nationalID = _nationalID;
	member.otherNames = _otherNames;
	member.photo = _photo;
	member.refferedBy = _refferedBy;
	member.status = _status;
	member.surname = _surname;
	member.telephone = _telephone;
	member.investmentAccount = _investmentAccount;
	member.loanAccount = _loanAccount;
	member.currentAccount = _currentAccount;
	member.interestIncAccount = _interestIncAccount;
	member.interestExpAccount = _interestExpAccount;
	member.customer = _customer;

	gapi.client.memberendpoint
			.updateMember(member)
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
												'window.location.href = "/Views/Account/EditProfile.html";',
												1000);
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
 * Enables the button callbacks in the UI.
 */
fanikiwa.memberendpoint.profile.enableButtons = function() {
	$("#btnUpdate").removeAttr('style');
	$("#btnUpdate").removeAttr('disabled');
	$("#btnUpdate").val('Update');
	var btnRegister = document.querySelector('#btnUpdate');
	btnRegister.addEventListener('click', function() {
		fanikiwa.memberendpoint.updateprofile();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.memberendpoint.profile.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.memberendpoint.profile.populateGender();
			fanikiwa.memberendpoint.profile.populateInformBy();
			fanikiwa.memberendpoint.profile.populateStatus();
			fanikiwa.memberendpoint.profile.enableButtons();
			fanikiwa.memberendpoint.profile.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('memberendpoint', 'v1', callback, apiRoot);

};

fanikiwa.memberendpoint.profile.initializeControls = function() {
	$('#apiResults').html('');
	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;
	gapi.client.memberendpoint
			.obtainMemberByEmail({
				'email' : email
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
								fanikiwa.memberendpoint.profile
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

fanikiwa.memberendpoint.profile.populateControls = function(member) {

	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
	if (member.dateActivated != undefined)
		document.getElementById('dtpdateActivated').value = formatDateForControl(member.dateActivated);
	if (member.dateJoined != undefined)
		document.getElementById('dtpdateJoined').value = formatDateForControl(member.dateJoined);
	if (member.dateOfBirth != undefined)
		document.getElementById('dtpdateOfBirth').value = formatDateForControl(member.dateOfBirth);
	else {
		document.getElementById('dtpdateOfBirth').value = decrementDateByYear(
				new Date(), -18, 'y');
	}
	if (member.email != undefined)
		document.getElementById('txtemail').value = member.email;
	if (member.gender != undefined)
		document.getElementById('cbogender').value = member.gender;
	if (member.informBy != undefined)
		document.getElementById('cboinformBy').value = member.informBy;
	else
		document.getElementById('cboinformBy').value = "EMAIL";
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

};

fanikiwa.memberendpoint.profile.populateGender = function() {
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

fanikiwa.memberendpoint.profile.populateInformBy = function() {
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

fanikiwa.memberendpoint.profile.populateStatus = function() {
	var statusarray = [ {
		id : "New",
		description : "New"
	},{
		id : "A",
		description : "Active"
	}, {
		id : "O",
		description : "Open"
	}, {
		id : "D",
		description : "Disabled"
	}, {
		id : "C",
		description : "Closed"
	} ];
	var statusoptions = '';
	for (var i = 0; i < statusarray.length; i++) {
		statusoptions += '<option value="' + statusarray[i].id + '">'
				+ statusarray[i].description + '</option>';
	}
	$("#cbostatus").append(statusoptions);
};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/LendingGroups/List.html" style="cursor: pointer;">My groups</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/ChangePassword.html" style="cursor: pointer;">Change Password</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/DeRegister.html" style="cursor: pointer;">Deregister</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
