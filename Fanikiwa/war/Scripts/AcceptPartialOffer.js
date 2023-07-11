/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.listborrowoffers = fanikiwa.offerendpoint.listborrowoffers
		|| {};

fanikiwa.offerendpoint.listborrowoffers.LoadOffers = function() {

	$('#listOffersResult').html('loading...');

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;

	gapi.client.offerendpoint.retrieveBorrowOffers({
		'email' : email
	}).execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listOffersResult').html(
								'You have no private borrow Offers...');
					} else {
						buildTable(resp);
					}
				}

			}, function(reason) {
				console.log('Error: ' + reason.result.error.message);
			});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.offerendpoint.listborrowoffers.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.listborrowoffers.LoadOffers();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

};
 
function PartialAccept(id) {
	$('#apiResults').html('processing...');
	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;
	gapi.client.offerendpoint
			.acceptPartialBorrowOffer({
				'id' : id,
				'email' : email
			})
			.execute(
					function(resp) {
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
								sessionStorage.acceptlendofferId = resp.result.id;
								window
										.setTimeout(
												'window.location.href = "/Views/Offers/ListLendOffers.html";',
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
					});
}
