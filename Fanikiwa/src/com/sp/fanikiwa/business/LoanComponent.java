package com.sp.fanikiwa.business;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.api.LoanEndpoint;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.financialtransactions.GenericTransaction;
import com.sp.fanikiwa.business.financialtransactions.NarrativeFormat;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.Config;
import com.sp.utils.GLUtil;
import com.sp.utils.LoanUtil;
import com.sp.utils.PeerLendingUtil;
import com.sp.utils.StringExtension;

public class LoanComponent {

	final String USERID = "SYS";
	final String AUTHORIZER = "SYS";

	/*
	 * 1) Dr borrower.Curr and Cr lender.Curr with accrued interest 2) Dr
	 * borrower.Curr and Cr lender.Curr with principal 3) if outstanding loan
	 * balance is 0, close the loan
	 */
	public RequestResult PrepayLoan(Loan loan, double amount, Date date)
			throws Exception {
		return PayLoan(loan, amount, date);
	}

	public RequestResult PayLoan(Loan loan, double amount, Date date)
			throws Exception {
		RequestResult re = new RequestResult();

		Member lender = PeerLendingUtil.GetMember(loan.getLenderId());
		Member borrower = PeerLendingUtil.GetMember(loan.getBorrowerId());

		// Recover accrued but unpaid interest first always
		re = ApplyAccruedInterest(loan, date);
		if (!re.isSuccess())
			return re;

		// recover principal
		re = PostPrincipal(loan, borrower.getCurrentAccount(),
				lender.getCurrentAccount(), amount);
		if (!re.isSuccess())
			return re;

		// reduce loan obligation
		re = ReduceLoanObligation(loan, borrower.getLoanAccount(),
				lender.getInvestmentAccount(), amount);
		if (!re.isSuccess())
			return re;

		// Update repayment history object
		loan.setAmountPrincipalPaid(loan.getAmountPrincipalPaid() + amount);
		// close loan if obligation <=0
		LoanEndpoint le = new LoanEndpoint();
		Loan newloan = le.getLoanByID(loan.getId()); // refresh
		if (((newloan.getAmount() - newloan.getAmountPrincipalPaid()) <= 0)
				&& (newloan.getAccruedInterest() + newloan
						.getAccruedIntInSusp()) <= 0) {
			// all the principal & Interest is paid
			this.CloseLoan(loan);
		}

		return re;
	}

	public RequestResult ApplyAccruedInterest(Loan loan, Date date)
			throws NotFoundException {

		// step 1. create loan application transactions.
		List<Transaction> txns = LoanInterestApplicationTransactions(loan);

		// step 2. post the transactions.
		RequestResult re = GLUtil.Simulate(txns);
		if (re.isSuccess()) { // try applying interest in the current accounts
			RequestResult res = TransactionPost.Post(txns);
			if (res.isSuccess()) {

				// step 3. Set varaiables
				LoanEndpoint lep = new LoanEndpoint();

				// Zerolize Accrued Interest.
				loan.setAccruedInterest(0);
				loan.setAccruedIntInSusp(0);
				loan.setLastIntAppDate(date);
				loan.setNextIntAppDate(LoanUtil.GetNextIntApplicationDate(loan,
						date));

				// update statistics
				loan.setNoOfInterestPaymentsMade(loan
						.getNoOfInterestPaymentsMade() + 1);

				// Update now
				lep.updateLoan(loan);

				re.setSuccess(true);
				re.setResultMessage("Successful");
				return re;
			}
			return res;
		} else {
			LoanEndpoint lep = new LoanEndpoint();
			loan.setNoOfInterestDefaults(loan.getNoOfInterestDefaults() + 1);
			// Update now
			lep.updateLoan(loan);
		}

		return re;
	}

	public void AccrueInterest(Loan loan, Date date) throws NotFoundException {
		// compute both normal and suspended interest
		double normalInterest = this.computeInterest("Normal", loan, date);
		double penaltyInterest = 0;
		if (loan.getAmountPrincipalDefaulted() > 0)
			penaltyInterest = this.computeInterest("Suspended", loan, date);

		// 2. Adjust interest accrual field
		loan.setAccruedInterest(loan.getAccruedInterest() + normalInterest);
		loan.setAccruedIntInSusp(loan.getAccruedIntInSusp() + penaltyInterest);

		// set accrued interest to date
		loan.setAccruedInterestToDate(loan.getAccruedInterestToDate()
				+ normalInterest);

		// 3. adjusting next accrual date
		AdjustNextAccrualDate(loan, date);
	}

