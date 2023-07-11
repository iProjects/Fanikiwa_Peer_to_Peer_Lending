package com.sp.fanikiwa.Enums;

public enum PassFlag {
	Unknown(-1), Ok(0),
	// PassFlag - Lock status
	DebitPostingProhibited(1), CreditPostingProhibited(2), AllPostingProhibited(
			3), Locked(4); // even inquiry is prohibited

	private int value;

	private PassFlag(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}
}
