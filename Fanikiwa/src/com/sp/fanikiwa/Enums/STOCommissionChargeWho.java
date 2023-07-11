package com.sp.fanikiwa.Enums;

public enum STOCommissionChargeWho {
	/*
	 * 0. Borrower to pay commission 1. Investor to pay commission 2. Both to
	 * pay commission
	 */

	Borrower(0), Investor(1), BothBorrowerAndInvestor(2);
	private int value;

	private STOCommissionChargeWho(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}
}
