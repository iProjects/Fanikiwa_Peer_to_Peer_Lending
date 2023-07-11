package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class AccountDTO {

	@Id
	Long accountID;

	private String accountName;

	private String accountNo;

	private double accruedInt;
	private double accruedIntInSusp;

	private Date maturityDate;

	private Date createDate;

	private double bookBalance;

	private String branch;

	private double clearedBalance;

	private boolean closed;

	private double interestRate;

	// penalty interest rates
	private double interestRateSusp;

	private double limit;

	private int limitCheckFlag;

	private String limitFlag;

	private String passFlag;
	// Interest Accrual
	@Index
	private String interestAccrualInterval; // Permissible values are - D, M, Y
											// or 1 time. This field will
											// determine how interest is accrued
											// in the account.
	@Index
	private Date lastIntAccrualDate;
	@Index
	private Date nextIntAccrualDate;
	private boolean accrueInSusp;
	// Interest Computation
	@Index
	private String interestComputationMethod; // Used in interest computation
												// formular. Permissible values
												// are -S simple; C compound
	@Index
	private String interestComputationTerm; // Used in int computation formula.
											// Permissible values are - D1,
											// D360, D365, M1, M30, Y

	// Interest Application
	// This means when is the interest earned/paid for investments or
	// expensed/paid for loans?.
	@Index
	private String interestApplicationMethod;// Permissible values are - M
												// monthly. Inst- when
												// installment goes thro. All-
												// when loan is finally paid
	@Index
	private Date lastIntAppDate;
	@Index
	private Date nextIntAppDate;
	private Long intPayAccount;
	// Foreign Keys
	private Long customer;

	private Long coadet;

	private Long accounttype;

	public AccountDTO() {
	}

	public Long getCustomer() {
		return customer;
	}

	public void setCustomer(Long customer) {
		this.customer = customer;
	}

	public Long getAccounttype() {
		return accounttype;
	}

	public void setAccounttype(Long accounttype) {
		this.accounttype = accounttype;
	}

	public Long getCoadet() {
		return coadet;
	}

	public void setCoadet(Long coadet) {
		this.coadet = coadet;
	}

	public Long getAccountID() {
		return accountID;
	}

	public void setAccountID(Long accountID) {
		this.accountID = accountID;
	}

	public String getAccountName() {
		return accountName;
	}

	public void setAccountName(String accountName) {
		this.accountName = accountName;
	}

	public String getAccountNo() {
		return accountNo;
	}

	public void setAccountNo(String accountNo) {
		this.accountNo = accountNo;
	}

	public double getAccruedInt() {
		return accruedInt;
	}

	public void setAccruedInt(double accruedInt) {
		this.accruedInt = accruedInt;
	}

	public double getAccruedIntInSusp() {
		return accruedIntInSusp;
	}

	public void setAccruedIntInSusp(double accruedIntInSusp) {
		this.accruedIntInSusp = accruedIntInSusp;
	}

	public Date getMaturityDate() {
		return maturityDate;
	}

	public void setMaturityDate(Date maturityDate) {
		this.maturityDate = maturityDate;
	}

	public boolean getAccrueInSusp() {
		return accrueInSusp;
	}

	public void setAccrueInSusp(boolean accrueInSusp) {
		this.accrueInSusp = accrueInSusp;
	}

	public double getInterestRateSusp() {
		return interestRateSusp;
	}

	public void setInterestRateSusp(double interestRateSusp) {
		this.interestRateSusp = interestRateSusp;
	}

	public double getBookBalance() {
		return bookBalance;
	}

	public void setBookBalance(double bookBalance) {
		this.bookBalance = bookBalance;
	}

	public String getBranch() {
		return branch;
	}

	public void setBranch(String branch) {
		this.branch = branch;
	}

	public double getClearedBalance() {
		return clearedBalance;
	}

	public void setClearedBalance(double clearedBalance) {
		this.clearedBalance = clearedBalance;
	}

	public boolean getClosed() {
		return closed;
	}

	public void setClosed(boolean closed) {
		this.closed = closed;
	}

	public double getInterestRate() {
		return interestRate;
	}

	public void setInterestRate(double interestRate) {
		this.interestRate = interestRate;
	}

	public double getLimit() {
		return limit;
	}

	public void setLimit(double limit) {
		this.limit = limit;
	}

	public int getLimitCheckFlag() {
		return limitCheckFlag;
	}

	public void setLimitCheckFlag(int limitCheckFlag) {
		this.limitCheckFlag = limitCheckFlag;
	}

	public String getLimitFlag() {
		return limitFlag;
	}

	public void setLimitFlag(String limitFlag) {
		this.limitFlag = limitFlag;
	}

	public String getPassFlag() {
		return passFlag;
	}

	public void setPassFlag(String passFlag) {
		this.passFlag = passFlag;
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

	public Long getIntPayAccount() {
		return intPayAccount;
	}

	public void setIntPayAccount(Long intPayAccount) {
		this.intPayAccount = intPayAccount;
	}

	public Date getCreateDate() {
		return createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}

}