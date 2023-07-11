package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Load;
import com.sp.utils.DateExtension;

public class OfferDTO {
	private String description = "";
	private double amount = 0;
	private int term = 1;
	private double interest = 1;
	private boolean privateOffer = false;
	private Date createdDate = new Date();
	private Date expiryDate = DateExtension.addMonths(new Date(), 1);
	private String offerees = "";
	private String offerType = "L";
	private boolean partialPay = false;
	private String status = "Open";
	private String email;

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public double getAmount() {
		return amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}

	public int getTerm() {
		return term;
	}

	public void setTerm(int term) {
		this.term = term;
	}

	public double getInterest() {
		return interest;
	}

	public void setInterest(double interest) {
		this.interest = interest;
	}

	public boolean isPrivateOffer() {
		return privateOffer;
	}

	public void setPrivateOffer(boolean privateOffer) {
		this.privateOffer = privateOffer;
	}

	public Date getCreatedDate() {
		return createdDate;
	}

	public void setCreatedDate(Date createdDate) {
		this.createdDate = createdDate;
	}

	public Date getExpiryDate() {
		return expiryDate;
	}

	public void setExpiryDate(Date expiryDate) {
		this.expiryDate = expiryDate;
	}

	public String getOfferees() {
		return offerees;
	}

	public void setOfferees(String offerees) {
		this.offerees = offerees;
	}

	public String getOfferType() {
		return offerType;
	}

	public void setOfferType(String offerType) {
		this.offerType = offerType;
	}

	public boolean isPartialPay() {
		return partialPay;
	}

	public void setPartialPay(boolean partialPay) {
		this.partialPay = partialPay;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

}
