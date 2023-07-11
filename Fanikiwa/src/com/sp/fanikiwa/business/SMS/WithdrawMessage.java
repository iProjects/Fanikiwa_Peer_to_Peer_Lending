package com.sp.fanikiwa.business.SMS;

public class WithdrawMessage extends FanikiwaMessage {

	public double Amount;
    public String Pwd ;

    public WithdrawMessage()
    {

    }
    public WithdrawMessage(double amount, String pwd)
    {
        this.Amount = amount; 
        this.Pwd = pwd;
    }
}
