package com.sp.fanikiwa.business.SMS;

public class LendOffersMessage extends FanikiwaMessage
{ 
    public String Pwd ;

    public LendOffersMessage()
    {

    }
    public LendOffersMessage(int offerid, double amount, String pwd)
    { 
        this.Pwd = pwd;
    }
}
