/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.createborrowoffer = fanikiwa.offerendpoint.createborrowoffer
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.offerendpoint.createborrowoffer = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _Description = document.getElementById('txtDescription').value;
	var _Amount = document.getElementById('txtAmount').value;
	var _Interest = document.getElementById('txtInterest').value;
	var _Term = document.getElementById('cboTerm').value;
	var _OfferType = document.getElementById('cboOfferType').value;
	var _privateOffer = Boolean(document.getElementById('chkprivateOffer').checked);
	var _PartialPay = Boolean(document.getElementById('chkPartialPay').checked);
	var _offerees = document.getElementById('txtofferees').value;

	if (_Description.length == 0) {
		errormsg += '<li>' + " Description cannot be null " + '</li>';
		error_free = false;
	}
	if (_Amount.length == 0) {
		errormsg += '<li>' + " Amount cannot be null " + '</li>';
		error_free = false;
	}
	if (_Amount < 0) {
		errormsg += '<li>' + " Amount cannot be negative " + '</li>';
		error_free = false;
	}
	if (_Interest.length == 0) {
		errormsg += '<li>' + " Interest Rate(%) cannot be null " + '</li>';
		error_free = false;
	}
	if (_Interest < 0) {
		errormsg += '<li>' + " Interest cannot be negative " + '</li>';
		error_free = false;
	}
	if (_Term.length == 0) {
		errormsg += '<li>' + " Select Term " + '</li>';
		error_free = false;
	} 
	if (_privateOffer == true && _offerees.length == 0) {
		errormsg += '<li>' + " Offerees cannot be null for a private offer"
				+ '</li>';
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

	$('#apiResults').html('creating offer...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;

	// Build the Request Object
	var OfferDTO = {};
	OfferDTO.description = _Description;
	OfferDTO.amount = _Amount;
	OfferDTO.interest = _Interest;
	OfferDTO.term = _Term;
	OfferDTO.privateOffer = _privateOffer;
	OfferDTO.offerType = _OfferType;
	OfferDTO.partialPay = _PartialPay;
	OfferDTO.status = "Open";
	OfferDTO.email = email;
	OfferDTO.offerees = _offerees;

	gapi.client.offerendpoint
			.saveOffer(OfferDTO)
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
												'window.location.href = "/Views/Offers/ListBorrowOffers.html";',
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
fanikiwa.offerendpoint.createborrowoffer.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnRegister = document.querySelector('#btnCreate');
	btnRegister.addEventListener('click', function() {
		fanikiwa.offerendpoint.createborrowoffer();
	});
	$("#chkprivateOffer").attr('checked', false);
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.offerendpoint.createborrowoffer.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.createborrowoffer.populateTerms();
			fanikiwa.offerendpoint.createborrowoffer.populateOfferTypes();
			fanikiwa.offerendpoint.createborrowoffer.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.offerendpoint.createborrowoffer.populateTerms = function() {
	var termarray = [ {
		id : "0",
		description : "0"
	}, {
		id : "1",
		description : "1"
	}, {
		id : "2",
		description : "2"
	}, {
		id : "3",
		description : "3"
	}, {
		id : "6",
		description : "6"
	}, {
		id : "12",
		description : "12"
	} ];
	var termoptions = '';
	for (var i = 0; i < termarray.length; i++) {
		termoptions += '<option value="' + termarray[i].id + '">'
				+ termarray[i].description + '</option>';
	}
	$("#cboTerm").append(termoptions);
};

fanikiwa.offerendpoint.createborrowoffer.populateOfferTypes = function() {
	var OfferTypearray = [ {
		id : "B",
		description : "Borrow"
	} ];
	var OfferTypeoptions = '';
	for (var i = 0; i < OfferTypearray.length; i++) {
		OfferTypeoptions += '<option value="' + OfferTypearray[i].id + '">'
				+ OfferTypearray[i].description + '</option>';
	}
	$("#cboOfferType").html(OfferTypeoptions);
};
 
function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Offers/Create.html">Make an Offer</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Offers/CreateLendOffer.html">I want to Lend Some Money</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Offers/CreateBorrowOffer.html">I want to Borrow Some Money</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
