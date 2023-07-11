package com.sp.fanikiwa.entity;

import java.util.List;

public class MultiEntry {
	private List<Transaction> transactions;

	public MultiEntry() {
	}

	public MultiEntry(List<Transaction> transactions) {
		this.transactions = transactions;
	}

	public void setTransactions(List<Transaction> transactions) {
		this.transactions = transactions;
	}

	public List<Transaction> getTransactions() {
		return this.transactions;
	}

}
