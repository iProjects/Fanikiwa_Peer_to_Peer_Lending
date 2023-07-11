package com.sp.fanikiwa.entity;

import com.googlecode.objectify.annotation.Entity;
import com.sp.fanikiwa.Enums.AccountLimitStatus;
import com.sp.fanikiwa.Enums.PassFlag;

import java.util.ArrayList;
import java.util.List;

@Entity
public class SimulatePostStatus {
	public SimulatePostStatus() {
		Errors = new ArrayList<Exception>();
	}

	// / <summary>
	// / Gets or sets a int value for the TransactionTypeId column.
	// / </summary>

	public Long TransactionTypeId;

	// / <summary>
	// / Gets or sets a int value for the AccountID column.
	// / </summary>

	public Long AccountID;
	// / <summary>
	// / Gets or sets a decimal value for the Amount column.
	// / </summary>

	public double TransactionAmount;
	// / <summary>
	// / Gets or sets a decimal value for the Amount column.
	// / </summary>

	public double BookBalanceBeforePosting;

	// / <summary>
	// / Gets or sets a decimal value for the Amount column.
	// / </summary>

	public double ClearedBalanceBeforePosting;

	// / <summary>
	// / Gets or sets a decimal value for the Amount column.
	// / </summary>

	public double Limit;

	// / <summary>
	// / Gets or sets a limit status.
	// / </summary>

	public AccountLimitStatus LimitStatus;

	// / <summary>
	// / Gets or sets a Blocked status.
	// / </summary>

	public PassFlag BlockedStatus;

	// / <summary>
	// / Gets or sets a limit status.
	// / </summary>

	public List<Exception> Errors;

	// / <summary>
	// / Gets or sets a limit status.
	// / </summary>

	public boolean isCanPost() {

		return Errors.size() == 0;

	}

}
