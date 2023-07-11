package com.sp.fanikiwa.entity;

import java.util.Date;

public class ActivationDTO {

	private Date activatedDate;
	private String email;
	private String telno;
	private String activationMethod; //M-Mobile app, S-sms or E-mail
	private String token;
	public Date getActivatedDate() {
		return activatedDate;
	}
	public void setActivatedDate(Date activatedDate) {
		this.activatedDate = activatedDate;
	}
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public String getTelno() {
		return telno;
	}
	public void setTelno(String telno) {
		this.telno = telno;
	}
	public String getActivationMethod() {
		return activationMethod;
	}
	public void setActivationMethod(String activationMethod) {
		this.activationMethod = activationMethod;
	}
	public String getToken() {
		return token;
	}
	public void setToken(String token) {
		this.token = token;
	}
	
}
