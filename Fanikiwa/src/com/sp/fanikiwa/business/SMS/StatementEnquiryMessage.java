package com.sp.fanikiwa.business.SMS;

import java.util.Date;

public class StatementEnquiryMessage extends FanikiwaMessage {
	public Long AccountId ;
    public String AccountLabel ;
    public Date StartDate;
    public Date EndDate;
    public String Pwd;

    public StatementEnquiryMessage()
    {

    }
    public StatementEnquiryMessage(Long acc, String pwd)
    {
        this.AccountId = acc;
        this.Pwd = pwd;
    }
}
