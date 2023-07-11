package com.sp.fanikiwa.Enums;

public enum UserType {
	Member(0), Admin(1),System(2), BackgroundUser(3), FrontEndUser(4);

	private int value;

	private UserType(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}

}

