package com.sp.fanikiwa.business.SMS;

public class ChangePinMessage extends FanikiwaMessage
{
    public String OldPassword ;
    public String NewPassword ;
    public String ConfirmPassword ;

    public ChangePinMessage()
    {

    }
    public ChangePinMessage(String oldpassword, String newpassword, String confirmpassword)
    {
        this.OldPassword = oldpassword;
        this.NewPassword = newpassword;
        this.ConfirmPassword = confirmpassword; 
    }
}
