package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class STO {

	@Id
	Long id;

 
	private double amountDefaulted;

 
	private double amountPaid;

 
	private boolean chargeCommFlag;

 
	private short chargeWho;

 
	private short commFreqFlag;

 
	private Long commissionAccount;

 
	private double commissionAmount;

 
	private boolean commissionPaidFlag;

 /*
  * [NoCommission |STO|TransactionType]
  * STO - Pick the value in the STO.commissionAmount 
  * TransactionType - Compute commission as defined by the TransactionType
  * */
	private String commSourceFlag;

 
	private Long crAccount;

 
	private Date createDate;

 
	private Long crTxnType;

 
	private Long drAccount;

 
	private Long drTxnType;

 
	private Date endDate;

 
	private String interval;

 
	private int limitFlag;

 
	private int feesFlag;

 
	@Index private Long loanId;

 
	private Date nextPayDate;

 
	private int noOfDefaults;

 
	private int noOfPayments;

 
	private int noOfPaymentsMade;

 
	private boolean partialPay;

 
	private double payAmount;

	private double interestAmount;
 
	private Date startDate;

 
	private int STOAccType;

 
	private String STOType;

 
	private double totalToPay;

	public STO() {
	}

	public Long getId() {
		return this.id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public double getAmountDefaulted() {
		return this.amountDefaulted;
	}

	public void setAmountDefaulted(double amountDefaulted) {
		this.amountDefaulted = amountDefaulted;
	}

	public double getInterestAmount() {
		return interestAmount;
	}

	public void setInterestAmount(double interestAmount) {
		this.interestAmount = interestAmount;
	}


	public double getAmountPaid() {
		return this.amountPaid;
	}

	public void setAmountPaid(double amountPaid) {
		this.amountPaid = amountPaid;
	}

	public boolean getChargeCommFlag() {
		return this.chargeCommFlag;
	}

	public void setChargeCommFlag(boolean chargeCommFlag) {
		this.chargeCommFlag = chargeCommFlag;
	}

	public short getChargeWho() {
		return this.chargeWho;
	}

	public void setChargeWho(short chargeWho) {
		this.chargeWho = chargeWho;
	}

	public short getCommFreqFlag() {
		return this.commFreqFlag;
	}

	public void setCommFreqFlag(short commFreqFlag) {
		this.commFreqFlag = commFreqFlag;
	}

	public Long getCommissionAccount() {
		return this.commissionAccount;
	}

	public void setCommissionAccount(Long commissionAccount) {
		this.commissionAccount = commissionAccount;
	}

	public double getCommissionAmount() {
		return this.commissionAmount;
	}

	public void setCommissionAmount(double commissionAmount) {
		this.commissionAmount = commissionAmount;
	}

	public boolean getCommissionPaidFlag() {
		return this.commissionPaidFlag;
	}

	public void setCommissionPaidFlag(boolean commissionPaidFlag) {
		this.commissionPaidFlag = commissionPaidFlag;
	}

	public String getCommSourceFlag() {
		return this.commSourceFlag;
	}

	public void setCommSourceFlag(String commSourceFlag) {
		this.commSourceFlag = commSourceFlag;
	}

	public Long getCrAccount() {
		return this.crAccount;
	}

	public void setCrAccount(Long crAccount) {
		this.crAccount = crAccount;
	}

	public Date getCreateDate() {
		return this.createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}

	public Long getCrTxnType() {
		return this.crTxnType;
	}

	public void setCrTxnType(Long crTxnType) {
		this.crTxnType = crTxnType;
	}

	public Long getDrAccount() {
		return this.drAccount;
	}

	public void setDrAccount(Long drAccount) {
		this.drAccount = drAccount;
	}

	public Long getDrTxnType() {
		return this.drTxnType;
	}

	public void setDrTxnType(Long drTxnType) {
		this.drTxnType = drTxnType;
	}

	public Date getEndDate() {
		return this.endDate;
	}

	public void setEndDate(Date endDate) {
		this.endDate = endDate;
	}

	public String getInterval() {
		return this.interval;
	}

	public void setInterval(String interval) {
		this.interval = interval;
	}

	public int getLimitFlag() {
		return this.limitFlag;
	}

	public void setLimitFlag(int limitFlag) {
		this.limitFlag = limitFlag;
	}

	public int getFeesFlag() {
		return feesFlag;
	}

	public void setFeesFlag(int feesFlag) {
		this.feesFlag = feesFlag;
	}

	public Long getLoanId() {
		return this.loanId;
	}

	public void setLoanId(Long loanId) {
		this.loanId = loanId;
	}

	public Date getNextPayDate() {
		return this.nextPayDate;
	}

	public void setNextPayDate(Date nextPayDate) {
		this.nextPayDate = nextPayDate;
	}

	public int getNoOfDefaults() {
		return this.noOfDefaults;
	}

	public void setNoOfDefaults(int noOfDefaults) {
		this.noOfDefaults = noOfDefaults;
	}

	public int getNoOfPayments() {
		return this.noOfPayments;
	}

	public void setNoOfPayments(int noOfPayments) {
		this.noOfPayments = noOfPayments;
	}

	public int getNoOfPaymentsMade() {
		return this.noOfPaymentsMade;
	}

	public void setNoOfPaymentsMade(int noOfPaymentsMade) {
		this.noOfPaymentsMade = noOfPaymentsMade;
	}

	public boolean getPartialPay() {
		return this.partialPay;
	}

	public void setPartialPay(boolean partialPay) {
		this.partialPay = partialPay;
	}

	public double getPayAmount() {
		return this.payAmount;
	}

	public void setPayAmount(double payAmount) {
		this.payAmount = payAmount;
	}

	public Date getStartDate() {
		return this.startDate;
	}

	public void setStartDate(Date startDate) {
		this.startDate = startDate;
	}

	public int getSTOAccType() {
		return this.STOAccType;
	}

	public void setSTOAccType(int STOAccType) {
		this.STOAccType = STOAccType;
	}

	public String getSTOType() {
		return this.STOType;
	}

	public void setSTOType(String STOType) {
		this.STOType = STOType;
	}

	public double getTotalToPay() {
		return this.totalToPay;
	}

	public void setTotalToPay(double totalToPay) {
		this.totalToPay = totalToPay;
	}

}