package com.sp.utils;

import java.util.logging.Logger;


public class SMSUtil {
	private static final Logger log = Logger.getLogger(SMSUtil.class
			.getName());
	public static void SendSMS(String msg, String telno)
	{
		log.info("Message["+msg+"] sent to["+telno+"]");
		//implement calling a sms server
	}
}
