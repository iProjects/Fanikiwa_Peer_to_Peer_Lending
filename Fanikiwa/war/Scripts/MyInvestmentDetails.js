/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.loandetail = fanikiwa.loanendpoint.loandetail || {};

fanikiwa.loanendpoint.loandetail.LoadLoanDetails = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('myinvestmentdetailsid');
	gapi.client.loanendpoint.getLoanByID({
		'id' : id
	})
			.execute(
					function(resp) {
						console.log('response =>> ' + resp);
						if (!resp.code) {
							if (resp == false || resp.result.id == undefined) {
								$('#apiResults').html(
										'failed to load loan details...');
							} else {
								$('#apiResults').html('');
								fanikiwa.loanendpoint.loandetail
										.populateLoanDetails(resp);
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

/**
 * Initializes the application.
 * 
 * @param {string}
 *            apiRoot Root of the API's path.
 */
fanikiwa.loanendpoint.loandetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.loandetail.LoadLoanDetails();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

fanikiwa.loanendpoint.loandetail.populateLoanDetails = function(resp) {
	$("#txtLoanId").val(resp.result.id);
	$("#txtamount").val(resp.result.amount);
	$("#txtterm").val(resp.result.term);
	$("#txtinterestRate").val(resp.result.interestRate);
	$("#txtAccruedInterest").val(resp.result.accruedInterest);
	$("#txtinterestRateSusp").val(resp.result.interestRateSusp);
	$("#txtaccruedIntInSusp").val(resp.result.accruedIntInSusp);
	$("#txtintPayingAccount").val(resp.result.intPayingAccount);
	$("#txtintPaidAccount").val(resp.result.intPaidAccount);
	$("#txtborrowerId").val(resp.result.borrowerId);
	$("#txtlenderId").val(resp.result.lenderId);
	$("#txtofferId").val(resp.result.offerId);
	$("#cbointerestAccrualInterval").val(resp.result.interestAccrualInterval);
	$("#cbointerestComputationMethod").val(
			resp.result.interestComputationMethod);
	$("#cbointerestComputationTerm").val(resp.result.interestComputationTerm);
	$("#cbointerestApplicationMethod").val(
			resp.result.interestApplicationMethod);
	document.getElementById('chkaccrueInSusp').checked = resp.result.accrueInSusp;
	document.getElementById('chkPartialPay').checked = resp.result.partialPay;
	$("#dtpcreatedDate").val(formatDateForControl(resp.result.createdDate));
	$("#dtpmaturityDate").val(formatDateForControl(resp.result.maturityDate));
	$("#dtplastIntAccrualDate").val(
			formatDateForControl(resp.result.lastIntAccrualDate));
	$("#dtpnextIntAccrualDate").val(
			formatDateForControl(resp.result.nextIntAccrualDate));
	$("#dtplastIntAppDate").val(
			formatDateForControl(resp.result.lastIntAppDate));
	$("#dtpnextIntAppDate").val(
			formatDateForControl(resp.result.nextIntAppDate));
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
