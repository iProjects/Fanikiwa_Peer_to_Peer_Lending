package com.sp.fanikiwa.business.Jobs;

import java.util.Collection;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.InterestComponent;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.STO;
import com.sp.utils.DateExtension;
import com.sp.utils.StringExtension;

public class JobAccrueAccountInterest implements IJobItem {

	boolean EnableLog = true;

	private static final Logger log = Logger.getLogger(JobAccrueAccountInterest.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog) log.info("Interest accrual for [" + d + "] started");
		AccrueInterest(d);
		if (EnableLog) log.info("Interest accrual for [" + d + "] ended");
	}

	private void AccrueInterest(Date d) {
		// TODO Auto-generated method stub
		AccountEndpoint ep = new AccountEndpoint();
        Collection<Account> _Schedule = ep.NextIntAccrualAccountsByDate(d).getItems();

        if (EnableLog) log.info("Processing [" + _Schedule.size() + "] transactions");
        for (Account acc : _Schedule)
        {
            //Process now
            try
            {
                String msg = String.format("Accruing interest for Id[%s]", 
                        acc.getAccountID());
                if (EnableLog) log.info(msg);

                ProcessAccrual(acc, d);
                if (EnableLog) log.info("Processing Accrual [" + acc.getAccountID()+ "] completed");
            }
            catch (Exception ex)
            {
                log.log(Level.SEVERE, "An error occurred while processing Accrual[" + acc.getAccountID() + "] completed\n", ex);
            }

        }

    }

	private void ProcessAccrual(Account acc, Date date) {
		//Compute interest
		InterestComponent ic = new InterestComponent();
		double intr=0; 
		String period = acc.getInterestComputationTerm();
		if(StringExtension.isNullOrEmpty(period) )period="Y";
		
		String method = acc.getInterestComputationMethod();
		if(!StringExtension.isNullOrEmpty(method) &&
				method.toUpperCase().equals("C"))
		{
		 intr = ic.ComputeCompoundInterest(acc.getBookBalance(),
				GetAccountTerm(acc,date), (double) GetEffectiveIntRate(acc));
		}else
		{
			 intr = ic.ComputeSimpleInterest(period,acc.getBookBalance(),
						GetAccountTerm(acc,date), (double) GetEffectiveIntRate(acc));
		}
		
		//Adjust interest accrual field
		if(acc.getAccrueInSusp())
		{
			acc.setAccruedIntInSusp(acc.getAccruedIntInSusp() + intr);
		}
		else
		{
			acc.setAccruedInt(acc.getAccruedInt() + intr);
		}
	}
	
	private double GetEffectiveIntRate(Account acc)
	{
		if(acc.getAccrueInSusp())
		{
			return acc.getInterestRateSusp();
		}
		else
		{
			return acc.getInterestRate();
		}
	}
	
	private int GetAccountTerm(Account acc, Date date) //return term in days
	{
		return DateExtension.DiffDays(acc.getLastIntAccrualDate(), date);
	}


}
