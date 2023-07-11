package com.sp.fanikiwa.entity;

import java.util.Date;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Fanikiwamessage {

	@Id
	Long id;
 
	private String accountId;
 
	private double amount;
 
	private String BE_AccLabel;
 
	private String body;
 
	private String command;
 
	private String CP_ConfirmPassword;
 
	private String CP_NewPassword;
 
	private String customerTelno;
 
	private String email;
 
	private Date endDate;
 
	private String exception;
 
	private String HM_Param;
 
	private int memberId;
 
	private Date messageDate;
 
	private int messageType;
 
	private double MO_Interest;
 
	private int MO_Term;
 
	private double mpesaBal;
 
	private String mpesaRef;
 
	private Date mpesaSentDate;
 
	private String nationalID;
 
	private String notificationMethod;
 
	private int offerId;
 
	private String pwd;
 
	private String senderTelno;
 
	private Date ST_EndDate;
 
	private Date ST_StartDate;
 
	private Date startDate;
 
	private String status;

	public Fanikiwamessage() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getAccountId() {
		return this.accountId;
	}

	public void setAccountId(String accountId) {
		this.accountId = accountId;
	}

	public double getAmount() {
		return this.amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}

	public String getBE_AccLabel() {
		return this.BE_AccLabel;
	}

	public void setBE_AccLabel(String BE_AccLabel) {
		this.BE_AccLabel = BE_AccLabel;
	}

	public String getBody() {
		return this.body;
	}

	public void setBody(String body) {
		this.body = body;
	}

	public String getCommand() {
		return this.command;
	}

	public void setCommand(String command) {
		this.command = command;
	}

	public String getCP_ConfirmPassword() {
		return this.CP_ConfirmPassword;
	}

	public void setCP_ConfirmPassword(String CP_ConfirmPassword) {
		this.CP_ConfirmPassword = CP_ConfirmPassword;
	}

	public String getCP_NewPassword() {
		return this.CP_NewPassword;
	}

	public void setCP_NewPassword(String CP_NewPassword) {
		this.CP_NewPassword = CP_NewPassword;
	}

	public String getCustomerTelno() {
		return this.customerTelno;
	}

	public void setCustomerTelno(String customerTelno) {
		this.customerTelno = customerTelno;
	}

	public String getEmail() {
		return this.email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public Date getEndDate() {
		return this.endDate;
	}

	public void setEndDate(Date endDate) {
		this.endDate = endDate;
	}

	public String getException() {
		return this.exception;
	}

	public void setException(String exception) {
		this.exception = exception;
	}

	public String getHM_Param() {
		return this.HM_Param;
	}

	public void setHM_Param(String HM_Param) {
		this.HM_Param = HM_Param;
	}

	public int getMemberId() {
		return this.memberId;
	}

	public void setMemberId(int memberId) {
		this.memberId = memberId;
	}

	public Date getMessageDate() {
		return this.messageDate;
	}

	public void setMessageDate(Date messageDate) {
		this.messageDate = messageDate;
	}

	public int getMessageType() {
		return this.messageType;
	}

	public void setMessageType(int messageType) {
		this.messageType = messageType;
	}

	public double getMO_Interest() {
		return this.MO_Interest;
	}

	public void setMO_Interest(double MO_Interest) {
		this.MO_Interest = MO_Interest;
	}

	public int getMO_Term() {
		return this.MO_Term;
	}

	public void setMO_Term(int MO_Term) {
		this.MO_Term = MO_Term;
	}

	public double getMpesaBal() {
		return this.mpesaBal;
	}

	public void setMpesaBal(double mpesaBal) {
		this.mpesaBal = mpesaBal;
	}

	public String getMpesaRef() {
		return this.mpesaRef;
	}

	public void setMpesaRef(String mpesaRef) {
		this.mpesaRef = mpesaRef;
	}

	public Date getMpesaSentDate() {
		return this.mpesaSentDate;
	}

	public void setMpesaSentDate(Date mpesaSentDate) {
		this.mpesaSentDate = mpesaSentDate;
	}

	public String getNationalID() {
		return this.nationalID;
	}

	public void setNationalID(String nationalID) {
		this.nationalID = nationalID;
	}

	public String getNotificationMethod() {
		return this.notificationMethod;
	}

	public void setNotificationMethod(String notificationMethod) {
		this.notificationMethod = notificationMethod;
	}

	public int getOfferId() {
		return this.offerId;
	}

	public void setOfferId(int offerId) {
		this.offerId = offerId;
	}

	public String getPwd() {
		return this.pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}

	public String getSenderTelno() {
		return this.senderTelno;
	}

	public void setSenderTelno(String senderTelno) {
		this.senderTelno = senderTelno;
	}

	public Date getST_EndDate() {
		return this.ST_EndDate;
	}

	public void setST_EndDate(Date ST_EndDate) {
		this.ST_EndDate = ST_EndDate;
	}

	public Date getST_StartDate() {
		return this.ST_StartDate;
	}

	public void setST_StartDate(Date ST_StartDate) {
		this.ST_StartDate = ST_StartDate;
	}

	public Date getStartDate() {
		return this.startDate;
	}

	public void setStartDate(Date startDate) {
		this.startDate = startDate;
	}

	public String getStatus() {
		return this.status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

}