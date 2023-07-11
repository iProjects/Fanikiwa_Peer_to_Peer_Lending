package com.sp.fanikiwa.entity;

import java.util.Date;

public class LoanPrepaymentDTO {
	private Long loanid;
	private double amount;
	private Date repayDate;
	public Long getLoanid() {
		return loanid;
	}
	public void setLoanid(Long loanid) {
		this.loanid = loanid;
	}
	public double getAmount() {
		return amount;
	}
	public void setAmount(double amount) {
		this.amount = amount;
	}
	public Date getRepayDate() {
		return repayDate;
	}
	public void setRepayDate(Date repayDate) {
		this.repayDate = repayDate;
	}
}
