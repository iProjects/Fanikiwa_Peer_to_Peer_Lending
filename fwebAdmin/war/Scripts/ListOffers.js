/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.offerendpoint = fanikiwa.offerendpoint || {};
fanikiwa.offerendpoint.listoffers = fanikiwa.offerendpoint.listoffers || {};

fanikiwa.offerendpoint.listoffers.LoadOffers = function() {

	$('#listOffersResult').html('loading...');

	var email = sessionStorage.getItem('loggedinuser');

	gapi.client.offerendpoint.listOffer().execute(
			function(resp) {
				console.log('response =>> ' + resp);
				if (!resp.code) {
					if (resp.result.items == undefined
							|| resp.result.items == null) {
						$('#listOffersResult').html('There are no Offers...');
					} else {
						buildTable(resp);
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
								+ reason.result.error.message);
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
fanikiwa.offerendpoint.listoffers.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.offerendpoint.listoffers.LoadOffers();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('offerendpoint', 'v1', callback, apiRoot);

};

var offerTable = '';
function buildTable(response) {

	offerTable = '';

	populateOffers(response);

	$("#listOffersResult").html(offerTable);

	$('#listOffersTable').DataTable(
			{
				"aLengthMenu" : [ [ 5, 10, 20, 50, 100, -1 ],
						[ 5, 10, 20, 50, 100, "All" ] ],
				"iDisplayLength" : 5
			});

}

function populateOffers(resp) {

	if (!resp.code) {
		resp.items = resp.items || [];

		$(".page-title").append("  [" + resp.result.items.length + "] ");

		offerTable += '<table id="listOffersTable">';
		offerTable += "<thead>";
		offerTable += "<tr>";
		offerTable += "<th>Description</th>";
		offerTable += "<th>Amount</th>";
		offerTable += "<th>Term</th>";
		offerTable += "<th>Interest</th>";
		offerTable += "<th>Private Offer</th>";
		offerTable += "<th>Partial Pay</th>";
		offerTable += "<th>Offer Type</th>";
		offerTable += "<th>Status</th>";
		offerTable += "<th></th>";
		offerTable += "</tr>";
		offerTable += "</thead>";
		offerTable += "<tbody>";

		for (var i = 0; i < resp.result.items.length; i++) {
			offerTable += '<tr>';
			offerTable += '<td>' + resp.result.items[i].description + '</td>';
			offerTable += '<td style="text-align:right">'
					+ resp.result.items[i].amount.formatMoney(2) + '</td>';
			offerTable += '<td style="text-align:right">'
					+ resp.result.items[i].term + '</td>';
			offerTable += '<td style="text-align:right">'
					+ resp.result.items[i].interest + '</td>';
			offerTable += '<td>' + resp.result.items[i].privateOffer + '</td>';
			offerTable += '<td>' + resp.result.items[i].partialPay + '</td>';
			if (resp.result.items[i].offerType == 'L')
				offerTable += '<td>' + 'Lend' + '</td>';
			else
				offerTable += '<td>' + 'Borrow' + '</td>';
			offerTable += '<td>' + resp.result.items[i].status + '</td>';
			offerTable += '<td><a href="#" onclick="Details('
					+ resp.result.items[i].id + ')">Details</a> </td>';
			offerTable += "</tr>";
		}

		offerTable += "</tbody>";
		offerTable += "</table>";

	} else {
		console.log('Error: ' + resp.error.message);
		$('#errormessage').html(
				'operation failed! Error...<br/>' + resp.error.message);
		$('#successmessage').html('');
		$('#apiResults').html('');
	}
}

function Details(id) {
	sessionStorage.offerdetailsid = id;
	window.location.href = "/Views/Offer/Details.html";
}
