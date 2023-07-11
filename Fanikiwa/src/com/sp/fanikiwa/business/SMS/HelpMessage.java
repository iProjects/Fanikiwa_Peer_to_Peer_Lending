package com.sp.fanikiwa.business.SMS;

public class HelpMessage extends FanikiwaMessage {
	public String HelpCommand;

    public HelpMessage()
    {
    }
    public HelpMessage(String command)
    {
        HelpCommand = command;
    }
}
