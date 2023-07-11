package com.sp.fanikiwa.business.Jobs;

import java.util.Collection;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.STOProcessingContext;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.STO;

/*
 * This job processes all stos
 * */
public class JobProcessSTO implements IJobItem {
	boolean EnableLog = true;
	String userID = "SYS";
	String Authorizer = "Auth";
	private static final Logger log = Logger.getLogger(JobProcessSTO.class
			.getName());

	@Override
	public void Run(Date d) {
		if (EnableLog)
			log.info("Processing STO for [" + d + "] started");
		ProcessSTO(d);
		if (EnableLog)
			log.info("Processing STO for [" + d + "] ended");
	}

	private void ProcessSTO(Date date) {
		// 1. Run STOs
		STOEndpoint st = new STOEndpoint();
		Collection<STO> _ScheduleTransactions = st.SelectSTOByDateFrom(date)
				.getItems();

		if (EnableLog)
			log.info("Processing [" + _ScheduleTransactions.size()
					+ "] transactions");
		for (STO sto : _ScheduleTransactions) { // Process now
			try {
				String msg = String.format("Processing STO[%s]", sto.getId());
				if (EnableLog)
					log.info(msg);

				RequestResult re = ProcessSingleSTO(sto, date);
				if (EnableLog)
					log.info("Processing STO [" + sto.getId()
							+ "] completed. \n" + "Results: " + re.isSuccess()
							+ "\n" + "Info: " + re.getResultMessage());
			} catch (Exception ex) {
				log.log(Level.SEVERE, "An error occurred while processing STO["
						+ sto.getId() + "] \n", ex);
			}

		}

	}

	private RequestResult ProcessSingleSTO(STO sto, Date date)
			throws Exception {
		STOProcessingContext ctx = new STOProcessingContext(sto);
		return ctx.Process();
	}

}
