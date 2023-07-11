package com.sp.fanikiwa.entity;

import java.util.ArrayList;
import java.util.Date;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Contract {

	@Id
	Long id; 
	private double amount;
	private Date createdDate;
	private String lender;
	private String borrower;
	private int term;
	private Date startInstallmentDate;
	private Date endInstallmentDate;
	private double interestRate;
	private ArrayList<Installment> repaymentSchedule;
	 
	public Contract() {
	}

	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public double getAmount() {
		return amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}

	public Date getCreatedDate() {
		return createdDate;
	}

	public void setCreatedDate(Date createdDate) {
		this.createdDate = createdDate;
	}

	public String getLender() {
		return lender;
	}

	public void setLender(String lender) {
		this.lender = lender;
	}

	public String getBorrower() {
		return borrower;
	}

	public void setBorrower(String borrower) {
		this.borrower = borrower;
	}

	public int getTerm() {
		return term;
	}

	public void setTerm(int term) {
		this.term = term;
	}

	public Date getStartInstallmentDate() {
		return startInstallmentDate;
	}

	public void setStartInstallmentDate(Date startInstallmentDate) {
		this.startInstallmentDate = startInstallmentDate;
	}

	public Date getEndInstallmentDate() {
		return endInstallmentDate;
	}

	public void setEndInstallmentDate(Date endInstallmentDate) {
		this.endInstallmentDate = endInstallmentDate;
	}

	public double getInterestRate() {
		return interestRate;
	}

	public void setInterestRate(double interestRate) {
		this.interestRate = interestRate;
	}

	public ArrayList<Installment> getRepaymentSchedule() {
		return repaymentSchedule;
	}

	public void setRepaymentSchedule(ArrayList<Installment> repaymentSchedule) {
		this.repaymentSchedule = repaymentSchedule;
	}
 

}