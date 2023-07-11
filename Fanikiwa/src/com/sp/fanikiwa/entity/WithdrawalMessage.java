package com.sp.fanikiwa.entity;

import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.googlecode.objectify.annotation.Load;

@Entity
public class WithdrawalMessage {
	@Id private Long Id;
	/*Current account being debited*/
	private Long AccountId;
	
	/*Member requesting to withdraw*/
	private Long MemberId;
	
	/*Amount to be withdrawn*/
	private Double Amount;
	/*
	 * How to remit the money
	 * Options are MPESA|EFT|BANKMOBI
	 * */
	private String RemissionMethod;
	/*
	 * NEW|PROCESSING|PAID|DENIED|ERROR|CLOSED
	 * */
	private String Status;
	
	/*Further information on status*/
	private String Remarks;

	public Long getId() {
		return Id;
	}

	public void setId(Long id) {
		Id = id;
	}

	public Long getAccountId() {
		return AccountId;
	}

	public void setAccountId(Long accountId) {
		AccountId = accountId;
	}

	public Long getMemberId() {
		return MemberId;
	}

	public void setMemberId(Long memberId) {
		MemberId = memberId;
	}

	public Double getAmount() {
		return Amount;
	}

	public void setAmount(Double amount) {
		Amount = amount;
	}

	public String getRemissionMethod() {
		return RemissionMethod;
	}

	public void setRemissionMethod(String remissionMethod) {
		RemissionMethod = remissionMethod;
	}

	public String getStatus() {
		return Status;
	}

	public void setStatus(String status) {
		Status = status;
	}

	public String getRemarks() {
		return Remarks;
	}

	public void setRemarks(String remarks) {
		Remarks = remarks;
	}

}
