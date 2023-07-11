/**
 * @fileoverview
 * Provides methods for the  Endpoints UI and interaction with the
 * Endpoints API.
 */

/** global namespace for projects. */
var fanikiwa = fanikiwa || {};
fanikiwa.loanendpoint = fanikiwa.loanendpoint || {};
fanikiwa.loanendpoint.loandetail = fanikiwa.loanendpoint.loandetail || {};

fanikiwa.loanendpoint.loandetail.initializeControls = function() {
	$('#apiResults').html('loading...');
	var id = sessionStorage.getItem('loandetailsid');
	gapi.client.loanendpoint
			.getLoanByID({
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
								fanikiwa.loanendpoint.loandetail
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
fanikiwa.loanendpoint.loandetail.init = function(apiRoot) {
	// Loads the APIs asynchronously, and triggers callback
	// when they have completed.
	var apisToLoad;
	var callback = function() {
		if (--apisToLoad == 0) {
			fanikiwa.loanendpoint.loandetail.initializeControls();
		}
	}

	apisToLoad = 1; // must match number of calls to gapi.client.load()
	gapi.client.load('loanendpoint', 'v1', callback, apiRoot);

};

fanikiwa.loanendpoint.loandetail.populateControls = function(resp) {
	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
	if (member.memberId != undefined)
		document.getElementById('txtmemberId').value = member.memberId;
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
