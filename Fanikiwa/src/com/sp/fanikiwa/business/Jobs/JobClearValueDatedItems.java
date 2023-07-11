package com.sp.fanikiwa.business.Jobs;

import java.util.Date;
import java.util.logging.Logger;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.api.AccountEndpoint;

public class JobClearValueDatedItems implements IJobItem {
	boolean EnableLog = true;

	private static final Logger log = Logger.getLogger(JobClearValueDatedItems.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog) log.info("Processing Value dated items for [" + d + "] started");
		RunClearItems(d);
		if (EnableLog) log.info("Processing Value dated items for [" + d + "] ended");
		
	}
	
	  private void RunClearItems(Date date)
	    {
		  try
		  {
	    	AccountEndpoint st = new AccountEndpoint();
	        st.ClearDaysEffects(date);
		  }catch(Exception e)
		  {
			  e.printStackTrace();
		  }
	    }

}
