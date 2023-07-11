package com.sp.utils;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Locale;

public class DateExtension {
	
	private final static String DATE_FORMAT = "MM/dd/yyyy HH:mm:ss";
	
	//maths
	public static Date addMinutes(Date date, int minutes)
    {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.MINUTE, minutes);
        return cal.getTime();
    }
	
	public static Date addDays(Date date, int days)
    {
        Calendar cal = Calendar.getInstance();
        cal.setTime(date);
        cal.add(Calendar.DATE, days); //minus number would decrement the days
        return cal.getTime();
    }
	public static Date addMonths(Date date, int months)
	{
		Calendar cal = Calendar.getInstance(); 
		cal.setTime(date);
		cal.add(Calendar.MONTH, months);
		return cal.getTime();
	}
	public static Date addYears(Date date, int years)
	{
		Calendar cal = Calendar.getInstance(); 
		cal.setTime(date);
		cal.add(Calendar.YEAR, years);
		return cal.getTime();
	}
	public static Date addInterval(Date d, String interval)
	    {
	        Date ret = d;
	        switch (interval.toUpperCase())
	        {
	            case "D":
	                ret = DateExtension.addDays(ret, 1);
	                break;

	            case "M":
	                ret = DateExtension.addMonths(ret,1);
	                break;
	            case "Y":
	                ret = DateExtension.addYears(ret,1);
	                break;
	        }
	        return ret;
	    }

	//Parsing
	public static Date parse(String s) throws ParseException
	{
		return parse( s, DATE_FORMAT);
	}
	public static Date parse(String s, String Format) throws ParseException
	{
		// Create an instance of SimpleDateFormat used for formatting 
		// the string representation of date (month/day/year)
		DateFormat df = new SimpleDateFormat(Format);
		return df.parse(s);
	}
	public static Date parse(String s, Locale locale) throws ParseException
	{
		 int style = DateFormat.MEDIUM;
		 DateFormat   df = DateFormat.getDateInstance(style,locale);
		return df.parse(s);
	}

	//
	public static int MonthOfDate(Date date)
	{
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);
		return cal.get(Calendar.MONTH);
	}
	public static int DaysOfTheMonth(Date date)
	{
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);
		return cal.getActualMaximum(Calendar.DAY_OF_MONTH) ;
	}
	public static int DaysOfTheYear(Date date)
	{
		Calendar cal = Calendar.getInstance();
		cal.setTime(date);
		return cal.getActualMaximum(Calendar.DAY_OF_YEAR) ;
	}
	public static int DiffDays(Date d1, Date d2) {
		int daysdiff=0;
		long diff = d2.getTime() - d1.getTime();
		long diffDays = diff / (24 * 60 * 60 * 1000)+1;
		 daysdiff = (int) diffDays;
		return daysdiff;
		 }
	public static boolean DateExpired(Date start, Date end)
	{
		return end.after(start); //i.e start > end
	}
}
