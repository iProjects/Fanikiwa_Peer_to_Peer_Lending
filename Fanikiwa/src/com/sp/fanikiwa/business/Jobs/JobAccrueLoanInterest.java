package com.sp.fanikiwa.business.Jobs;

import java.util.Collection;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.LoanEndpoint;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.InterestComponent;
import com.sp.fanikiwa.business.LoanComponent;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.STO;
import com.sp.utils.DateExtension;
import com.sp.utils.LoanUtil;
import com.sp.utils.StringExtension;

/*
 * This job accrues interest to all loans that are due. It uses the Loan.nextIntAccrualDate <= rundate to determine
 * the loans to accrue the interest to. 
 * It computes interest using the interest computation method defined in loan.InterestComputationMethod
 * and accrues interest on the either field loan.AccruedInterest or loan.AccruedIntInSusp depending on the
 * setting loan.AccrueInSusp value. It adds interest to the already existing figure in those fields
 * 
 * Finally it adjusts the loan.LastIntAccrualDate and loan.NextIntAccrualDate depending on the setting
 * in field loan.InterestAccrualInterval
 * */
public class JobAccrueLoanInterest implements IJobItem {

	boolean EnableLog = true;

	private static final Logger log = Logger
			.getLogger(JobAccrueLoanInterest.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog)
			log.info("Interest accrual for [" + d + "] started");
		AccrueInterest(d);
		if (EnableLog)
			log.info("Interest accrual for [" + d + "] ended");
	}

	private void AccrueInterest(Date d) {
		// TODO Auto-generated method stub
		LoanEndpoint ep = new LoanEndpoint();
		Collection<Loan> _Schedule = ep.NextIntAccrualLoanByDate(d).getItems();

		if (EnableLog)
			log.info("Processing [" + _Schedule.size() + "] Loan transactions");
		for (Loan loan : _Schedule) {
			// Process now
			try {
				String msg = String.format("Accruing interest for LoanId[%s]",
						loan.getId());
				if (EnableLog)
					log.info(msg);

				LoanComponent lc = new LoanComponent();
				lc.AccrueInterest(loan, d);
				if (EnableLog)
					log.info("Processing Accrual [" + loan.getId()
							+ "] completed");
			} catch (Exception ex) {
				log.log(Level.SEVERE,
						"An error occurred while processing Accrual for Loan["
								+ loan.getId() + "] \n", ex);
			}

		}

	}

}
