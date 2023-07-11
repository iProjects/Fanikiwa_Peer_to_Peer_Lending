package com.sp.fanikiwa.business;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.sp.fanikiwa.Enums.FanikiwaMessageType;
import com.sp.fanikiwa.business.SMS.MpesaDepositMessage;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.MpesaIPNMessage;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferDTO;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.utils.DateDeserializer;
import com.sp.utils.DateSerializer;

public class MpesaComponent {
 
	public String ProcessMessage(MpesaIPNMessage mpesaIPNMessage) throws Exception {
  
			MpesaDepositMessage message = MakeMpesaDepositMessageFromRequest(mpesaIPNMessage);
		    String msg = "Deposit started...";
		    String narr = mpesaIPNMessage.getMpesa_code() + " from " + mpesaIPNMessage.getMpesa_msisdn();
				 
			List<Transaction> txns = TransactionFactory.MpesaDeposit(
					message.AccountId, message.Amount, narr,
					message.Mpesaref);
			TransactionPost.Post(txns);
			msg += message.CustomerTelno + " deposited on "
					+ message.MessageDate.toString();		
			 return msg;
	}
	
	private MpesaDepositMessage MakeMpesaDepositMessageFromRequest(MpesaIPNMessage mpesaIPNMessage) throws ParseException{
		
		MpesaDepositMessage msg = new MpesaDepositMessage();  
		
		msg.setCustomerTelno(mpesaIPNMessage.getMpesa_msisdn());
		msg.setAccountId(Long.parseLong(mpesaIPNMessage.getMpesa_acc()));
		msg.setAmount(mpesaIPNMessage.getMpesa_amt());
		msg.setMpesaref(mpesaIPNMessage.getMpesa_code()); 
		SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy");
		msg.setSentDate(sdf.parse(mpesaIPNMessage.getMpesa_trx_date()));
//		msg.setMpesaBal(mpesaIPNMessage.Bal);
//		msg.setMemberId(fmessage.MemberId); 
		msg.setSenderTelno(mpesaIPNMessage.getMpesa_msisdn());
		msg.setMessageDate(new Date());
		msg.setFanikiwaMessageType(FanikiwaMessageType.MpesaDepositMessage);
		msg.setBody(mpesaIPNMessage.getText()); 
		msg.setStatus("NEW");
		 
		return msg;
	}
	
}
