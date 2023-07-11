package com.sp.fanikiwa.business.SMS;

public class BorrowOffersMessage extends FanikiwaMessage
{ 
    public String Pwd ;

    public BorrowOffersMessage()
    {

    }
    public BorrowOffersMessage(String pwd)
    { 
        this.Pwd = pwd;
    }
}
