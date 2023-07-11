package com.sp.fanikiwa.Enums;

public enum STOCommFreqFlag {
	/*
	 * NoCommission = 0, PayCommPerSTO = 1, PayCommissionPerOnce = 2
	 */
	NoCommission(0), PayCommPerSTO(1), PayCommissionPerOnce(2);

	private int value;

	private STOCommFreqFlag(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}

}
