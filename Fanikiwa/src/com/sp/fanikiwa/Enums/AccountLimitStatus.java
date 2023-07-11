package com.sp.fanikiwa.Enums;

public enum AccountLimitStatus {

	Unknown(-1), Ok(0),

	// LimitCheckFlag - Limit financial transaction Post status
	// thrown as exceptions by the posting component
	PostingNoLimitChecking(5), // 0 - Do not check limit flag
	PostingOverDrawingProhibited(6), // 1 - Check limit on cleared balance. i.e.
										// Cleared-Limit should not be < 0
	PostingDrawingOnUnclearedEffectsAllowed(7), // 2 - Check limit on book
												// balance. i.e. book-Limit
												// should not be < 0

	// Limit Flag - Marking Limit status
	LimitsAllowed(8), // 0 - Do not check limit flag
	LimitForAdvanceProhibited(9), // 3 - the account cannot have a negative
									// limit. ie. we cannot advance. i.e limit
									// should not be <0
	LimitForBlockingProhibited(10), // 4 - the account cannot have a +ve limit.
									// ie. we cannot block. i.e limit should not
									// be >0
	AllLimitsProhibited(11); // 5 - All limits prohibited i.e limit ( 0

	private int value;

	private AccountLimitStatus(int value) {
		this.value = value;
	}

	public int getValue() {
		return value;
	}
}
