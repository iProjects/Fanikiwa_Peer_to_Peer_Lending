package com.sp.fanikiwa.business;

import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.ForbiddenException;
import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.Enums.OfferStatus;
import com.sp.fanikiwa.Enums.PostingCheckFlag;
import com.sp.fanikiwa.Enums.RepaymentInterval;
import com.sp.fanikiwa.Enums.STOCommissionChargeWho;
import com.sp.fanikiwa.Enums.STOType;
import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.LoanEndpoint;
import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.api.OfferEndpoint;
import com.sp.fanikiwa.api.OfferReceipientEndpoint;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.financialtransactions.GenericTransaction;
import com.sp.fanikiwa.business.financialtransactions.NarrativeFormat;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.BatchSimulateStatus;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferReceipient;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.SimulatePostStatus;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.fanikiwa.pdf.ContractPDF;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.LoanUtil;
import com.sp.utils.MailUtil;
import com.sp.utils.PeerLendingUtil;

public class AcceptOfferComponent {
	public AcceptOfferComponent() {

	}

	// / <summary>
	// / AcceptBorrowOffer business method.
	// / </summary>
	// / <param name="id">A id value.</param>
	// / <param name="account">A account value.</param>
	// / <param name="loan">A loan value.</param>
	// / <param name="aBorrowOffer">A offer value.</param>
	// /
	String userID = "SYS";
	String Authorizer = "Auth";

	public Loan AcceptBorrowOffer(Member lender, Offer aBorrowOffer)
			throws Exception {
		// realize accept offer usecase
		Loan loan = null;

		ValidateOffer(aBorrowOffer, lender);
		PeerLendingUtil.SetOfferStatus(aBorrowOffer, OfferStatus.Processing);
		try {

			// get the borrower from the offer
			MemberEndpoint mDAC = new MemberEndpoint();
			Member borrower = aBorrowOffer.getMember();
			if (borrower.getMemberId() == lender.getMemberId()) {
				// Before throwing the error, revert status to open
				throw new ForbiddenException("Cannot accept self offers");
			}

			// Check ability to pay
			List<Transaction> txns = LoanTransactions(lender, borrower,
					aBorrowOffer);
			if (txns.size() < 4) {
				// Before throwing the error, revert status to open
				throw new ForbiddenException(
						"Loan Transactions not well formed");
			}

			BatchSimulateStatus bss = TransactionPost.SimulatePost(txns,
					PostingCheckFlag.CheckLimitAndPassFlag);
			boolean canPost = bss.CanPost();
			if (!canPost) {
				String msg = "";
				for (SimulatePostStatus s : bss.SimulateStatus) {
					for (Exception e : s.Errors) {
						msg += e.getMessage() + "\n";
					}
				}
				throw new Exception("Simulation Error: \n" + msg);
			}

			// create loan
			loan = CreateLoan(borrower, lender, aBorrowOffer);
			PeerLendingUtil.SetOfferStatus(aBorrowOffer, OfferStatus.Closed);

		} catch (Exception e) {
			PeerLendingUtil.SetOfferStatus(aBorrowOffer, OfferStatus.Open);
			throw e;
		}
		return loan;
	}

	// <summary>
	// AcceptLendOffer business method.
	// </summary>
	// <param name="loan">A loan value.</param>

	public Loan AcceptLendOffer(Member borrower, Offer aLendOffer)
			throws Exception {
		// realize accept offer usecase
		Loan loan = null;

		ValidateOffer(aLendOffer, borrower);
		PeerLendingUtil.SetOfferStatus(aLendOffer, OfferStatus.Processing);
		try {

			// get the lender from the offer
			MemberEndpoint mDAC = new MemberEndpoint();
			Member lender = aLendOffer.getMember(); // mDAC.getMemberByID(aLendOffer.getMemberId());
			if (borrower.getMemberId() == lender.getMemberId()) {
				throw new ForbiddenException("Cannot accept self offers");
			}

			AccountEndpoint aep = new AccountEndpoint();
			Account lenderCurr = lender.getCurrentAccount();
			if (GLUtil.CheckLimit(lenderCurr, aLendOffer.getAmount())) {
				aep.UnBlockFunds(lenderCurr, aLendOffer.getAmount());
				loan = CreateLoan(borrower, lender, aLendOffer);
				PeerLendingUtil.SetOfferStatus(aLendOffer, OfferStatus.Closed);
			} else {
				throw new Exception("Insufficient Limit");
			}

		} catch (Exception e) {
			// Before throwing the error, revert status to open
			PeerLendingUtil.SetOfferStatus(aLendOffer, OfferStatus.Open);
			throw e;
		}
		return loan;
	}