	private double computeInterest(String symbol, Loan loan, Date date) {
		// period
		String period = loan.getInterestComputationTerm();
		if (StringExtension.isNullOrEmpty(period))
			period = "Y";

		InterestComponent ic = new InterestComponent();
		String method = loan.getInterestComputationMethod();

		// Amount
		double baseAmount = loan.getAmount();

		if (symbol.equals("Suspended")) {
			baseAmount = loan.getAmountPrincipalDefaulted();
		}
		if (!StringExtension.isNullOrEmpty(method)
				&& method.toUpperCase().equals("C")) {
			return ic.ComputeCompoundInterest(baseAmount,
					LoanUtil.GetAccountTerm(loan, date),
					(double) LoanUtil.GetEffectiveIntRate(symbol, loan));
		} else {
			return ic.ComputeSimpleInterest(period, baseAmount,
					LoanUtil.GetAccountTerm(loan, date),
					(double) LoanUtil.GetEffectiveIntRate(symbol, loan));
		}
	}

	private void AdjustNextAccrualDate(Loan loan, Date date)
			throws NotFoundException {
		// 1
		Date nextIntAccrualDate = LoanUtil.GetNextIntAccrualDate(loan, date);

		// 2
		loan.setLastIntAccrualDate(date);
		loan.setNextIntAccrualDate(nextIntAccrualDate);
		LoanEndpoint ep = new LoanEndpoint();
		ep.updateLoan(loan);

	}

	private RequestResult ReduceLoanObligation(Loan loan, Account loanAccount,
			Account investmentAccount, double amount) {
		TransactionType lpay = Config
				.GetTransactionType("LOANREPAYMENTTRANSACTIONTYPE");
		if (lpay == null)
			throw new NullPointerException(
					"Config Item[LOANREPAYMENTTRANSACTIONTYPE] Transaction type cannot be null");

		// the loan transaction also unblocks blocked funds
		GenericTransaction ltxn = new GenericTransaction(lpay, "LEP",
				new Date(), investmentAccount, loanAccount, amount, false,
				true, AUTHORIZER, USERID, loan.getId().toString());
		List<Transaction> txns = ltxn.GetTransactionsIncludingCommission(
				new NarrativeFormat(lpay), new NarrativeFormat(lpay));
		RequestResult re = GLUtil.Simulate(txns);
		if (re.isSuccess()) {
			return TransactionPost.Post(txns);
		} else {
			return re;
		}
	}

	private RequestResult PostPrincipal(Loan loan, Account DrAmount,
			Account CrAccount, double Amount) throws Exception {
		TransactionType tt = GLUtil.GetTransactionType(Config
				.GetLong("PREPAYLOANTRANSACTIONTYPE"));
		if (tt == null)
			throw new NullPointerException(
					"Config Item[PREPAYLOANTRANSACTIONTYPE] Transaction type cannot be null");

		List<Transaction> txns = TransactionFactory.LoanRepayment(tt,
				new Date(), DrAmount, CrAccount, Amount, "Loan repayment", loan
						.getId().toString(), USERID, AUTHORIZER);
		RequestResult re = GLUtil.Simulate(txns);
		if (re.isSuccess()) {
			return TransactionPost.Post(txns);
		} else {
			return re;
		}
	}

	private List<Transaction> LoanInterestApplicationTransactions(Loan loan) {

		// create all transactions
		List<Transaction> txns = new ArrayList<Transaction>();

		TransactionType intt = Config
				.GetTransactionType("INTERESTTRANSACTIONTYPE");
		if (intt == null)
			throw new NullPointerException("Transaction type cannot be null");

		Account paidAccount = GLUtil.GetAccount(loan.getIntPaidAccount()); // lender.CurrentAcc
																			// or
																			// loan
																			// account
		Account payingAccount = GLUtil.GetAccount(loan.getIntPayingAccount());// borrower.CurrentAcc

		// Accrue interest
		GenericTransaction inttxn = new GenericTransaction(intt, "INT",
				new Date(), payingAccount, paidAccount,
				loan.getAccruedInterest() + loan.getAccruedIntInSusp(), false,
				true, AUTHORIZER, USERID, loan.getId().toString());

		txns.addAll(inttxn.GetTransactionsIncludingCommission(
				new NarrativeFormat(intt), new NarrativeFormat(intt)));

		return txns;
	}

	private void CloseLoan(Loan loan) throws NotFoundException {
		// Remove scheduled STOs
		STOEndpoint se = new STOEndpoint();
		se.deleteSTOByLoanId(loan.getId());

		// Mark the loan as closed
		loan.setStatus("Closed");

		LoanEndpoint le = new LoanEndpoint();
		le.updateLoan(loan);
	}

}
