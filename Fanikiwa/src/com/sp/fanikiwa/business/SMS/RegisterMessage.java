package com.sp.fanikiwa.business.SMS;

public class RegisterMessage extends FanikiwaMessage
{
    public String Email ;
    public String Pwd ;
    public String NationalID ;
    public String NotificationMethod ;

    public RegisterMessage()
    {

    }
    public RegisterMessage(String email, String pwd, String nationalid, String notificationMethod)
    {
        this.Email = email;
        this.Pwd = pwd;
        this.NationalID = nationalid;
        this.NotificationMethod = notificationMethod;
    }
}