	private void ValidateOffer(Offer offer, Member acceptee)
			throws ForbiddenException {

		if (offer.getStatus().equals("Processing")) {
			throw new NullPointerException(MessageFormat.format(
					"Cannot accept Offer [{0}], Status is Processing. ", offer
							.getId().toString()));
		}
		if (offer.getStatus().equals("Closed")) {
			throw new NullPointerException(MessageFormat.format(
					"Cannot accept Offer [{0}],,  Status is Closed. ", offer
							.getId().toString()));
		}
		if (offer.getStatus().equals("Edit")) {
			throw new NullPointerException(MessageFormat.format(
					"Cannot accept Offer [{0}],,  Status is Edit. ", offer
							.getId().toString()));
		}
		if (offer.getExpiryDate().before(new Date())) {
			throw new ForbiddenException(MessageFormat.format(
					"Cannot accept Offer [{0}] since it is expired. ", offer
							.getId().toString()));
		}
		if (!offer.getPrivateOffer() && !PrivateOfferred(offer, acceptee)) {
			// the offer is a private offer and you dont exist in the offerees
			// list
			throw new ForbiddenException(
					MessageFormat
							.format("Cannot accept Offer [{0}], private offer is not offerred to you. ",
									offer.getId().toString()));
		}
	}

	private boolean PrivateOfferred(Offer offer, Member member) {
		OfferReceipientEndpoint oep = new OfferReceipientEndpoint();
		OfferReceipient or = new OfferReceipient();
		or.setOffer(offer);
		or.setMember(member);
		return (oep.isOfferAvaiable(or) == null);
	}

	private Loan CreateLoan(Member borrower, Member lender, Offer offer)
			throws NotFoundException, ConflictException {
		/*
		 * 1. The system blocks the ‘lend offer’ so that other potential
		 * borrowers do not accept the offer
		 * 
		 * 2. The system establishes the loan in the loan book
		 * 
		 * 3. The system logs the loan repayment schedule in the diary
		 * 
		 * 4. The systems creates electronic loan contract
		 * 
		 * 5. The AccountingSystem posts the loan transaction with its attendant
		 * commission
		 * 
		 * 6. The system closes the ‘lend offer’
		 * 
		 * 7. The MessagingSystem sends the electronic loan contract
		 * 
		 * 8. The MessagingSystem informs both the lender and borrower of the
		 * successful transaction
		 */

		// STEP 1 Establish Loan
		Loan loan = this.EstablishLoan(lender, borrower, offer);

		// STEP 2 Log Loan repayment scedhule
		LogLoanRepayment(lender, borrower, loan);

		// SETP 3 Post loan transation
		TransactionPost.Post(LoanTransactions(lender, borrower, offer));

		String msg = "Fanikiwa Loan established. Amount:" + loan.getAmount()
				+ " Interest: " + loan.getInterestRate() + " Term: "
				+ loan.getTerm();
		MailUtil.sendEmailWithPDF(borrower.getEmail(), "Fanikiwa Contract",
				msg, "FanikiwaContract.PDF", ContractPDF.class);

		return loan;
	}

	// <summary>
	// AcceptPartialBorrowOffer business method.
	// </summary>
	// <param name="loan">A loan value.</param>
	// <param name="offer">A offer value.</param>
	public Loan AcceptPartialBorrowOffer(Member lender, Offer partialoffer)
			throws ForbiddenException, NotFoundException, ConflictException {
		// realize accept offer usecase
		Loan loan;

		// Get offer
		OfferEndpoint oep = new OfferEndpoint();
		Offer _offer = oep.getOfferByID(partialoffer.getId());
		// check that partial offer is less or equal to offer amount
		if (partialoffer.getAmount() > _offer.getAmount()) {
			throw new ForbiddenException(
					"Accepted Amount is greater than Offer Amount!");
		}

		// get the borrower from the offer
		Member borrower = partialoffer.getMember();

		loan = CreateLoan(borrower, lender, partialoffer);

		// decrease offer amount. Use the OfferDAC
		_offer.setAmount(_offer.getAmount() - partialoffer.getAmount());

		// offer.Amount = offer.Amount - loan.Amount;
		if (_offer.getAmount() <= 0) {
			// Offer fully subscribed. Change the offer status to closed.
			PeerLendingUtil.SetOfferStatus(_offer, OfferStatus.Closed);
		} else {
			// unlock the offer. Change the offer status to Open.
			PeerLendingUtil.SetOfferStatus(_offer, OfferStatus.Open);
		}
		return loan;
	}

