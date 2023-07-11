package com.sp.utils;

import java.io.UnsupportedEncodingException;
import java.util.Date;

import com.sp.fanikiwa.entity.Userprofile;

public class TokenUtil {
	public static void SendToken(Userprofile user, String token) throws UnsupportedEncodingException
	{
		SendToken("SMS",  user,  token);
	}

	public static void SendToken(String sendMethod, Userprofile user, String token) throws UnsupportedEncodingException
	{
		if(sendMethod.equals("Web")) 
		{
//			//send token via sms
//			StringBuilder sb = new StringBuilder();
//			sb.append("Your registration to Fanikiwa was successful. ");
//			//sb.append(re.getResultMessage());
//			sb.append("In order to access further functionalities in Fanikiwa, please key in the following token to activate ");
//			sb.append( token);
//			SMSUtil.SendSMS(sb.toString(), user.getTelephone());
			
			//send token via mail
			StringBuilder sb = new StringBuilder();
			sb.append("Your registration to Fanikiwa was successful. ");
			//sb.append(re.getResultMessage());
			sb.append("In order to access further functionalities in Fanikiwa, please click the link below to activate your account ");
			sb.append("http://www.kufanikiwa.co.ke/Activate?token=" + token + "&email="+user.getUserId()+"&method=web");
			String body = sb.toString();
		
			MailUtil.sendMail(user.getUserId(), "Registration ["+token+"]", body);
		} 
		else if (sendMethod.equals("SMS")) 
		{
			//send token via mail
			StringBuilder sb = new StringBuilder();
			sb.append("Your registration to Fanikiwa was successful. ");
			//sb.append(re.getResultMessage());
			sb.append("In order to access further functionalities in Fanikiwa, please click the link below to activate your account ");
			sb.append("http://www.kufanikiwa.co.ke/Activate?token=" + token + "&email="+user.getUserId()+"&method=web");
			String body = sb.toString();
			MailUtil.sendMail(user.getUserId(), "Registration ["+token+"]", body);
		}else
		{
			//must be mobi
		} 
	}

	public static void SetUserToken(Userprofile user, Date genDate)
	{
		SessionIdentifierGenerator gen = new SessionIdentifierGenerator();
		user.setToken(gen.nextId());
		user.setActivationTokenExpiryDate(DateExtension.addMinutes(genDate,Config.GetInt("TOKENEXPIRYDURATION",30)));

	}
}
