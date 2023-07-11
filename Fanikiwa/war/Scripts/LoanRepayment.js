/**
 * @fileoverview
 * Provides methods for the Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.loanrepayment = fanikiwa.loanendpoint.loanrepayment || {};

var errormsg = '';
errormsg += '<ul id="errorList">';

fanikiwa.loanendpoint.loanrepayment = function() {

	errormsg = '';
	ClearException();
	errormsg += '<ul id="errorList">';
	var error_free = true;
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Validate the entries
	var _loanId = document.getElementById('txtloanId').value;
	var _amount = document.getElementById('txtamount').value;

	if (_loanId.length == 0) {
		errormsg += '<li>' + " Id cannot be null " + '</li>';
		error_free = false;
	}
	if (_amount.length == 0) {
		errormsg += '<li>' + " Amount cannot be null " + '</li>';
		error_free = false;
	}
	if (_amount < 0) {
		errormsg += '<li>' + " Amount cannot be negative " + '</li>';
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

	$('#apiResults').html('repaying loan...');
	$('#successmessage').html('');
	$('#errormessage').html('');

	// Build the Request Object
	var loanPrepaymentDTO = {};
	loanPrepaymentDTO.id = _loanId;
	loanPrepaymentDTO.amount = _amount;
	loanPrepaymentDTO.repayDate = new Date();

	gapi.client.loanendpoint
			.prepayLoan(loanPrepaymentDTO)
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
												'window.location.href = "/Views/Loans/ListMyLoans.html";',
												1000);
							}
						} else {
							$('#errormessage').html(
									'operation failed! Please try again.');
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

fanikiwa.loanendpoint.loanrepayment.initializeControls = function() {
	$('#apiResults').html('');
	$('#successmessage').html('');
	$('#errormessage').html('');

	var id = sessionStorage.getItem('loanrepaymentid');
	gapi.client.loanendpoint
			.retrieveLoan({
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
								fanikiwa.loanendpoint.loanrepayment
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

fanikiwa.loanendpoint.loanrepayment.populateControls = function(loan) {

	if (loan.partialPay == true) {
		if (loan.id != undefined) {
			document.getElementById('txtloanId').value = loan.id;
			$('#txtloanId').attr('disabled', true);
		}
		if (loan.amount != undefined) {
			document.getElementById('txtamount').value = loan.amount;
			$('#txtamount').attr('disabled', false);
		}
	} else {
		if (loan.id != undefined) {
			document.getElementById('txtloanId').value = loan.id;
			$('#txtloanId').attr('disabled', true);
		}
		if (loan.amount != undefined) {
			document.getElementById('txtamount').value = loan.amount;
			$('#txtamount').attr('disabled', true);
		}
	}

};

/**
 * Enables the button callbacks in the UI.
 */
fanikiwa.loanendpoint.loanrepayment.enableButtons = function() {
	$("#btnSubmit").removeAttr('style');
	$("#btnSubmit").removeAttr('disabled');
	$("#btnSubmit").val('Repay Loan');
	var btnSubmit = document.querySelector('#btnSubmit');
	btnSubmit.addEventListener('click', function() {
		fanikiwa.loanendpoint.loanrepayment();
	});
};

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.loanendpoint.loanrepayment.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.loanrepayment.enableButtons();
			fanikiwa.loanendpoint.loanrepayment.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

function ClearException() {
	$('#errorList').remove();
	$('#error-display-div').empty();
}

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
