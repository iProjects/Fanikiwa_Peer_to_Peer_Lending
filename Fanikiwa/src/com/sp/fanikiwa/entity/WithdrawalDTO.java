package com.sp.fanikiwa.entity;

public class WithdrawalDTO {
	/*Current account being debited*/
	private String email;
	
	/*Amount to be withdrawn*/
	private Double amount;
	/*
	 * How to remit the money
	 * Options are MPESA|EFT|BANKMOBI
	 * */
	private String remissionMethod;
	public String getEmail() {
		return email;
	}
	public void setEmail(String email) {
		this.email = email;
	}
	public Double getAmount() {
		return this.amount;
	}
	public void setAmount(Double amount) {
		this.amount = amount;
	}
	public String getRemissionMethod() {
		return this.remissionMethod;
	}
	public void setRemissionMethod(String remissionMethod) {
		this.remissionMethod = remissionMethod;
	}

}
