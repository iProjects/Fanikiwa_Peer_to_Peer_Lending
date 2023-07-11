package com.sp.fanikiwa.entity;

import javax.jdo.annotations.IdGeneratorStrategy;
import javax.jdo.annotations.IdentityType;
import javax.jdo.annotations.PersistenceCapable;
import javax.jdo.annotations.Persistent;
import javax.jdo.annotations.PrimaryKey;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Messagestore {

	@Id
	Long id;

 
	private String amPm;
 
	private String dateReceived;
 
	private String dateSent;
 
	private String firstName;
 
	private byte isDeleted;
 
	private String lastName;
 
	private String messageBody;

 
	private String messageIndex;
 
	private String messageStatus;
 
	private String messageType;
 
	private double mpesaAmount;
 
	private double mpesaBalance;
 
	private String originatingAddress;
 
	private String originatingAddressType;
 
	private String phoneNo;
 
	private byte processed;
 
	private String SCTimestamp;
 
	private String smscAddress;
 
	private String smscAddressType;
 
	private String status;
 
	private String storage;
 
	private String timeSent;
 
	private String userDataText;

	public Messagestore() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public String getAmPm() {
		return this.amPm;
	}

	public void setAmPm(String amPm) {
		this.amPm = amPm;
	}

	public String getDateReceived() {
		return this.dateReceived;
	}

	public void setDateReceived(String dateReceived) {
		this.dateReceived = dateReceived;
	}

	public String getDateSent() {
		return this.dateSent;
	}

	public void setDateSent(String dateSent) {
		this.dateSent = dateSent;
	}

	public String getFirstName() {
		return this.firstName;
	}

	public void setFirstName(String firstName) {
		this.firstName = firstName;
	}

	public byte getIsDeleted() {
		return this.isDeleted;
	}

	public void setIsDeleted(byte isDeleted) {
		this.isDeleted = isDeleted;
	}

	public String getLastName() {
		return this.lastName;
	}

	public void setLastName(String lastName) {
		this.lastName = lastName;
	}

	public String getMessageBody() {
		return this.messageBody;
	}

	public void setMessageBody(String messageBody) {
		this.messageBody = messageBody;
	}

	public String getMessageIndex() {
		return this.messageIndex;
	}

	public void setMessageIndex(String messageIndex) {
		this.messageIndex = messageIndex;
	}

	public String getMessageStatus() {
		return this.messageStatus;
	}

	public void setMessageStatus(String messageStatus) {
		this.messageStatus = messageStatus;
	}

	public String getMessageType() {
		return this.messageType;
	}

	public void setMessageType(String messageType) {
		this.messageType = messageType;
	}

	public double getMpesaAmount() {
		return this.mpesaAmount;
	}

	public void setMpesaAmount(double mpesaAmount) {
		this.mpesaAmount = mpesaAmount;
	}

	public double getMpesaBalance() {
		return this.mpesaBalance;
	}

	public void setMpesaBalance(double mpesaBalance) {
		this.mpesaBalance = mpesaBalance;
	}

	public String getOriginatingAddress() {
		return this.originatingAddress;
	}

	public void setOriginatingAddress(String originatingAddress) {
		this.originatingAddress = originatingAddress;
	}

	public String getOriginatingAddressType() {
		return this.originatingAddressType;
	}

	public void setOriginatingAddressType(String originatingAddressType) {
		this.originatingAddressType = originatingAddressType;
	}

	public String getPhoneNo() {
		return this.phoneNo;
	}

	public void setPhoneNo(String phoneNo) {
		this.phoneNo = phoneNo;
	}

	public byte getProcessed() {
		return this.processed;
	}

	public void setProcessed(byte processed) {
		this.processed = processed;
	}

	public String getSCTimestamp() {
		return this.SCTimestamp;
	}

	public void setSCTimestamp(String SCTimestamp) {
		this.SCTimestamp = SCTimestamp;
	}

	public String getSmscAddress() {
		return this.smscAddress;
	}

	public void setSmscAddress(String smscAddress) {
		this.smscAddress = smscAddress;
	}

	public String getSmscAddressType() {
		return this.smscAddressType;
	}

	public void setSmscAddressType(String smscAddressType) {
		this.smscAddressType = smscAddressType;
	}

	public String getStatus() {
		return this.status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getStorage() {
		return this.storage;
	}

	public void setStorage(String storage) {
		this.storage = storage;
	}

	public String getTimeSent() {
		return this.timeSent;
	}

	public void setTimeSent(String timeSent) {
		this.timeSent = timeSent;
	}

	public String getUserDataText() {
		return this.userDataText;
	}

	public void setUserDataText(String userDataText) {
		this.userDataText = userDataText;
	}

}