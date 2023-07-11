package com.sp.fanikiwa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.DateFormat;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.servlet.ServletRequest;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;



//import com.fasterxml.jackson.databind.ObjectMapper;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.sp.fanikiwa.business.SMS.FanikiwaMessage;
import com.sp.fanikiwa.business.SMS.FanikiwaMessageFactory;
import com.sp.fanikiwa.business.SMS.MpesaDepositMessage;
import com.sp.fanikiwa.business.SMS.SMSProcessorComponent;
import com.sp.fanikiwa.business.messaging.SMSmessage;
import com.sp.utils.HttpUtil;

public class SMSReceiveServlet extends HttpServlet {
	private static final Logger log = Logger.getLogger(SMSReceiveServlet.class
			.getName());

	final String FANIKIWATELNO = "0717769329";
	boolean Enablelog = true;
	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	public void doPost(HttpServletRequest request, HttpServletResponse response)
			throws IOException {
		ProcessSMS(request, response);
	}

	public void doGet(HttpServletRequest request, HttpServletResponse response)
			throws IOException {
		ProcessSMS(request, response);
	}

	private void ProcessSMS(HttpServletRequest request,
			HttpServletResponse response) {
		SMSmessage msg;
		try {
			// 1. Get sms from request
			msg = MakeSMSFromRequest(request, response);
			if (msg != null) {
				// 2. convert sms into a fanikiwa message
				if(Enablelog) log.info("Creating fanikiwa message from sms["+msg.getBody().toString()+"]");
				FanikiwaMessage fMsg = FanikiwaMessageFactory.CreateMessage(msg
						.getAddressFrom(), msg.getMessageDate(), msg.getBody()
						.toString());

				// 3.process the fanikiwa message
				if(Enablelog) log.info("Processing fanikiwa message["+fMsg.getFanikiwaMessageType().name()+"] ");
				SMSProcessorComponent smsComp = new SMSProcessorComponent();
				String result = smsComp.ProcessFanikiwaMessage(fMsg);

				// 4. send the response back
				if(Enablelog) log.info("Sending response  result["+result+"] ");
				SendResponse(response, result, msg);
			}
		} catch (IOException e) {
			log.log(Level.SEVERE,"Error Message = "+e.getMessage(),e);
		} catch (Exception e) {
			log.log(Level.SEVERE,"Error Message = "+e.getMessage(),e);
		}
	}

	private SMSmessage MakeSMSFromRequest(HttpServletRequest request,
			HttpServletResponse response) throws IOException {

		// 1. get received JSON data from request
		BufferedReader br = new BufferedReader(new InputStreamReader(
				request.getInputStream()));
		String json = "";
		if (br != null) {
			json = br.readLine();
		}

		// 2. initiate jackson mapper
		//Gson gson = new Gson();
		Gson gson=  new GsonBuilder().setDateFormat("yyyy-MM-dd'T'HH:mm:ssX").create();
		SMSmessage msg = gson.fromJson(json, SMSmessage.class);

		if (msg == null) {
			return null;
		}
		
		log.info("Gson = " + gson.toJson(msg));

		// 3. Convert received JSON to Article
		msg.setAddressTo(FANIKIWATELNO);
		msg.setMessageDate(new Date());

		return msg;
	}

	private void SendResponse(HttpServletResponse response, String result,
			SMSmessage sms) {
		// write to response -- FOR TESTING
		try {
			//String msg = "To: " + sms.getAddressFrom() + "\nMessage: " + result;
			response.getWriter().println(result);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			log.log(Level.SEVERE,e.getMessage(),e);
		}

		// FOR LIVE
		/*
		 * SMSURL="https://smscompany/fanikiwaAcc/SMS" 1. smsrequest =
		 * create(SMSURL) 2. Json jsonresult = ConvertToJason(result,original)
		 * 3. smsrequest.getWriter().write(jsonresult)
		 */

	}
}