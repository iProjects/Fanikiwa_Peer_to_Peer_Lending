package com.sp.fanikiwa;

import java.io.IOException;
import java.text.ParseException;
import java.util.Collection;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.sp.fanikiwa.api.MpesaIPNMessageEndpoint;
import com.sp.fanikiwa.business.MpesaComponent;
import com.sp.fanikiwa.entity.MpesaIPNMessage;

public class MpesaProcessServelet extends HttpServlet {
	private static final Logger log = Logger.getLogger(MpesaProcessServelet.class.getName());
	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessDBMessages(req, resp);
		} catch (ParseException e) {
			log.log(Level.SEVERE,e.getMessage(),e);
		} catch(Exception e){
			log.log(Level.SEVERE,e.getMessage(),e);
		}
	}

	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		try {
			ProcessDBMessages(req, resp);
		} catch (ParseException e) {
			log.log(Level.SEVERE,e.getMessage(),e);
		} catch(Exception e){
			log.log(Level.SEVERE,e.getMessage(),e);
		}
	}

	private void ProcessDBMessages(HttpServletRequest request,
			HttpServletResponse response) throws ParseException, IOException {
		
		log.info("Processing Started at "+(new Date()).toString()+"....");
		ProcessDBMessagesNow(request, response);
		log.info("Processed finished at "+(new Date()).toString()+".");
	}
	
	private void ProcessDBMessagesNow(HttpServletRequest request,
			HttpServletResponse response) throws ParseException, IOException {
		try {
				// make sure the message is not processed twice
				// get all messages where status = 'New'
				MpesaIPNMessageEndpoint mep = new MpesaIPNMessageEndpoint();
				Collection<MpesaIPNMessage> mpesaMsgs = mep
						.ListNewMpesaIPNMessages(null, null).getItems();

				log.info("STEP 3: Processing "+ mpesaMsgs.size() + " new messages..." );
				for (MpesaIPNMessage mpesaMsg : mpesaMsgs) {

					log.info("STEP 3.1: Processing mgsid["+ mpesaMsg.getMpesa_code() + "] ..." );
					MpesaComponent mcomp = new MpesaComponent();
					String str = mcomp.ProcessMessage(mpesaMsg);
					log.info("STEP 3.2: Processed mgsid["+ mpesaMsg.getMpesa_code() + "] ...\nResult =" + str );
					
					// 5. update the processed messaged status so it is not
					// processed again.
					mpesaMsg.setStatus("Processed");
					mep.updateMpesaIPNMessage(mpesaMsg);
					log.info("STEP 3.3: DB Status of message id["+ mpesaMsg.getMpesa_code() + "] changed to Processed" );
					
				}

		} catch (Exception e) {
			// TODO Auto-generated catch block
			log.log(Level.SEVERE,e.getMessage(),e);
		}
	}


}

