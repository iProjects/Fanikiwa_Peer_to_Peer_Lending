package com.sp.fanikiwa;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.text.DateFormat;
import java.text.ParseException;
import java.util.Collection;
import java.util.Date;
import java.util.List;

import javax.servlet.ServletRequest;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.gson.Gson;
import com.sp.fanikiwa.Enums.FanikiwaMessageType;
import com.sp.fanikiwa.api.MpesaIPNTestMessageEndpoint;
import com.sp.fanikiwa.business.messaging.SMSmessage;
import com.sp.fanikiwa.entity.MpesaTestIPNMessage;
import com.sp.utils.DateExtension;
import com.sp.utils.HttpUtil;

public class MpesaIPNTestServlet extends HttpServlet {

	// final String SAFARICOM_SERVER_NAME = "172.29.229.171";
	// final Integer SAFARICOM_SERVER_PORT = 8080;
	// final String KUFANIKIWA_USER_NAME = "user";
	// final String KUFANIKIWA_PWD = "pwd";

	final String SAFARICOM_SERVER_NAME = "http://www."
			+ "kufanikiwa.co.ke/Mpesa";
	final Integer SAFARICOM_SERVER_PORT = 8888;
	final String KUFANIKIWA_USER_NAME = "123";
	final String KUFANIKIWA_PWD = "123";

	// final String SAFARICOM_SERVER_NAME = "localhost";
	// final Integer SAFARICOM_SERVER_PORT = 8888;
	// final String KUFANIKIWA_USER_NAME = "123";
	// final String KUFANIKIWA_PWD = "123";

	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessIPNNotification(req, resp);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessIPNNotification(req, resp);
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private void ProcessIPNNotification(HttpServletRequest request,
			HttpServletResponse response) throws ParseException {

		try {

			// 1. Verify message is actually from MPESA
			// if (isMessageFromSafaricom(request)) {
			if (isMessageFromSafaricomMock(request)) {

				// 2. Get message
				MpesaTestIPNMessage msg = GetMpeasaIPNMessageFromRequest(request);

				// 3. Save message
				MpesaIPNTestMessageEndpoint mep = new MpesaIPNTestMessageEndpoint();

				// make sure no duplicate safcomid
				MpesaTestIPNMessage exists = mep
						.isMpesaTestIPNMessageExists(msg);
				if (exists == null) {

					mep.insertMpesaTestIPNMessage(msg);
				}

			} else {
				// This is an impostor; Log the message for audit purposes
			}

		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}

	private boolean isMessageFromSafaricom(HttpServletRequest request) {

		boolean isMessageFromSafaricom = false;
		String server = request.getServerName();
		Integer port = request.getServerPort();
		String user = request.getParameter("user");
		String pass = request.getParameter("pass");

		// compare all the above to what is known and return true if all match
		if (server.equals(SAFARICOM_SERVER_NAME)
				&& port.equals(SAFARICOM_SERVER_PORT)
				&& user.equals(KUFANIKIWA_USER_NAME)
				&& pass.equals(KUFANIKIWA_PWD))
			isMessageFromSafaricom = true;

		return isMessageFromSafaricom;
	}

	private boolean isMessageFromSafaricomMock(HttpServletRequest request) {
		return true;
	}

	private MpesaTestIPNMessage GetMpeasaIPNMessageFromRequest(
			HttpServletRequest request) {
		MpesaTestIPNMessage msg = new MpesaTestIPNMessage();
		msg.setMpesaIPNMessageID(request.getParameter("id"));
		msg.setOrig(request.getParameter("orig"));
		msg.setDest(request.getParameter("dest"));
		msg.setTstamp(request.getParameter("tstamp"));
		msg.setText(request.getParameter("text"));
		msg.setUser(request.getParameter("user"));
		msg.setPass(request.getParameter("pass"));
		msg.setMpesa_code(request.getParameter("mpesa_code"));
		msg.setMpesa_acc(request.getParameter("mpesa_acc"));
		msg.setMpesa_msisdn(request.getParameter("mpesa_msisdn"));
		msg.setMpesa_trx_date(request.getParameter("mpesa_trx_date"));
		msg.setMpesa_trx_time(request.getParameter("mpesa_trx_time"));
		msg.setMpesa_amt(Double.parseDouble(request.getParameter("mpesa_amt")));
		msg.setMpesa_sender(request.getParameter("mpesa_sender"));
		msg.setStatus("New");

		return msg;
	}

}
