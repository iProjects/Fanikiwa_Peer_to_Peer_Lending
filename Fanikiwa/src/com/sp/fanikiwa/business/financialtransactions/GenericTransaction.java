package com.sp.fanikiwa.business.financialtransactions;

import java.util.Date;

import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.TransactionType;

public class GenericTransaction extends abstractMoneyTransaction {

	public GenericTransaction(TransactionType TType, String shortCode,
			Date postDate, Account drAccount, Account crAccount, double amount,
			boolean forcePost, boolean statFlag, String authorizer, String user,
			String reference) {

		super(TType, shortCode, postDate, drAccount, crAccount, amount,
				forcePost, statFlag, authorizer, user, reference);

	}
}
