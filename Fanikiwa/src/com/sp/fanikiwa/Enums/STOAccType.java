package com.sp.fanikiwa.Enums;

public enum STOAccType {
	Cash(0), Loan(1);

	private int value;

	private STOAccType(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}
}
