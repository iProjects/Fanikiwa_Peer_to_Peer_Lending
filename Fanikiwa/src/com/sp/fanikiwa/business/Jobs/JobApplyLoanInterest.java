package com.sp.fanikiwa.business.Jobs;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.api.LoanEndpoint;
import com.sp.fanikiwa.business.InterestComponent;
import com.sp.fanikiwa.business.LoanComponent;
import com.sp.fanikiwa.business.financialtransactions.GenericTransaction;
import com.sp.fanikiwa.business.financialtransactions.NarrativeFormat;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.LoanUtil;
import com.sp.utils.StringExtension;

/*
 * This job applies interest to all loans that are due. It uses the Loan.nextIntAppDate <= rundate to determine
 * the loans to apply the interest to. It passes the journal below using Config[INTERESTTRANSACTIONTYPE]
 * DR loan.IntPayingAccount with loan.AccruedInterest
 *   CR loan.IntPaidAccount with loan.AccruedInterest
 * 
 * If Config[INTERESTTRANSACTIONTYPE] is defined to charge commission, this job also passes the commission
 * transaction as defined by Config[INTERESTTRANSACTIONTYPE]
 * 
 * If the job is unable to post, it does not adjust LastIntAppDate,AccruedInterest and NextIntAppDate
 * otherwise it zerolizes the getLastIntAppDate and adjusts LastIntAppDate and NextIntAppDate according 
 * to the setting in loan.InterestComputationTerm
 * */
public class JobApplyLoanInterest implements IJobItem {
	boolean EnableLog = true;
	String userID = "SYS";
	String Authorizer = "Auth";
	private static final Logger log = Logger
			.getLogger(JobApplyLoanInterest.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog)
			log.info("Interest application for [" + d + "] started");
		ApplyLoanInterest(d);
		if (EnableLog)
			log.info("Interest application for [" + d + "] ended");
	}

	private void ApplyLoanInterest(Date d) {
		// TODO Auto-generated method stub
		LoanEndpoint ep = new LoanEndpoint();
		Collection<Loan> _Schedule = ep.NextIntAppLoanByDate(d).getItems();

		if (EnableLog)
			log.info("Processing Loan Int application for [" + _Schedule.size()
					+ "] Loan transactions");
		for (Loan loan : _Schedule) {
			// Process now
			try {
				String msg = String.format("Accruing interest for LoanId[%s]",
						loan.getId());
				if (EnableLog)
					log.info(msg);

				LoanComponent lc = new LoanComponent();
				RequestResult re =lc.ApplyAccruedInterest(loan, d);
				if (EnableLog)
					log.info("Processing Accrual [" + loan.getId()
							+ "] completed. \n"+
							"Results: " + re.isSuccess() + "\n" +
							"Info: " + re.getResultMessage());
			} catch (Exception ex) {
				log.log(Level.SEVERE,
						"An error occurred while processing Accrual for Loan["
								+ loan.getId() + "] \n", ex);
			}

		}

	}

	

}
