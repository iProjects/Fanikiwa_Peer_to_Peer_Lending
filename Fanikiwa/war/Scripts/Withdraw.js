/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.accountendpoint = fanikiwa.accountendpoint || {};
fanikiwa.accountendpoint.withdraw = fanikiwa.accountendpoint.withdraw || {};

fanikiwa.accountendpoint.withdraw.enableButtons = function() {
	$("#btnWithdraw").removeAttr('style');
	$("#btnWithdraw").removeAttr('disabled');
	$("#btnWithdraw").val('Withdraw');
	var btnWithdraw = document.querySelector('#btnWithdraw');
	btnWithdraw.addEventListener('click', function() {
		fanikiwa.accountendpoint.withdraw.memberWithdraw();
	});


	$("#btnDeposit").removeAttr('style');
	$("#btnDeposit").removeAttr('disabled');
	$("#btnDeposit").val('How To Deposit');
	var btnDeposit = document.querySelector('#btnDeposit');
	btnDeposit.addEventListener('click', function() {
		window.location.href = "/Views/Account/Deposit.html";
	});

};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.accountendpoint.withdraw.memberWithdraw = function() {

	errormsg = '';
	$('#errorList').remove();
	$('#error-display-div').empty();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	var email = JSON.parse(sessionStorage.getItem('loggedinuser')).userId;
	var amount = document.getElementById('txtAmount').value;
	var remissionMethod = document.getElementById('cboremissionMethod').value;

	if (amount.length == 0) {
		errormsg += '<li>' + " Amount cannot be null " + '</li>';
		error_free = false;
	}
	if (amount < 0) {
		errormsg += '<li>' + " Amount cannot be negative " + '</li>';
		error_free = false;
	}
//	if (amount < 100) {
//		errormsg += '<li>' + " Minimum withdraw amount is [ 100.0 ]" + '</li>';
//		error_free = false;
//	}
	if (remissionMethod.length == 0) {
		errormsg += '<li>' + " Select Remission Method " + '</li>';
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
		$('#errorList').remove();
		$('#error-display-div').empty();
	}

	$('#apiResults').html('processing...');

	var withdrawalDTO = {};
	withdrawalDTO.email = email;
	withdrawalDTO.remissionMethod = remissionMethod;
	withdrawalDTO.amount = amount;
	gapi.client.accountendpoint.withdraw(withdrawalDTO)
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
fanikiwa.accountendpoint.withdraw.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.accountendpoint.withdraw.populateRemissionMethod();
			fanikiwa.accountendpoint.withdraw.enableButtons();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('accountendpoint', 'v1', callback, apiRoot);

};

fanikiwa.accountendpoint.withdraw.populateRemissionMethod = function() {
	var remissionMethodarray = [ {
		id : "MPESA",
		description : "MPESA"
	}
//	, {
//		id : "EFT",
//		description : "EFT"
//	}, {
//		id : "BANKMOBI",
//		description : "BANKMOBI"
//	}
//	
	];
	var remissionMethodoptions = '';
	for (var i = 0; i < remissionMethodarray.length; i++) {
		remissionMethodoptions += '<option value="'
				+ remissionMethodarray[i].id + '">'
				+ remissionMethodarray[i].description + '</option>';
	}
	$("#cboremissionMethod").append(remissionMethodoptions);
};

function CreateSubMenu() {
	var SubMenu = [];
	SubMenu.push('<div class="nav"><ul class="menu">');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Statement.html" style="cursor: pointer;">Statement</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Account/Withdraw.html" style="cursor: pointer;">Withdraw</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Loans/ListMyLoans.html" style="cursor: pointer;">My loans</a></div></div></li>');
	SubMenu
			.push('<li><div class="floatleft"><div><a href="/Views/Loans/MyInvestMentsList.html" style="cursor: pointer;">My investments</a></div></div></li>');
	SubMenu.push('</ul></div>');

	$("#SubMenu").html(SubMenu.join(" "));
}

$(document).ready(function() {
	CreateSubMenu();
});
