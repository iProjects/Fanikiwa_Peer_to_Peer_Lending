package com.sp.fanikiwa.business.SMS;

public class BalanceEnquiryMessage extends FanikiwaMessage {
	public Long AccountId;
    public String AccountLabel;
    public String Pwd;

    public BalanceEnquiryMessage()
    {

    }
    public BalanceEnquiryMessage(Long acc, String pwd)
    {
        this.AccountId = acc;
        this.Pwd = pwd;
    }
    public BalanceEnquiryMessage(String accLabel, String pwd)
    {
        AccountLabel = accLabel;
        this.Pwd = pwd;
    }
}
