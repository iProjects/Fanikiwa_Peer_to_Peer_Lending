package com.sp.fanikiwa.business.SMS;

public class EarlyLoanRepaymentMessage extends FanikiwaMessage
{

    public Long OfferId ;
    public double Amount ;
    public String Pwd ;

    public EarlyLoanRepaymentMessage()
    {

    }
    public EarlyLoanRepaymentMessage(Long offer, double amount, String pwd)
    {
        this.Amount = amount;
        this.OfferId = offer;
        this.Pwd = pwd;
    }
}