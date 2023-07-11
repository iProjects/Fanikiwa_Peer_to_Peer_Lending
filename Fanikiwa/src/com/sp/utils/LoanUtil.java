package com.sp.utils;

import java.util.Date;

import com.sp.fanikiwa.entity.Loan;

public class LoanUtil {
	public static Date GetNextIntAccrualDate(Loan loan, Date date) {
		// TODO Auto-generated method stub

		String term = loan.getInterestAccrualInterval();
		int timeInDays =  AccrualTermToInt( term,  date);
		return DateExtension.addDays(date, timeInDays);
	}

	public static int AccrualTermToInt(String term, Date date) {
		if (StringExtension.isNullOrEmpty(term))
			return 1;
		switch (term.toUpperCase()) // D, D1, D360, D365, M, M30, Y
		{
		case "D":
			return 1; //accrue daily
		case "D1":
			return 1;
		case "D360": //equivalent to accrue yearly
			return 360;
		case "D365":
			return 365;
		case "M": 
			return DateExtension.DaysOfTheMonth(date);
		case "M30":
			return 30;
		case "Y":
			return DateExtension.DaysOfTheYear(date);

		default:
			return 1;
		}
	}

	public static double GetEffectiveIntRate(String Symbol, Loan loan) {
		if (Symbol.equals("Suspended")) {
			return loan.getInterestRateSusp(); //normal + penalty
		} else {
			return loan.getInterestRate();
		}
	}

	public static int GetAccountTerm(Loan loan, Date date) // return term in days
	{
		return DateExtension.DiffDays(loan.getLastIntAccrualDate(), date);
	}

	public static Date GetNextIntApplicationDate(Loan loan, Date date) {
		String term = loan.getInterestComputationTerm();
		switch (term.toUpperCase()) // D, D1, D360, D365, M, M30, Y
		{
		case "D":
			return DateExtension.addDays(date, 1);
		case "D1":
			return DateExtension.addDays(date, 1);
		case "D360": //equivalent to accrue yearly
			return DateExtension.addYears(date, 1);
		case "D365":
			return DateExtension.addYears(date, 1);
		case "M": 
			return DateExtension.addMonths(date, 1);
		case "M30":
			return DateExtension.addMonths(date, 1);
		case "Y":
			return DateExtension.addYears(date, 1);

		default:
			return DateExtension.addDays(date, 1);
		}

	}

}
