package com.sp.fanikiwa.business.SMS;

import java.util.List;

public class MailingGroupMessage extends FanikiwaMessage
{
    public String Parent ;
    public String GroupName ;
    public String Pwd ;
    public List<String> Members ;

    private String parent = "ROOT"; //Default
    public MailingGroupMessage()
    {

    }
    public MailingGroupMessage(String pwd, String groupname)
    {
        this.GroupName = groupname;
        this.Pwd = pwd;
    }
}