package com.sp.fanikiwa.business;

import java.util.List;

import com.sp.fanikiwa.Enums.PostingCheckFlag;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.BatchSimulateStatus;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.SimulatePostStatus;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.WithdrawalMessage;
import com.sp.utils.GLUtil;
import com.sp.utils.MpesaPayUtil;
import com.sp.utils.PeerLendingUtil;

public class WithdrawalComponent {
	final String USERID = "SYS";
	final String AUTHORIZER = "SYS";

	/*
	 * 1.
	 */
	public RequestResult Withdraw(WithdrawalMessage wm) throws Exception {
		PeerLendingUtil.SetWithdrawalStatus(wm, "Processing");
		String AccountSymbol ="CASHACCOUNT"; 
		String TransactionTypeSymbol="CASHWITHDRAWALTRANSACTIONTYPE";
		String Narrative = "";
		String Reference = "";
		switch (wm.getRemissionMethod()) // MPESA|EFT|BANKMOBI
		{
		case "MPESA":
			Narrative = "WTD Account["+wm.getAccountId()+"]";
			Reference = "Mpesa";
			AccountSymbol ="MPESACASHACCOUNT";
			TransactionTypeSymbol ="MPESAWITHDRAWALTRANSACTIONTYPE";
			break;
		case "EFT":
			// 1. Get banking details
			// 2. instruct our bank to pay via EFT
			throw new Exception("NotImplemented");
		case "BANKMOBI":
			// 1. Get member phone
			// 2. instruct our bank to pay via MobileMoney
			throw new Exception("NotImplemented");

			default:
				AccountSymbol ="CASHACCOUNT";
				TransactionTypeSymbol ="CASHWITHDRAWALTRANSACTIONTYPE";
		}
		// Step 1 Debit the account
		RequestResult re = AccountWithdraw(wm.getAccountId(), wm.getAmount(),
				Narrative,
				Reference,
				AccountSymbol,TransactionTypeSymbol);

		if (re.isSuccess()) {
			PeerLendingUtil.SetWithdrawalStatus(wm, "Transacted");
			// Step 2: Remit the money
			RequestResult re2 = RemitMoney(wm);
			if (re2.isSuccess()) {
				PeerLendingUtil.SetWithdrawalStatus(wm, "Remitted",re2.getResultMessage());
				return re2;
			} else {
				PeerLendingUtil.SetWithdrawalStatus(wm, "RemissionError",
						re2.getResultMessage());
				return re2;
			}
		} else {
			PeerLendingUtil.SetWithdrawalStatus(wm, "TransactionError",
					re.getResultMessage());
		}

		return re;
	}

	private RequestResult RemitMoney(WithdrawalMessage wm) throws Exception {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		Member member = PeerLendingUtil.GetMember(wm.getMemberId());
		if (member == null) {
			re.setSuccess(false);
			re.setResultMessage("Member not found +[" + wm.getMemberId() + "]");
			return re;
		}

		switch (wm.getRemissionMethod()) // MPESA|EFT|BANKMOBI
		{
		case "MPESA":
			// 1. Get phone details
			// 2. instruct Safaricom to send money via MPESA

			if (MpesaPayUtil.PostToMpesaMock(wm.getAmount(),
					member.getTelephone())) {
				re.setSuccess(true);
				re.setResultMessage("PostToMpesaTest successful: Telephone["+member.getTelephone()+"] Amount["+wm.getAmount()+"]");
				return re;
			} else {
				re.setSuccess(false);
				re.setResultMessage("PostToMpesaTest not successful");
				return re;
			}
		case "EFT":
			// 1. Get banking details
			// 2. instruct our bank to pay via EFT
			throw new Exception("NotImplemented");
		case "BANKMOBI":
			// 1. Get member phone
			// 2. instruct our bank to pay via MobileMoney
			throw new Exception("NotImplemented");

		}

		return re;

	}

	private RequestResult AccountWithdraw(Long AccountId, double Amount,
			String Narr, String reference,
			 String AccountSymbol, 
			 String TransactionTypeSymbol) throws Exception {

		List<Transaction> txns = TransactionFactory.Withdraw(AccountId, Amount,
				Narr, reference,AccountSymbol,TransactionTypeSymbol);

		RequestResult re = GLUtil.Simulate(txns);
		if (re.isSuccess()) {
			return TransactionPost.Post(txns);
		}else
		{
		return re;
		}
	}
}
