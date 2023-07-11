package com.sp.fanikiwa.business.SMS;

public class ErrorMessage extends FanikiwaMessage {
	public String Error_Message;
    public String FriendlyMessage;
    public Exception Exception;
    public ErrorMessage()
    {
    }
    public ErrorMessage(String message)
    {
        Error_Message = message;
    }
}
