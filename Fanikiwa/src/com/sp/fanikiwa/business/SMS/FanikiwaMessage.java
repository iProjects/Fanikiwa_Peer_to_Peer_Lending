package com.sp.fanikiwa.business.SMS;

import java.util.Date;
import com.sp.fanikiwa.Enums.FanikiwaMessageType;

public abstract class FanikiwaMessage {

	public Long MemberId;
	public String SenderTelno;
	public Date MessageDate;
	public FanikiwaMessageType FanikiwaMessageType;
	public String Body;
	public String Status;

	public Long getMemberId() {
		return MemberId;
	}

	public void setMemberId(Long memberId) {
		MemberId = memberId;
	}

	public String getSenderTelno() {
		return SenderTelno;
	}

	public void setSenderTelno(String senderTelno) {
		SenderTelno = senderTelno;
	}

	public Date getMessageDate() {
		return MessageDate;
	}

	public void setMessageDate(Date messageDate) {
		MessageDate = messageDate;
	}

	public FanikiwaMessageType getFanikiwaMessageType() {
		return FanikiwaMessageType;
	}

	public void setFanikiwaMessageType(FanikiwaMessageType fanikiwaMessageType) {
		FanikiwaMessageType = fanikiwaMessageType;
	}

	public String getBody() {
		return Body;
	}

	public void setBody(String body) {
		Body = body;
	}

	public String getStatus() {
		return Status;
	}

	public void setStatus(String status) {
		Status = status;
	}
}
