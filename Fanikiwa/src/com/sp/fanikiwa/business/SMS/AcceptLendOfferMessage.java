package com.sp.fanikiwa.business.SMS;

public class AcceptLendOfferMessage extends FanikiwaMessage
{
    public Long OfferId ;
    public double Amount ;
    public String Pwd ;

    public AcceptLendOfferMessage()
    {

    }
    public AcceptLendOfferMessage(Long offerid, double amount, String pwd)
    {
        this.OfferId = offerid;
        this.Amount = amount;
        this.Pwd = pwd;
    }
}