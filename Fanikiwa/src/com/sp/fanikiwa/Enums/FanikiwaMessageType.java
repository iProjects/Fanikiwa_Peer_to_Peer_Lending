package com.sp.fanikiwa.Enums;


public enum FanikiwaMessageType
{
    //Financial Transactions
    MpesaDepositMessage ( 1), //Identified by source being MPESA 
    WithdrawMessage ( 2),//Identified by message structure e.g. W*<Amount><Password> - means try withdraw the amount
    EarlyLoanRepaymentMessage ( 3),//Identified by message structure e.g. PAY*<LoanId>*<Amount><Password> - means try repay the identified loan by the amount
    BalanceEnquiryMessage ( 4), //Identified by message structure e.g. B*[C*/<AccountId>]<Password> - means return current account balance
    StatementEnquiryMessage ( 5),//Identified by message structure e.g. S*[C*/<AccountId>]<Password> - means return current account mini statement
    //Registration
    RegisterMessage ( 6),//Identified by message structure e.g. R*<Email><Password> - means try register with the email and password supplied
    DeRegisterMessage ( 7),//Identified by message structure e.g. D*<Password> - means try deregister the user
    //offers
    AcceptLendOfferMessage ( 8),//Identified by message structure e.g. ALO*<OfferId><Amount><Password> - means try accept the identified lend offer by the amount
    AcceptBorrowOfferMessage ( 9),//Identified by message structure e.g. ABO*<OfferId><Amount><Password> - means try accept the identified borrow offer by the amount
    MakeLendOfferMessage ( 10),//Identified by message structure e.g. MLO*<Amount><Term><Interest><Password> - means try create a lend offer by the supplied params
    MakeBorrowOfferMessage ( 11),//Identified by message structure e.g. MBO*<Amount><Term><Interest><Password> - means try create a borrow offer by the supplied params
    LendOffersMessage ( 12),//Identified by message structure e.g. LO*<Amount><Password> - means try list lend offers for the identified member
    BorrowOffersMessage ( 13),//Identified by message structure e.g. BO*<Password> - means try create a borrow offers for the identified member
    //Profile   
    ChangePINMessage ( 14),//Identified by message structure e.g. CP*<OldPin><NewPin><ConfirmPin> - means try Change the identified oldpin by the newpin
    HelpMessage ( 15),
    ErrorMessage ( 16),
    MiniStatementEnquiryMessage ( 17),
    MailingGroupMessage ( 18), 
    AccountsListMessage( 19);
    
    private int value;
    private FanikiwaMessageType(int value)
    {
    	this.value = value;
    }
    public int getValue(){ return value;}
}
