package com.sp.fanikiwa;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.util.Map;
import java.util.HashMap;
import com.twilio.sdk.resource.instance.Account;
import com.twilio.sdk.TwilioRestClient;
import com.twilio.sdk.TwilioRestException;
import com.twilio.sdk.resource.factory.SmsFactory;
import com.twilio.sdk.resource.instance.Sms;

//Sending sms
public class SMSTwilioSenderServlet extends HttpServlet {
	/* Find your sid and token at twilio.com/user/account */
	public static final String ACCOUNT_SID = "AC123";
	public static final String AUTH_TOKEN = "456bef";

	// Handle an incoming HTTP Request
	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws IOException {
		// Create a Twilio REST client
		TwilioRestClient client = new TwilioRestClient(ACCOUNT_SID, AUTH_TOKEN);
		Account account = client.getAccount();
		// Use the API to send a text message
		SmsFactory smsFactory = account.getSmsFactory();
		Map<String, String> smsParams = new HashMap<String, String>();
		smsParams.put("To", "+254715413144");
		smsParams.put("From", "(410) 555-6789"); // Replace with a Twilio phone
													// number in your account
		smsParams.put("Body", "Where's Wallace?");
		try {
			Sms sms = smsFactory.create(smsParams);
		} catch (TwilioRestException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
}