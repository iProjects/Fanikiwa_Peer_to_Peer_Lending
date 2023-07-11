/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.offerdetail = fanikiwa.offerendpoint.offerdetail || {};

fanikiwa.offerendpoint.offerdetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('offerdetailsid');
	gapi.client.offerendpoint
			.selectOffer({
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
								fanikiwa.offerendpoint.offerdetail
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
fanikiwa.offerendpoint.offerdetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.offerdetail.populateOfferType();
			fanikiwa.offerendpoint.offerdetail.populateStatus();
			fanikiwa.offerendpoint.offerdetail.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

};

fanikiwa.offerendpoint.offerdetail.populateControls = function(offer) {

	if (offer.id != undefined)
		document.getElementById('txtOfferId').value = offer.id;
	if (offer.description != undefined)
		document.getElementById('txtDescription').value = offer.description;
	if (offer.amount != undefined)
		document.getElementById('txtAmount').value = offer.amount;
	if (offer.interest != undefined)
		document.getElementById('txtInterest').value = offer.interest;
	if (offer.term != undefined)
		document.getElementById('txtTerm').value = offer.term;
	if (offer.offerees != undefined)
		document.getElementById('txtofferees').value = offer.offerees;
	if (offer.offerType != undefined)
		document.getElementById('cboOfferType').value = offer.offerType;
	if (offer.publicOffer != undefined)
		document.getElementById('chkPublicOffer').value = offer.publicOffer;
	if (offer.partialPay != undefined)
		document.getElementById('chkPartialPay').value = offer.partialPay;
	if (offer.createdDate != undefined)
		document.getElementById('dtpcreatedDate').value = formatDateForControl(offer.createdDate);
	if (offer.expiryDate != undefined)
		document.getElementById('dtpexpiryDate').value = formatDateForControl(offer.expiryDate);
	if (offer.status != undefined)
		document.getElementById('cbostatus').value = offer.status;

}

fanikiwa.offerendpoint.offerdetail.populateOfferType = function() {
	var OfferTypearray = [ {
		id : "L",
		description : "Lend"
	}, {
		id : "B",
		description : "Borrow"
	} ];
	var OfferTypeoptions = '';
	for (var i = 0; i < OfferTypearray.length; i++) {
		OfferTypeoptions += '<option value="' + OfferTypearray[i].id + '">'
				+ OfferTypearray[i].description + '</option>';
	}
	$("#cboOfferType").append(OfferTypeoptions);
};

fanikiwa.offerendpoint.offerdetail.populateStatus = function() {
	var statusarray = [ {
		id : "Open",
		description : "Open"
	}, {
		id : "Processing",
		description : "Processing"
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