	public Loan EstablishLoan(Member lender, Member borrower, Offer offer)
			throws NotFoundException, ConflictException {
		LoanEndpoint loanepC = new LoanEndpoint();
		// create a new loan
		Loan loan = new Loan();

		// fill up loan details from offer details
		loan.setTerm(offer.getTerm());
		loan.setAmount(offer.getAmount());
		// loan.setInterest(offer.getInterest());
		loan.setMaturityDate(offer.getExpiryDate());
		loan.setCreatedDate(new Date());
		loan.setBorrowerId(borrower.getMemberId());
		loan.setLenderId(lender.getMemberId());
		loan.setOfferId(offer.getId());
		loan.setPartialPay(offer.getPartialPay());
		loan.setInterestAccrualInterval(Config
				.GetString("DEFAULT_INT_ACCRUAL_INTERVAL")); // D, D1, D360,
																// D365, M, M30,
																// Y
		loan.setInterestApplicationMethod(Config
				.GetString("DEFAULT_INT_APPLICATION_METHOD"));
		loan.setInterestComputationMethod(Config
				.GetString("DEFAULT_INT_COMPUTATION_METHOD"));// S, C
		loan.setInterestComputationTerm(Config
				.GetString("DEFAULT_INT_COMPUTATION_TERM")); // D, D1, D360,
																// D365, M, M30,
																// Y
		loan.setInterestRate(offer.getInterest());
		loan.setInterestRateSusp(offer.getInterest()
				+ Config.GetDouble("PENALTY_INTEREST_MARGIN"));
		loan.setIntPayingAccount(borrower.getCurrentAccount().getAccountID());
		loan.setIntPaidAccount(lender.getCurrentAccount().getAccountID());
		// loan.setLastIntAppDate(new Date());
		loan.setNextIntAppDate(LoanUtil.GetNextIntApplicationDate(loan,
				new Date()));

		// Accrue interest immediately
		// loan.setLastIntAccrualDate(new Date());
		loan.setNextIntAccrualDate(new Date());
		loan.setLastIntAccrualDate(new Date());

		// ESTABLISHLOANTRANSACTIONTYPE
		loan.setTransactionType(Config.GetLong("ESTABLISHLOANTRANSACTIONTYPE"));
		loan.setStatus("Open");

		// Now create the loan in the loan book
		return loanepC.insertLoan(loan);
	}

