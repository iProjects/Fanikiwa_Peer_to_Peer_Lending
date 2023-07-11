package com.sp.fanikiwa.business.Jobs;

import java.util.Date;
import java.util.logging.Logger;

public class JobApplyInterest implements IJobItem  {
	boolean EnableLog = true;

	private static final Logger log = Logger.getLogger(JobApplyInterest.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog) log.info("Interest application for [" + d + "] started");
		ApplyInterest(d);
		if (EnableLog) log.info("Interest application for [" + d + "] ended");
	}

	private void ApplyInterest(Date d) {
		// TODO Auto-generated method stub
		
	}

}
