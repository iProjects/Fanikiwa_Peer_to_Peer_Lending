package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class Loan {

	@Id
	Long id;

	private double amount;

	private Date createdDate;

	private Date maturityDate;

	@Index
	private Long borrowerId;
	@Index
	private Long lenderId;

	private Long offerId;

	private boolean partialPay;

	private int term;

	private double accruedInterest; //for the period
	private double accruedInterestToDate;
	private double interestRate;

	// penalty interest rates
	private double interestRateSusp;
	private double interestRatePenalty;

	private double accruedIntInSusp;

	/*
	 * Interest Accrual Permissible values are - D, M, Y or 1 time. This field
	 * will determine how interest is accrued in the account.
	 */
	@Index
	private String interestAccrualInterval;
	@Index
	private Date lastIntAccrualDate;
	@Index
	private Date nextIntAccrualDate;

	private boolean accrueInSusp;
	/*
	 * Interest Computation Used in interest computation formular. Permissible
	 * values are -S simple; C compound
	 */
	@Index
	private String interestComputationMethod;
	@Index
	private String interestComputationTerm; // Used in int computation formula.
											// Permissible values are - D1,
											// D360, D365, M1, M30, Y

	// Interest Application. This means when is the interest earned/paid for
	// investments or expensed/paid for loans?.
	@Index
	private String interestApplicationMethod;
	// Permissible values are - M monthly. Inst- when installment goes thro.
	// All- when loan is finally paid
	@Index
	private Date lastIntAppDate;
	@Index
	private Date nextIntAppDate;
	private Long intPayingAccount;
	private Long intPaidAccount;
	private Long transactionType;
	private String status;

	// Payment history
	private int noOfPrincipalDefaults;
	private int noOfInterestDefaults;
	private int noOfPricipalPaymentsMade;
	private int noOfInterestPaymentsMade;
	private double amountPrincipalDefaulted;
	private double amountPrincipalPaid;

	public Loan() {
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

	public Date getMaturityDate() {
		return maturityDate;
	}

	public void setMaturityDate(Date maturityDate) {
		this.maturityDate = maturityDate;
	}

	public Long getBorrowerId() {
		return borrowerId;
	}

	public void setBorrowerId(Long borrowerId) {
		this.borrowerId = borrowerId;
	}

	public Long getLenderId() {
		return lenderId;
	}

	public void setLenderId(Long lenderId) {
		this.lenderId = lenderId;
	}

	public Long getOfferId() {
		return offerId;
	}

	public void setOfferId(Long offerId) {
		this.offerId = offerId;
	}

	public boolean isPartialPay() {
		return partialPay;
	}

	public void setPartialPay(boolean partialPay) {
		this.partialPay = partialPay;
	}

	public int getTerm() {
		return term;
	}

	public void setTerm(int term) {
		this.term = term;
	}

	public double getAccruedInterest() {
		return accruedInterest;
	}

	public void setAccruedInterest(double accruedInterest) {
		accruedInterest = accruedInterest;
	}

	public double getInterestRate() {
		return interestRate;
	}

	public void setInterestRate(double interestRate) {
		this.interestRate = interestRate;
	}

	public double getInterestRateSusp() {
		return interestRateSusp;
	}

	public void setInterestRateSusp(double interestRateSusp) {
		this.interestRateSusp = interestRateSusp;
	}

	public double getAccruedIntInSusp() {
		return accruedIntInSusp;
	}

	public void setAccruedIntInSusp(double accruedIntInSusp) {
		this.accruedIntInSusp = accruedIntInSusp;
	}

	public String getInterestAccrualInterval() {
		return interestAccrualInterval;
	}

	public void setInterestAccrualInterval(String interestAccrualInterval) {
		this.interestAccrualInterval = interestAccrualInterval;
	}

	public Date getLastIntAccrualDate() {
		return lastIntAccrualDate;
	}

	public void setLastIntAccrualDate(Date lastIntAccrualDate) {
		this.lastIntAccrualDate = lastIntAccrualDate;
	}

	public Date getNextIntAccrualDate() {
		return nextIntAccrualDate;
	}

	public void setNextIntAccrualDate(Date nextIntAccrualDate) {
		this.nextIntAccrualDate = nextIntAccrualDate;
	}

	public boolean isAccrueInSusp() {
		return accrueInSusp;
	}

	public void setAccrueInSusp(boolean accrueInSusp) {
		this.accrueInSusp = accrueInSusp;
	}

	public String getInterestComputationMethod() {
		return interestComputationMethod;
	}

	public void setInterestComputationMethod(String interestComputationMethod) {
		this.interestComputationMethod = interestComputationMethod;
	}

	public String getInterestComputationTerm() {
		return interestComputationTerm;
	}

	public void setInterestComputationTerm(String interestComputationTerm) {
		this.interestComputationTerm = interestComputationTerm;
	}

	public String getInterestApplicationMethod() {
		return interestApplicationMethod;
	}

	public void setInterestApplicationMethod(String interestApplicationMethod) {
		this.interestApplicationMethod = interestApplicationMethod;
	}

	public Date getLastIntAppDate() {
		return lastIntAppDate;
	}

	public void setLastIntAppDate(Date lastIntAppDate) {
		this.lastIntAppDate = lastIntAppDate;
	}

	public Date getNextIntAppDate() {
		return nextIntAppDate;
	}

	public void setNextIntAppDate(Date nextIntAppDate) {
		this.nextIntAppDate = nextIntAppDate;
	}

	public Long getIntPayingAccount() {
		return intPayingAccount;
	}

	public void setIntPayingAccount(Long intPayingAccount) {
		this.intPayingAccount = intPayingAccount;
	}

	public Long getIntPaidAccount() {
		return intPaidAccount;
	}

	public void setIntPaidAccount(Long intPaidAccount) {
		this.intPaidAccount = intPaidAccount;
	}

	public Long getTransactionType() {
		return transactionType;
	}

	public void setTransactionType(Long transactionType) {
		this.transactionType = transactionType;
	}

	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
	}

	public int getNoOfPrincipalDefaults() {
		return noOfPrincipalDefaults;
	}

	public void setNoOfPrincipalDefaults(int noOfPrincipalDefaults) {
		this.noOfPrincipalDefaults = noOfPrincipalDefaults;
	}

	public int getNoOfInterestDefaults() {
		return noOfInterestDefaults;
	}

	public void setNoOfInterestDefaults(int noOfInterestDefaults) {
		this.noOfInterestDefaults = noOfInterestDefaults;
	}

	public int getNoOfPricipalPaymentsMade() {
		return noOfPricipalPaymentsMade;
	}

	public void setNoOfPricipalPaymentsMade(int noOfPricipalPaymentsMade) {
		this.noOfPricipalPaymentsMade = noOfPricipalPaymentsMade;
	}

	public int getNoOfInterestPaymentsMade() {
		return noOfInterestPaymentsMade;
	}

	public void setNoOfInterestPaymentsMade(int noOfInterestPaymentsMade) {
		this.noOfInterestPaymentsMade = noOfInterestPaymentsMade;
	}

	public double getAccruedInterestToDate() {
		return accruedInterestToDate;
	}

	public void setAccruedInterestToDate(double accruedInterestToDate) {
		this.accruedInterestToDate = accruedInterestToDate;
	}

	public double getInterestRatePenalty() {
		return interestRatePenalty;
	}

	public void setInterestRatePenalty(double interestRatePenalty) {
		this.interestRatePenalty = interestRatePenalty;
	}

	public double getAmountPrincipalDefaulted() {
		return amountPrincipalDefaulted;
	}

	public void setAmountPrincipalDefaulted(double amountPrincipalDefaulted) {
		this.amountPrincipalDefaulted = amountPrincipalDefaulted;
	}

	public double getAmountPrincipalPaid() {
		return amountPrincipalPaid;
	}

	public void setAmountPrincipalPaid(double amountPrincipalPaid) {
		this.amountPrincipalPaid = amountPrincipalPaid;
	}
}