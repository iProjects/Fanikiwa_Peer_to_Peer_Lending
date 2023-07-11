package com.sp.fanikiwa.entity;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class MpesaTestIPNMessage {
	
	@Id
	Long id ;
	@Index
	private String mpesaIPNMessageID; //	This is the IPN notification unique number 	: Eg. 	100 
	private String orig 	; //	This will be source of the notification. 	: Eg. 	MPESA 
	@Index
	private String dest 	; //	This will be same as your business terminal Msisdn	: Eg. 	254722123456 
	private String tstamp 	; //	This is the timestamp for when the IPN notification was received from MPESA. 	: Eg. 	2011-04-26 12:47:31.0 
	private String text 	; //	This is the full text message as received from MPESA.	: Eg. 	This is will be the full SMS message. 
	private String user 	; //	If you provide us a username and password to access your URL this parameter will contain the username	: Eg. 	Username for Developer URL 
	private String pass 	; //	If you provide us a username and password to access your URL this parameter will contain the password. 	: Eg. 	Password for Developer URL 
	@Index private String mpesa_code 	; //	The MPESA transaction code. 	: Eg. 	BI55EQ862 
	@Index private String mpesa_acc 	; //	The Account as entered by the subscriber 	: Eg. 	TEST 
	@Index private String mpesa_msisdn 	; //	The mobile number of the subscriber that has sent the funds 	: Eg. 	254722123456
	@Index private String mpesa_trx_date 	; //	The transaction date 	: Eg. 	26/4/11 
	private String mpesa_trx_time 	; //	The transaction time 	: Eg. 	1:03 PM 
	private Double mpesa_amt 	; //	The transaction amount 	: Eg. 	50.0 
	@Index private String mpesa_sender 	; //	The name of the sender (subscriber). 	: Eg. 	ALFRED
	@Index
	private String status; 
	
	 
	public Long getId() {
		return id;
	}
	public void setId(Long id) {
		this.id = id;
	}
	public String getOrig() {
		return orig;
	}
	public void setOrig(String orig) {
		this.orig = orig;
	}
	public String getDest() {
		return dest;
	}
	public void setDest(String dest) {
		this.dest = dest;
	}
	public String getTstamp() {
		return tstamp;
	}
	public void setTstamp(String tstamp) {
		this.tstamp = tstamp;
	}
	public String getText() {
		return text;
	}
	public void setText(String text) {
		this.text = text;
	}
	public String getUser() {
		return user;
	}
	public void setUser(String user) {
		this.user = user;
	}
	public String getPass() {
		return pass;
	}
	public void setPass(String pass) {
		this.pass = pass;
	}
	public String getMpesa_code() {
		return mpesa_code;
	}
	public void setMpesa_code(String mpesa_code) {
		this.mpesa_code = mpesa_code;
	}
	public String getMpesa_acc() {
		return mpesa_acc;
	}
	public void setMpesa_acc(String mpesa_acc) {
		this.mpesa_acc = mpesa_acc;
	}
	public String getMpesa_msisdn() {
		return mpesa_msisdn;
	}
	public void setMpesa_msisdn(String mpesa_msisdn) {
		this.mpesa_msisdn = mpesa_msisdn;
	}
	public String getMpesa_trx_date() {
		return mpesa_trx_date;
	}
	public void setMpesa_trx_date(String mpesa_trx_date) {
		this.mpesa_trx_date = mpesa_trx_date;
	}
	public String getMpesa_trx_time() {
		return mpesa_trx_time;
	}
	public void setMpesa_trx_time(String mpesa_trx_time) {
		this.mpesa_trx_time = mpesa_trx_time;
	}
	public Double getMpesa_amt() {
		return mpesa_amt;
	}
	public void setMpesa_amt(Double mpesa_amt) {
		this.mpesa_amt = mpesa_amt;
	}
	public String getMpesa_sender() {
		return mpesa_sender;
	}
	public void setMpesa_sender(String mpesa_sender) {
		this.mpesa_sender = mpesa_sender;
	}
	public String getStatus() {
		return status;
	}
	public void setStatus(String status) {
		this.status = status;
	}
	public String getMpesaIPNMessageID() {
		return mpesaIPNMessageID;
	}
	public void setMpesaIPNMessageID(String mpesaIPNMessageID) {
		this.mpesaIPNMessageID = mpesaIPNMessageID;
	}
	


}
