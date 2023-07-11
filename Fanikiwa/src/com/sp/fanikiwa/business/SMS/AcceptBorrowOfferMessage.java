package com.sp.fanikiwa.business.SMS;

public class AcceptBorrowOfferMessage extends FanikiwaMessage
{
    public Long OfferId ; 
    public double Amount ;        
    public String Pwd ;

    public AcceptBorrowOfferMessage()
    {

    }
    public AcceptBorrowOfferMessage(Long offerid, double amount, String pwd)
    {
        this.OfferId = offerid;
        this.Amount = amount; 
        this.Pwd = pwd;
    }
}

