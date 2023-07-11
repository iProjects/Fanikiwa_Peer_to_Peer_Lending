package com.sp.fanikiwa;

import java.io.IOException;
import java.text.ParseException;
import java.util.Date;
import java.util.logging.Logger;

import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.business.DiaryComponent;
import com.sp.utils.DateExtension;
import com.sp.utils.StringExtension;

public class DiaryServlet extends HttpServlet {
	private static final Logger log = Logger.getLogger(DiaryServlet.class.getName());
	public void doGet(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		RunDiary( req,  resp);
	}
	public void doPost(HttpServletRequest req, HttpServletResponse resp)
			throws IOException {
		RunDiary( req,  resp);
	}
	
	private void RunDiary(HttpServletRequest req, HttpServletResponse resp) throws IOException
	{
		String datestr = req.getParameter("RunDate");
		Date RunDate = new Date();
		if(!StringExtension.isNullOrEmpty(datestr))
		{
			try {
				RunDate = DateExtension.parse(datestr);
			} catch (ParseException e) {
				// TODO Auto-generated catch block
				log.severe(e.getMessage());
			}
		}
		
		resp.getWriter().println("Starting diary run Date["+RunDate.toString()+"]");
		
		DiaryComponent dc = new DiaryComponent();
		dc.RunDiary(RunDate);
		resp.getWriter().println("Diary completed running");
		
	}
}
