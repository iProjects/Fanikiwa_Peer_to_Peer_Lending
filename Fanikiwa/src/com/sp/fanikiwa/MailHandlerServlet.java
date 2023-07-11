package com.sp.fanikiwa;

import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.util.Properties;
import java.util.logging.Logger;

import javax.mail.MessagingException;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.MimeMessage;
import javax.servlet.http.*;

import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.api.InformdbEndpoint;
import com.sp.fanikiwa.entity.Informdb;
import com.sp.utils.MailUtil;

public class MailHandlerServlet extends HttpServlet {
	private static final Logger log = Logger.getLogger(MailHandlerServlet.class
			.getName());

	@Override
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		Properties props = new Properties();
		Session session = Session.getDefaultInstance(props, null);
		try {
			MimeMessage message = new MimeMessage(session, req.getInputStream());
			// save the mail
			Informdb mail = new Informdb();
			mail.setAddressFrom(message.getSender().toString());
			mail.setSubject(message.getSubject());
			mail.setBody(message.toString());
			mail.setMessageDate(message.getReceivedDate());
			mail.setStatus("New");

			InformdbEndpoint idep = new InformdbEndpoint();
			idep.insertInformdb(mail);
			
			//Acknowledge
			MailUtil.sendMail(message.getSender().toString(), "Acknowlegment", 
					"Dear "+message.getSender().toString()
					+"\n\nWe have recieved your email with thanks.  Fanikiwa Team will get back to you ASAP.\nRegards Fanikiwa");

		} catch (AddressException e) {
			log.severe(e.getMessage());
		} catch (MessagingException e) {
			log.severe(e.getMessage());
		} catch (UnsupportedEncodingException e) {
			log.severe(e.getMessage());
		} catch (NotFoundException e) {
			log.severe(e.getMessage());
		} catch (ConflictException e) {
			log.severe(e.getMessage());
		} catch (Exception e) {
			log.severe(e.getMessage());
		}
	}

}