	public void LogLoanRepayment(Member lender, Member borrower, Loan loan)
			throws NotFoundException, ConflictException {
		/*
		 * We create 2 STOs STO 1 - for Repoaying Dr Borrower.CurrentID with
		 * PayAmount Cr Investor.CurrentAcc with PayAmount
		 */

		if (loan.getTerm() != 0) {

			STO lr = new STO();

			// fill up the repayment schedule
			lr.setLoanId(loan.getId());
			lr.setInterval(RepaymentInterval.M.toString()); // Create enum
															// called
															// RapaymentInterval
			lr.setSTOType(STOType.Normal.name());
			lr.setNoOfPayments(loan.getTerm()); // no of payments is loan terms
			lr.setCreateDate(new Date());
			lr.setStartDate(new Date()); // when does repayment start);
			lr.setNextPayDate(DateExtension.addMonths(lr.getStartDate(), 1)); // next
																				// repayment
																				// starts
																				// a
																				// month
																				// from
																				// today
			lr.setEndDate(DateExtension.addMonths(lr.getStartDate(),
					lr.getNoOfPayments())); // when does repayment end?
											// Repayment
											// ends start date plus no of
											// payments
											// months
			lr.setDrAccount(borrower.getCurrentAccount().getAccountID()); // during
																			// loan
																			// repayment
																			// debit
																			// borrower
			lr.setCrAccount(lender.getCurrentAccount().getAccountID()); // during
																		// loan
																		// repayment
																		// credit
																		// lender
			lr.setCommissionAccount(Config.GetLong("COMMISSIONACCOUNT"));
			lr.setDrTxnType(Config.GetLong("LOANDRAWTRANSACTIONTYPE"));
			lr.setCrTxnType(Config.GetLong("LOANDRAWTRANSACTIONTYPE"));
			lr.setAmountPaid(0);

			if (loan.getTerm() != 0) // Re
			{
				lr.setPayAmount(((loan.getAmount() + loan.getAccruedInterest()) / loan
						.getTerm()));
				lr.setInterestAmount(loan.getAccruedInterest());
			}

			lr.setTotalToPay(loan.getAmount() + loan.getAccruedInterest());
			lr.setFeesFlag(Config.GetInt("LOANREPAYMENTFEESFLAG"));
			String cWho = Config.GetString("CHARGEWHOFLAG");
			// parse this string e.g. Borrowr to retun short 0

			// try this; howerver you will need to include error checking just
			// in case a wrong setting is put in the database
			STOCommissionChargeWho enumWho = STOCommissionChargeWho
					.valueOf(cWho);
			lr.setChargeWho((short) enumWho.getValue());
			lr.setLimitFlag(0);
			lr.setPartialPay(loan.isPartialPay());

			STOEndpoint stPost = new STOEndpoint();
			stPost.insertSTO(lr);
		}
		// else this was a grant. identified by a zero term
	}

	public List<Transaction> LoanTransactions(Member lender, Member borrower,
			Offer offer) {
		// Use this for all general ledger methods that does not post

		// create all transactions
		List<Transaction> txns = new ArrayList<Transaction>();
		InterestComponent ic = new InterestComponent();

		// establish loan with attendant commission
		TransactionType tt = Config
				.GetTransactionType("ESTABLISHLOANTRANSACTIONTYPE");
		TransactionType intt = Config
				.GetTransactionType("INTERESTTRANSACTIONTYPE");
		if (tt == null)
			throw new NullPointerException(
					"Config Item[ESTABLISHLOANTRANSACTIONTYPE] Transaction type cannot be null");
		if (intt == null)
			throw new NullPointerException(
					"Config Item[INTERESTTRANSACTIONTYPE] Transaction type cannot be null");

		// the loan transaction also unblocks blocked funds
		GenericTransaction ltxn = new GenericTransaction(tt, "LES", new Date(),
				borrower.getLoanAccount(), lender.getInvestmentAccount(),
				offer.getAmount(), false, true, Authorizer, userID, offer
						.getId().toString());
		// Txn 1
		txns.addAll(ltxn.GetTransactionsIncludingCommission(
				new NarrativeFormat(tt), new NarrativeFormat(tt)));

		// Accrue interest
		// Default interest is Simple and only computation method for this
		// version
		// /TODO Rethink interest accrual
		/*
		 * double interest = ic.ComputeSimpleInterest(offer.getAmount(),
		 * offer.getTerm(), (double) offer.getInterest()); GenericTransaction
		 * inttxn = new GenericTransaction(intt, "INT", new Date(),
		 * borrower.getinterestExpAccount(), lender.getinterestIncAccount(),
		 * interest, false, "Y", Authorizer, userID, offer.getId().toString());
		 * 
		 * // Txn 2 txns.addAll(inttxn.GetTransactionsIncludingCommission( new
		 * NarrativeFormat(tt), new NarrativeFormat(tt)));
		 */

		// Disburse Amount
		TransactionType Distt = Config
				.GetTransactionType("DISBURSELOANTRANSACTIONTYPE");
		if (Distt == null)
			throw new NullPointerException(
					"Config Item [DISBURSELOANTRANSACTIONTYPE] Transaction type cannot be null");

		GenericTransaction Distxn = new GenericTransaction(Distt, "DIS",
				new Date(), lender.getCurrentAccount(),
				borrower.getCurrentAccount(), offer.getAmount(), false, true,
				Authorizer, userID, offer.getId().toString());
		// Txn 3
		txns.addAll(Distxn.GetTransactionsIncludingCommission(
				new NarrativeFormat(Distt), new NarrativeFormat(Distt)));
		return txns;
	}

}
