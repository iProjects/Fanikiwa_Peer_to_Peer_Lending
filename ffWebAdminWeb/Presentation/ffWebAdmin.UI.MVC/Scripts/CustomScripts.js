///reference path="_reference.js" />
 
$(document).ajaxStart(function () {
    $("#progress").removeClass('displaynone');
    $("#progress").addClass('displayblock');
    $("#progress").show();
     
}).ajaxStop(function () {
    $("#progress").removeClass('displayblock');
    $("#progress").addClass('displaynone');
    $("#progress").hide();
});
 
$(document).ready(function () {

    $("#btnSubmitIndexForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#home-form").submit();
    });
     
    $("#btnSubmitGetAllMailingGroupsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#offer-groups-form").submit();
    });

    $("#btnSubmitMPESAMMessagesForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#mpesa-messages-form").submit();
    });

    $("#btnSubmitGetAllRegisteredMembersForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#registered-members-form").submit();
    });
     
    $("#btnSubmitDepositCashForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#deposit-form").submit();
    });

    $("#btnSubmitWithDrawCashForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#withdraw-form").submit();
    });

    $("#btnSubmitGetAllAccountsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#fanikiwa-accounts-form").submit();
    });

    $("#btnSubmitGetAllTransactionTypesForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#transaction-types-form").submit();
    });

    $("#btnSubmitGetAllTransactionsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#transactions-form").submit();
    });

    $("#btnSubmitGetAllOffersForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#all-offers-form").submit();
    });

    $("#btnSubmitListAllLoansForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#list-all-loans-form").submit();
    });

    $("#btnSubmitHelpForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#help-form").submit();
    });

    $("#btnSubmitContactUsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#contact-us-form").submit();
    });

    $("#btnSubmitContactsForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#contacts-form").submit();
    });

    $("#btnSubmitAboutForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#about-form").submit();
    });

    $("#btnSubmitLogOffPartialForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#log-off-partial-form").submit();
    });
     
    $("#btnSubmitLoginPartialForm").on("click", function (e) {

        e.preventDefault();

        $("#progress").removeClass('displaynone');
        $("#progress").addClass('displayblock');
        $("#progress").show();

        $("#login-partial-form").submit();
    });
     



});

  


