package com.sp.fanikiwa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.DateFormat;
import java.text.ParseException;
import java.util.Date;
import java.util.List;

import javax.servlet.ServletRequest;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.sp.fanikiwa.Enums.FanikiwaMessageType;
import com.sp.fanikiwa.business.SMS.FanikiwaMessage;
import com.sp.fanikiwa.business.SMS.FanikiwaMessageFactory;
import com.sp.fanikiwa.business.SMS.MpesaDepositMessage;
import com.sp.fanikiwa.business.SMS.SMSProcessorComponent;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.business.messaging.SMSmessage;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.utils.DateDeserializer;
import com.sp.utils.DateExtension;
import com.sp.utils.DateSerializer;
import com.sp.utils.HttpUtil;

public class MpesaDepositServlet extends HttpServlet {

	final String FANIKIWATELNO = "0717769329";

	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessMpesa(req, resp);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessMpesa(req, resp);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void ProcessMpesa(HttpServletRequest request,
			HttpServletResponse response) throws ParseException {
		/*
		 * 1. verify the call is from safaricom 2. retrieve the deposit
		 * parameters and create double entry transactions 3. Post. i.e
		 * AccountEnpoint aep = new AccountEnpoint(); aep.DoubleEntry(de)
		 */
		MpesaDepositMessage message;
		try {
			String narr = "Deposit started...";
			
			// 1. Get sms from request and convert to MpesaDepositMessage
			message = MakeMpesaDepositMessageFromRequest(request);

			try {
			// 3.process the fanikiwa message

			
				List<Transaction> txns = TransactionFactory.MpesaDeposit(
						message.AccountId, message.Amount, narr,
						message.Mpesaref);
				TransactionPost.Post(txns);
				narr = message.CustomerTelno + " deposited on "
						+ message.MessageDate.toString();		
			} catch (Exception e) {
				narr = e.getMessage();
			}

			// 4. send the response back
				SendResponse(response, narr, message);
				
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private MpesaDepositMessage MakeMpesaDepositMessageFromRequest(
			HttpServletRequest request) throws IOException {

		// 1. get received JSON data from request
		BufferedReader br = new BufferedReader(new InputStreamReader(
				request.getInputStream()));
		String json = "";
		if (br != null) {
			json = br.readLine();
		}

		// 2. initiate jackson mapper
		//ObjectMapper mapper = new ObjectMapper();
		Gson gson = new GsonBuilder()
		.registerTypeAdapter(Date.class, new DateSerializer())
		.registerTypeAdapter(Date.class, new DateDeserializer()) 
		.enableComplexMapKeySerialization()
		.serializeNulls()
		.setPrettyPrinting()
		.create();
		 
		// 3. Convert received JSON to MpesaDepositMessage
		MpesaDepositMessage msg = gson.fromJson(json, MpesaDepositMessage.class);

//		msg.setBody(json);
		msg.setMessageDate(new Date());
		msg.setFanikiwaMessageType(FanikiwaMessageType.MpesaDepositMessage);
		msg.setStatus("NEW");
		msg.setSenderTelno("MPESA");

		return msg;
	}

	private void SendResponse(HttpServletResponse response, String result,
			SMSmessage sms) {
		// write to response -- FOR TESTING
		try {
			String msg = "To: " + sms.getAddressFrom() + "\nMessage: " + result;
			response.getWriter().println(msg);
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		// FOR LIVE
		/*
		 * SMSURL="https://smscompany/fanikiwaAcc/SMS" 1. smsrequest =
		 * create(SMSURL) 2. Json jsonresult = ConvertToJason(result,original)
		 * 3. smsrequest.getWriter().write(jsonresult)
		 */

	}
//
//	private static MpesaDepositMessage ParseMpesaMessage(
//			String OriginatingAddress, Date MessageDate, String Body)
//			throws ParseException {
//
//		MpesaDepositMessage mo = new MpesaDepositMessage();
//
//		mo.SenderTelno = OriginatingAddress;
//		mo.FanikiwaMessageType = FanikiwaMessageType.MpesaDepositMessage;
//		mo.Body = Body;
//		mo.Status = "NEW";
//
//		String[] fieldPairs = Body.toUpperCase().trim().split("\\,");
//
//		for (String field : fieldPairs) {
//			String[] f = field.split("\\:");
//			switch (f[0]) {
//			case "REF":
//				mo.Mpesaref = f[1];
//				break;
//			case "MPESADATE":
//				String dstr = f[1];
//				mo.SentDate = DateExtension.parse(dstr);
//				break;
//			case "AMOUNT":
//				mo.Amount = Double.parseDouble(f[1]);
//				break;
//			case "FROMTEL":
//				mo.CustomerTelno = f[1];
//				break;
//			case "ACCOUNTNO":
//				mo.AccountId = Long.parseLong(f[1]);
//				break;
//			case "BALANCE":
//				mo.Bal = Double.parseDouble(f[1]);
//				break;
//
//			}
//		}
//		return mo;
//	}
//
	private void SendResponse(HttpServletResponse response, String result,
			MpesaDepositMessage sms) {
		// write to response -- FOR TESTING
		try {
			String msg = "To: " + sms.CustomerTelno +"<br/>"+ "Message: " + result;
			response.getWriter().println(msg);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

		// FOR LIVE
		/*
		 * SMSURL="https://smscompany/fanikiwaAcc/SMS" 1. smsrequest =
		 * create(SMSURL) 2. Json jsonresult = ConvertToJason(result,original)
		 * 3. smsrequest.getWriter().write(jsonresult)
		 */

	}

}
