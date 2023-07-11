package com.sp.fanikiwa.business.messaging;

import java.io.Serializable;
import java.util.Date;

public abstract class Message implements Serializable {

	private Object body;

	// / <summary>
	// / Gets or sets addressFrom.
	// / </summary>
	private String addressFrom;

	// / <summary>
	// / Gets or sets addressTo.
	// / </summary>
	private String addressTo;

	// / <summary>
	// / Gets or sets a messageDate.
	// / </summary>
	private Date messageDate;

	
	
	public Object getBody() {
		return body;
	}

	public void setBody(Object body) {
		this.body = body;
	}

	public String getAddressFrom() {
		return addressFrom;
	}

	public void setAddressFrom(String addressFrom) {
		this.addressFrom = addressFrom;
	}

	public String getAddressTo() {
		return addressTo;
	}

	public void setAddressTo(String addressTo) {
		this.addressTo = addressTo;
	}

	public Date getMessageDate() {
		return messageDate;
	}

	public void setMessageDate(Date messageDate) {
		this.messageDate = messageDate;
	}

}