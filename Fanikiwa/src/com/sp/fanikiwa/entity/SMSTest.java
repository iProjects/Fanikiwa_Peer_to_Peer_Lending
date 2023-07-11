package com.sp.fanikiwa.entity;

import com.googlecode.objectify.annotation.Entity;

@Entity
public class SMSTest {
	public String Telno;
	public String Body;
	public String Response;
	
	public String getTelno() {
		return Telno;
	}
	public void setTelno(String telno) {
		Telno = telno;
	}
	public String getBody() {
		return Body;
	}
	public void setBody(String body) {
		Body = body;
	}
	public String getResponse() {
		return Response;
	}
	public void setResponse(String response) {
		Response = response;
	}

}
