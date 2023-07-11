package com.sp.fanikiwa.business.SMS;

public class DeRegisterMessage extends FanikiwaMessage
{
    public String Email ;
    public String Pwd ;

    public DeRegisterMessage()
    {

    }
    public DeRegisterMessage(String pwd, String email)
    {
        this.Email = email;
        this.Pwd = pwd;
    }
}
