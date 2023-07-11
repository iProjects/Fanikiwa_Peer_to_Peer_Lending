/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.createlendoffer = fanikiwa.offerendpoint.createlendoffer
		|| {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.offerendpoint.createlendoffer = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');

	// Validate the entries
	var _Description = document.getElementById('txtDescription').value;
	var _Amount = document.getElementById('txtAmount').value;
	var _Interest = document.getElementById('txtInterest').value;
	var _Term = document.getElementById('txtTerm').value;
	var _OfferType = document.getElementById('cboOfferType').value;
	var _PublicOffer = Boolean(document.getElementById('chkPublicOffer').checked);
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
	if (_Interest.length == 0) {
		errormsg += '<li>' + " Interest Rate(%) cannot be null " + '</li>';
		error_free = false;
	}
	if (_Term.length == 0) {
		errormsg += '<li>' + " Term in Month cannot be null " + '</li>';
		error_free = false;
	}
	if (_PublicOffer == true && _offerees.length == 0) {
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

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;

	// Build the Request Object
	var OfferDTO = {};
	OfferDTO.description = _Description;
	OfferDTO.amount = _Amount;
	OfferDTO.interest = _Interest;
	OfferDTO.term = _Term;
	OfferDTO.publicOffer = _PublicOffer;
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
							if (resp.result.id == undefined
									|| resp.result.id == null) {
								$('#apiResults').html(
										'operation failed! Please try again');
							} else {
								$('#apiResults').html(
										'operation successful... <br/>'
												+ 'offer id = '
												+ resp.result.id);
								sessionStorage.createlendofferId = resp.result.id;
								window
										.setTimeout(
												'window.location.href = "/Views/Offers/ListLendOffers.html";',
												1000);
							}
						} else {
							$('#apiResults').html(
									'operation failed! Please try again');
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
fanikiwa.offerendpoint.createlendoffer.enableButtons = function() {
	$("#btnCreate").removeAttr('style');
	$("#btnCreate").removeAttr('disabled');
	$("#btnCreate").val('Create');
	var btnRegister = document.querySelector('#btnCreate');  
	btnRegister.addEventListener('click', function() {
		fanikiwa.offerendpoint.createlendoffer();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.offerendpoint.createlendoffer.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.createlendoffer.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

}; 