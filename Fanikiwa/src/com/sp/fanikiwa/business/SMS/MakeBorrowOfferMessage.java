package com.sp.fanikiwa.business.SMS;

public class MakeBorrowOfferMessage extends FanikiwaMessage
{
    public double Amount ;
    public int Term ;
    public double InterestRate ;
    public String Pwd ;

    public MakeBorrowOfferMessage()
    {

    }
    public MakeBorrowOfferMessage(double amount, int term, double interestrate, String pwd)
    {
        this.Amount = amount;
        this.Term = term;
        this.InterestRate = interestrate;
        this.Pwd = pwd;
    }
}
