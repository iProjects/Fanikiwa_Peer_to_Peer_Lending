package com.sp.utils;

import java.util.List;

import com.sp.fanikiwa.Enums.PostingCheckFlag;
import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.CustomerEndpoint;
import com.sp.fanikiwa.api.TransactionTypeEndpoint;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.BatchSimulateStatus;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.SimulatePostStatus;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;

public class GLUtil {
	/*These are public methods but not APIs*/
	public static double GetAvailableBalance(Account acc)
	{
		return acc.getClearedBalance() - acc.getLimit();
	}
	public static boolean AccountExists(Long id)
	{ 
		if(id == null) return false;
		
		AccountEndpoint aep = new AccountEndpoint();
		Account acc = aep.getAccount(id);
		return acc != null;
	}
	
	public static boolean CheckLimit( Account acc, double Amount)
	{
		return acc.getLimit() >= Amount;
	}
	public static Account GetAccount(Long accId)
	{
		if(accId == null) return null;
		AccountEndpoint aep = new AccountEndpoint();
		
		return aep.getAccount(accId);
	}
	public static Customer GetCustomer(Long custId)
	{
		if(custId == null) return null;
		CustomerEndpoint cep = new CustomerEndpoint();
		
		return cep.getCustomer(custId);
	}
	public static TransactionType GetTransactionType(Long TtypeId)
	{
		if(TtypeId == null) return null;
		TransactionTypeEndpoint aep = new TransactionTypeEndpoint();
		
		return aep.getTransactionType(TtypeId);
	}
	
	public static RequestResult Simulate(List<Transaction> txns)
	{
		RequestResult re = new RequestResult();
		
		
		BatchSimulateStatus bss = TransactionPost.SimulatePost(txns,
				PostingCheckFlag.CheckLimitAndPassFlag);
		boolean canPost = bss.CanPost();
		if (!canPost) {
			String msg = "";
			for (SimulatePostStatus s : bss.SimulateStatus) {
				for (Exception e : s.Errors) {
					msg += e.getMessage() + "\n";
				}
			}
			
			re.setSuccess(false);
			re.setResultMessage("Simulation Error: \n" + msg);
		}
		else
		{
			re.setSuccess(true);
			re.setResultMessage("Sucessfull");
		}
		
		return re;
		
	}
}
