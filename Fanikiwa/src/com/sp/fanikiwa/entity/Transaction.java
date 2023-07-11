package com.sp.fanikiwa.entity;

import java.util.Date;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class Transaction {

	@Id
	Long transactionID;

	private double amount;

	private String authorizer;

	private String contraReference;

	private boolean forcePostFlag;

	private String narrative;

	@Index
	private Date postDate;

	private Date recordDate;

	private String reference;

	private boolean statementFlag;

	private String userID;

	private Date valueDate;

	// Foreign keys
	@Index
	@Load
	Ref<Account> account;

	@Load
	Ref<TransactionType> transactionType;

	public Transaction() {
	}

	public Long getTransactionID() {
		return this.transactionID;
	}

	public void setTransactionID(Long transactionID) {
		this.transactionID = transactionID;
	}

	public double getAmount() {
		return this.amount;
	}

	public void setAmount(double amount) {
		this.amount = amount;
	}

	public String getAuthorizer() {
		return this.authorizer;
	}

	public void setAuthorizer(String authorizer) {
		this.authorizer = authorizer;
	}

	public String getContraReference() {
		return this.contraReference;
	}

	public void setContraReference(String contraReference) {
		this.contraReference = contraReference;
	}

	public boolean getForcePostFlag() {
		return this.forcePostFlag;
	}

	public void setForcePostFlag(boolean forcePostFlag) {
		this.forcePostFlag = forcePostFlag;
	}

	public String getNarrative() {
		return this.narrative;
	}

	public void setNarrative(String narrative) {
		this.narrative = narrative;
	}

	public Date getPostDate() {
		return this.postDate;
	}

	public void setPostDate(Date postDate) {
		this.postDate = postDate;
	}

	public Date getRecordDate() {
		return this.recordDate;
	}

	public void setRecordDate(Date recordDate) {
		this.recordDate = recordDate;
	}

	public String getReference() {
		return this.reference;
	}

	public void setReference(String reference) {
		this.reference = reference;
	}

	public boolean getStatementFlag() {
		return this.statementFlag;
	}

	public void setStatementFlag(boolean statementFlag) {
		this.statementFlag = statementFlag;
	}

	public String getUserID() {
		return this.userID;
	}

	public void setUserID(String userID) {
		this.userID = userID;
	}

	public Date getValueDate() {
		return this.valueDate;
	}

	public void setValueDate(Date valueDate) {
		this.valueDate = valueDate;
	}

	public Account getAccount() {
		return this.account.get();
	}

	public void setAccount(Account account) {
		this.account = Ref.create(account);
	}

	public TransactionType getTransactionType() {
		return this.transactionType.get();
	}

	public void setTransactionType(TransactionType transactionType) {
		this.transactionType = Ref.create(transactionType);
	}

}