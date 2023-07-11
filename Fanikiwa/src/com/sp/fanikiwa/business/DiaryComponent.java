package com.sp.fanikiwa.business;
import java.util.Collection;
import java.util.Date;

import java.util.LinkedHashMap;
import java.util.Map;
import java.util.logging.Logger;

import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.entity.Diaryprogramcontrol;
import com.sp.fanikiwa.api.DiaryprogramcontrolEndpoint;
import com.sp.fanikiwa.business.Jobs.JobAccrueLoanInterest;
import com.sp.fanikiwa.business.Jobs.JobApplyLoanInterest;
import com.sp.fanikiwa.business.Jobs.JobClearValueDatedItems;
import com.sp.fanikiwa.business.Jobs.JobProcessSTO;
import com.sp.fanikiwa.business.Jobs.IJobItem;
import com.sp.utils.DateExtension;

public class DiaryComponent {
	private static final LinkedHashMap<String, IJobItem>items = new LinkedHashMap<String, IJobItem>();

	static{

		DiaryComponent.register("01STO", new JobProcessSTO());
		DiaryComponent.register("02VALUEDATED", new  JobClearValueDatedItems());
		DiaryComponent.register("03INTACR", new  JobAccrueLoanInterest());
		DiaryComponent.register("04INTAPP", new  JobApplyLoanInterest());
	}
	
	boolean EnableLog = true;

	private static final Logger log = Logger.getLogger(DiaryComponent.class.getName());

    public void RunDiary(Date date)
    {
    	log.info("Running diary for["+date+"]");
        try {
        DiaryprogramcontrolEndpoint dDac = new DiaryprogramcontrolEndpoint();
        Collection<Diaryprogramcontrol> controlRec =  dDac.listDiaryprogramcontrol(null, null).getItems();
        Diaryprogramcontrol dp = new Diaryprogramcontrol();
        
        if (controlRec.size() <= 0)
        {
            dp.setLastRun(date);
            dp.setNextRun(DateExtension.addDays(date, 1)) ;
            dDac.insertDiaryprogramcontrol(dp);
        }
        else { 
        	dp = controlRec.iterator().next(); }

        Date lastRun = dp.getLastRun();
        log.info("Last run date["+date+"]");
        for (Date i = dp.getLastRun(); i.before(date) ; i = DateExtension.addDays(i,1))
        {
            if (EnableLog) log.info("Processing started for [" + i + "]");

            for (Map.Entry<String, IJobItem> entry : items.entrySet())
            {
                //run the item
            	entry.getValue().Run(i);
            }
            
            

            //update control rec
            lastRun = i;
        }

        //update db now- control rec
        dp.setLastRun(lastRun);
        dp.setNextRun(DateExtension.addDays(lastRun,1));

			dDac.updateDiaryprogramcontrol(dp);
		} catch (NotFoundException | ConflictException e) {
			// TODO Auto-generated catch block
			//e.printStackTrace();
			log.severe(e.getMessage());
		}
    }

    public static void register(String Name, IJobItem item)
    {
    	//if(item instanceof IDiaryItem)
    	if(item != null) items.put(Name, item);
    }


}