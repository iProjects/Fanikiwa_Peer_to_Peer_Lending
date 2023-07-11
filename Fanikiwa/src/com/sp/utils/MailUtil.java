package com.sp.utils;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.UnsupportedEncodingException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Properties;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.mail.Message;
import javax.mail.MessagingException;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.AddressException;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;
import javax.activation.DataHandler;
import javax.activation.DataSource;
import javax.mail.Multipart;
import javax.mail.internet.MimeBodyPart;
import javax.mail.internet.MimeMultipart;
import javax.mail.util.ByteArrayDataSource;

import com.sp.fanikiwa.MailHandlerServlet;

public class MailUtil {
	private static final Logger log = Logger.getLogger(MailHandlerServlet.class
			.getName());
	private static String KUFANIKIWAEMAIL = "info.kufanikiwa@gmail.com";

	public static void sendMail(String to, String subject, String Body)
			throws UnsupportedEncodingException {
		sendMail(KUFANIKIWAEMAIL, to, subject, Body);
	}

	public static void sendMail(String from, String to, String subject,
			String Body) throws UnsupportedEncodingException {

		String delimiters = "\\,+|\\;+";
		ArrayList<String> toList = new ArrayList<String>(Arrays.asList(to
				.split(delimiters)));
		sendMail(from, toList, subject, Body);
	}

	public static void sendMail(String from, List<String> toList,
			String subject, String Body) throws UnsupportedEncodingException {
		try {
			Message msg = SimpleMailMessage(from, toList, subject, Body);
			Transport.send(msg);

		} catch (AddressException e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		} catch (MessagingException e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		} catch (Exception e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		}
	}

	// public static void sendPDF(String toAddress,
	// String subject, String attchedfilename, String Body,
	// byte[] pdfAttacchment) throws MessagingException,
	// UnsupportedEncodingException{
	// String delimiters = "\\,+|\\;+";
	// ArrayList<String> toList = new
	// ArrayList<String>(Arrays.asList(toAddress.split(delimiters)));
	// sendMailWithAttachment(KUFANIKIWAEMAIL, toList,
	// subject, attchedfilename, Body,
	// pdfAttacchment, "application/pdf");
	// }
	public static void sendMailWithAttachment(String from, List<String> toList,
			String subject, String attchedfilename, String Body,
			byte[] attachmentData, String attachmenttype)
			throws MessagingException, UnsupportedEncodingException {

		Message message = SimpleMailMessage(from, toList, subject, Body);

		// add attachments
		Multipart mp = new MimeMultipart();
		MimeBodyPart attachment = new MimeBodyPart();
		attachment.setFileName(attchedfilename);
		InputStream attachmentDataStream = new ByteArrayInputStream(
				attachmentData);
		attachment.setContent(attachmentDataStream, attachmenttype);
		mp.addBodyPart(attachment);

		message.setContent(mp);
		Transport.send(message);
	}

	public static void sendEmailWithPDF(String recipient, String subject,
			String content,String filename, Class<?> pdf) {
		Properties props = new Properties();
		Session session = Session.getDefaultInstance(props, null);
		session.setDebug(true);

		String delimiters = "\\,+|\\;+";
		ArrayList<String> toList = new ArrayList<String>(
				Arrays.asList(recipient.split(delimiters)));

		ByteArrayOutputStream outputStream = null;

		try {
			//construct the text body part
            MimeBodyPart textBodyPart = new MimeBodyPart();
            textBodyPart.setText(content);
             
            //now write the PDF content to the output stream
            outputStream = new ByteArrayOutputStream();
            //writePdf(outputStream);
            PDFUtil.writePdf( outputStream,   pdf);
            byte[] bytes = outputStream.toByteArray();
             
            //construct the pdf body part
            DataSource dataSource = new ByteArrayDataSource(bytes, "application/pdf");
            MimeBodyPart pdfBodyPart = new MimeBodyPart();
            pdfBodyPart.setDataHandler(new DataHandler(dataSource));
            pdfBodyPart.setFileName(filename);
                         
            //construct the mime multi part
            MimeMultipart mimeMultipart = new MimeMultipart();
            mimeMultipart.addBodyPart(textBodyPart);
            mimeMultipart.addBodyPart(pdfBodyPart);
             
            //create the sender/recipient addresses
            InternetAddress iaSender = new InternetAddress(KUFANIKIWAEMAIL, "Fanikiwa Admin");
            InternetAddress iaRecipient = new InternetAddress(recipient);
             
            //construct the mime message
            MimeMessage mimeMessage = new MimeMessage(session);
            mimeMessage.setSender(iaSender);
            mimeMessage.setSubject(subject);
            mimeMessage.setRecipient(Message.RecipientType.TO, iaRecipient);
            mimeMessage.setContent(mimeMultipart);
             
            //send off the email
            Transport.send(mimeMessage);
		} catch (AddressException e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		} catch (MessagingException e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		} catch (UnsupportedEncodingException e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		} catch (Exception e) {
			log.log(Level.SEVERE, e.getMessage(),e);
		}
	}

	public static boolean isValidEmailAddress(String email) {
		boolean result = true;
		try {
			InternetAddress emailAddr = new InternetAddress(email);
			emailAddr.validate();
		} catch (AddressException ex) {
			result = false;
			log.log(Level.SEVERE, ex.getMessage(),ex);
		}
		return result;
	}

	public static boolean isValidEmailRegex(String email) {
		String EMAIL_REGEX = "^[\\w-_\\.+]*[\\w-_\\.]\\@([\\w]+\\.)+[\\w]+[\\w]$";
		Boolean b = email.matches(EMAIL_REGEX);
		return b;
	}

	private static MimeMessage SimpleMailMessage(String from,
			List<String> toList, String subject, String Body)
			throws MessagingException, UnsupportedEncodingException {
		Properties props = new Properties();
		Session session = Session.getDefaultInstance(props, null);
		Message msg = new MimeMessage(session);

		InternetAddress fromaddr = new InternetAddress(from, "Fanikiwa Admin");
		msg.setFrom(fromaddr);
		for (String to : toList) {
			InternetAddress toaddr = new InternetAddress(to);
			msg.addRecipient(Message.RecipientType.TO, toaddr);
		}
		msg.setSubject(subject);
		msg.setText(Body);

		return (MimeMessage) msg;
	}

}
