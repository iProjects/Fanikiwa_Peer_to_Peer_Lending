package com.sp.fanikiwa.entity;

public class DoubleEntryDTO {

	private Long transactionType;
	private Long drAccount;
	private Long crAccount;
	private double amount;
	private String narrative;

	public DoubleEntryDTO() {
	}

	public Long getTransactionType() {
		return transactionType;
	}

	public void setTransactionType(Long transactionType) {
		this.transactionType = transactionType;
	}

	public Long getDrAccount() {
		return drAccount;
	}

	public void setDrAccount(Long drAccount) {
		this.drAccount = drAccount;
	}

	public Long getCrAccount() {
		return crAccount;
	}

	public void setCrAccount(Long crAccount) {
		this.crAccount = crAccount;
	}

	public double getAmount() {
		return amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}

	public String getNarrative() {
		return narrative;
	}

	public void setNarrative(String narrative) {
		this.narrative = narrative;
	}
 
	
}
