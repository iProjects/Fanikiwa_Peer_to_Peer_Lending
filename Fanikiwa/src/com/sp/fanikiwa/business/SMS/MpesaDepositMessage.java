package com.sp.fanikiwa.business.SMS;

import java.util.Date;

public class MpesaDepositMessage extends FanikiwaMessage {
	
	public String CustomerTelno;
	public Long AccountId;
	public double Amount;
	public String Mpesaref;
	public Date SentDate;
	public double Bal;

	public MpesaDepositMessage() {

	}

	public MpesaDepositMessage(Long acc, double amount, String customerTelno) {
		this.Amount = amount;
		this.AccountId = acc;
		this.CustomerTelno = customerTelno;
	}

	public String getCustomerTelno() {
		return CustomerTelno;
	}

	public void setCustomerTelno(String customerTelno) {
		CustomerTelno = customerTelno;
	}

	public Long getAccountId() {
		return AccountId;
	}

	public void setAccountId(Long accountId) {
		AccountId = accountId;
	}

	public double getAmount() {
		return Amount;
	}

	public void setAmount(double amount) {
		Amount = amount;
	}

	public String getMpesaref() {
		return Mpesaref;
	}

	public void setMpesaref(String mpesaref) {
		Mpesaref = mpesaref;
	}

	public Date getSentDate() {
		return SentDate;
	}

	public void setSentDate(Date sentDate) {
		SentDate = sentDate;
	}

	public double getBal() {
		return Bal;
	}

	public void setBal(double bal) {
		Bal = bal;
	}
}
