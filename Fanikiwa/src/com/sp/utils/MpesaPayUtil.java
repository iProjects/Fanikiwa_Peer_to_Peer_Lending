package com.sp.utils;

import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;
import java.io.OutputStream;
import java.net.URL;
import java.security.KeyStore;
import java.security.PublicKey;
import java.security.SecureRandom;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Random;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.KeyManagerFactory;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.TrustManagerFactory;




import com.sp.utils.CryptoUtil;
import com.sp.fanikiwa.business.DiaryComponent;


//import com.sun.org.apache.xerces.internal.impl.dv.util.Base64; - NOT SUPPORTED ON GAE
import org.apache.commons.codec.binary.Base64;

public class MpesaPayUtil {
	
	private static final Logger log = Logger.getLogger(MpesaPayUtil.class.getName());

	//from safaricom
	final static String ORIGINATOR_CONVERSATION_ID = ""; 
	final static String MPESA_URL = "https://IPADDRESS/mminterface/request";
	final static String SOAP_ACTION_URL = "https://IPADDRESS/mminterface/request";
	final static String SPID = "1000150"; 
	final static String SERVICE_ID = "10001502";
	
	public static boolean PostToMpesaMock(double Amount, String MobileNo)
	{
		String msg = "Posted to Mpesa Mock MobileNo["+MobileNo+"] and Amount["+Amount+"]";
		log.info(msg);
		return true;
	}
	public static void PostToMpesa(double Amount, String MobileNo)
			throws Exception {

		Date dNow = new Date();
		SimpleDateFormat ft = new SimpleDateFormat("yyyyMMddHHmmss"); // ("E yyyyMMddhhmmss a zzz");
		Date dNow1 = new Date();
		SimpleDateFormat ft1 = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss"); // ("E yyyyMMddhhmmss a zzz");
		String FinalDate = ft1.format(dNow1);
		System.out.println("Current Date: " + ft1.format(dNow1));
		FinalDate = FinalDate.substring(0, 10) + "T"
				+ FinalDate.substring(10, 19) + "Z";
		System.out.println("Final Date: " + FinalDate);
		FinalDate = FinalDate.replace(" ", "");
		System.out.println("Final Date: " + FinalDate);
		SimpleDateFormat ft2 = new SimpleDateFormat("HH:mm:ss.0000000");
		String converted = ft1.format(dNow1) + "T" + ft2.format(dNow1) + "Z";
		String myCertPassword = "";

		String spPassword = "Password0!";// "Abcd123!@#";//"COOPbank@01";//"COOP@100001";
		String spPassword1 = "Password01";
		String mypassword = "";
		mypassword = SPID + spPassword + ft.format(dNow);
		String s = "";
		String a = CryptoUtil.hash256(mypassword);
		a = a.toUpperCase();
		s = javax.xml.bind.DatatypeConverter.printBase64Binary(a
				.getBytes("UTF-8")); // ("UTF-8")
		
		double Ran = 0;
		Random rand = new Random();
		Ran = rand.nextDouble();
		String Myrandom = Double.toString(Ran);
		Myrandom = Myrandom.replace("0.", "");
		Myrandom = Myrandom.replace(".", "");
		int Ranlen = Myrandom.length();
		// String MyAmount ="1500";
		String message = "";
		byte[] messageBytes;
		byte[] tempPub = null;
		String sPub = null;
		byte[] ciphertextBytes = null;
		byte[] textBytes = null;
		try {
			if (!CryptoUtil.areKeysPresent()) {
				CryptoUtil.generateKey();
			}
			final String originalText = spPassword1;
			ObjectInputStream inputStream = null;
			final PublicKey publicKey = null;

			InputStream inStream = new FileInputStream(
					"C:\\B2C Latest\\Post B2C Transactions\\PostB2C/testib.cer");
			CertificateFactory cf = CertificateFactory.getInstance("X.509");
			X509Certificate cert = (X509Certificate) cf
					.generateCertificate(inStream);
			inStream.close();
			PublicKey pubkey = (PublicKey) cert.getPublicKey();
			final byte[] cipherText =CryptoUtil.encrypt(originalText, pubkey);
			tempPub = pubkey.getEncoded();
			sPub = new String(tempPub);
			myCertPassword = new String(Base64.encodeBase64String(cipherText));
		} catch (Exception e) {
			System.out.println(e);
			log.log(Level.SEVERE, e.getMessage(),e);
		}
		String send = "<soap:Envelope xmlns:mrns0=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:req=\"http://api-v1.gen.mm.vodafone.com/mminterface/request\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\">"
				+ "<soap:Header>"
				+ "<tns:RequestSOAPHeader xmlns:tns=\"http://www.huawei.com/schema/osg/common/v2_1\">"
				+ "<tns:spId>"
				+ SPID
				+ "</tns:spId>"
				+ "<tns:spPassword>"
				+ s
				+ "</tns:spPassword>"
				+ "<tns:serviceId>"
				+ SERVICE_ID
				+ "</tns:serviceId>"
				+ "<tns:timeStamp>"
				+ ft.format(dNow)
				+ "</tns:timeStamp>"
				+ "</tns:RequestSOAPHeader>"
				+ "</soap:Header>"
				+ "<soap:Body>"
				+ "<req:RequestMsg><![CDATA[<?xml version=\"1.0\" encoding=\"UTF8\"?>"
				+ "<request xmlns=\"http://api-v1.gen.mm.vodafone.com/mminterface/request\">"
				+ "<Transaction>"
				+ "<CommandID>BusinessPayment</CommandID>"
				+ "<LanguageCode></LanguageCode>"
				+ "<OriginatorConversationID>"
				+ ORIGINATOR_CONVERSATION_ID
				+ "</OriginatorConversationID>"
				+ "<ConversationID></ConversationID>"
				+ "<Remark>Remark0</Remark>"
				+ "<EncryptedParameters>EncryptedParameters0</EncryptedParameters>"
				+ "<Parameters>"
				+ "<Parameter>"
				+ "<Key>Amount</Key>"
				+ "<Value>"
				+ Amount
				+ "</Value>"
				+ "</Parameter>"
				+ "</Parameters>"
				+ "<ReferenceData>"
				+ "<ReferenceItem>"
				+ "<Key>QueueTimeoutURL</Key>"
				+ "<Value>http://IPADDRESS:8080/C2BWebService/C2B</Value>"
				+ "</ReferenceItem>"
				+ "</ReferenceData>"
				+ "<Timestamp>"
				+ FinalDate
				+ "</Timestamp>"
				+ "</Transaction>"
				+ "<Identity>"
				+ "<Caller>"
				+ "<CallerType>2</CallerType>"
				+ "<ThirdPartyID>ThirdPartyID0</ThirdPartyID>"
				+ "<Password>Password0</Password>"
				+ "<CheckSum>CheckSum0</CheckSum>"
				+ "<ResultURL>http://IPADDRESS:8080/C2BWebService/C2B</ResultURL>"
				+ "</Caller>"
				+ "<Initiator>"
				+ "<IdentifierType>11</IdentifierType>"
				+ "<Identifier>BANK12</Identifier>"
				+ "<SecurityCredential>"
				+ myCertPassword
				+ "</SecurityCredential>"
				+ "<ShortCode>1000150</ShortCode>"
				+ "</Initiator>"
				+ "<PrimaryParty>"
				+ "<IdentifierType>1</IdentifierType>"
				+ "<Identifier>BANK12</Identifier>"
				+ "<ShortCode></ShortCode>"
				+ "</PrimaryParty>"
				+ "<ReceiverParty>"
				+ "<IdentifierType>1</IdentifierType>"
				+ "<Identifier>"
				+ MobileNo
				+ "</Identifier>"
				+ "<ShortCode></ShortCode>"
				+ "</ReceiverParty>"
				+ "<AccessDevice>"
				+ "<IdentifierType>1</IdentifierType>"
				+ "<Identifier>Identifier3</Identifier>"
				+ "</AccessDevice>"
				+ "</Identity>"
				+ "<KeyOwner>1</KeyOwner>"
				+ "</request>]]>"
				+ "</req:RequestMsg>" + "</soap:Body>" + "</soap:Envelope>";


//TODO rethink how to do this without use of FILE

		KeyManagerFactory kmf = KeyManagerFactory.getInstance("SunX509",
				"SunJSSE");
		KeyStore ks = KeyStore.getInstance("JKS");
		// File cert = new File("D:\\certs\\client.jks");
		File cert = new File("C:\\B2C Latest\\test.jks");
		InputStream stream = new FileInputStream(cert);
		ks.load(stream, "TEST".toCharArray());
		stream.close();
		kmf.init(ks, "TEST".toCharArray());

		TrustManagerFactory tmf = TrustManagerFactory.getInstance("SunX509");
		KeyStore trustks = KeyStore.getInstance("JKS");
		File trustcert = new File("C:\\B2C Latest\\test.jks");
		InputStream truststream = new FileInputStream(trustcert);
		trustks.load(truststream, "TEST".toCharArray());
		truststream.close();
		tmf.init(trustks);

		SSLContext context = SSLContext.getInstance("TLSv1");
		context.init(kmf.getKeyManagers(), tmf.getTrustManagers(),
				new SecureRandom());
		// context.init(kmf.getTrustManagers(), tmf.getTrustManagers(), null);

		SSLSocketFactory factory = context.getSocketFactory();
		URL urlc = new URL(MPESA_URL);

		HttpsURLConnection connection = (HttpsURLConnection) urlc
				.openConnection();
		// connection.setHostnameVerifier(new NullHostNameVerifier());

		HttpsURLConnection.setDefaultSSLSocketFactory(context
				.getSocketFactory());
		// sslFactory = context.getSocketFactory();
		HttpsURLConnection con = (HttpsURLConnection) urlc.openConnection();

		ByteArrayOutputStream b2cmsg = new ByteArrayOutputStream();
		byte[] buffer = new byte[send.length()];
		buffer = send.getBytes();
		b2cmsg.write(buffer);
		byte[] b = b2cmsg.toByteArray();

		String soapAction = SOAP_ACTION_URL;
		con.setRequestProperty("Content-Length", String.valueOf(b.length));
		con.setRequestProperty("Content-Type", "text/xml; charset=utf-8");
		con.setRequestProperty("SOAPAction", soapAction);
		// con.setSSLSocketFactory(sslFactory);
		con.setRequestMethod("POST");
		con.setDoOutput(true);
		con.setDoInput(true);
		OutputStream out = con.getOutputStream();
		out.write(b);
		out.close();
		String responseString = "", outputString = "";
		InputStreamReader isr = new InputStreamReader(con.getInputStream());
		BufferedReader in = new BufferedReader(isr);
		while ((responseString = in.readLine()) != null) {
			outputString = outputString + responseString;
		}
		// ps.write(buffer, 0, buffer.length);

		log.info("request message: \n" + send);
		log.info("Response message: \n" + outputString);
	}
}
