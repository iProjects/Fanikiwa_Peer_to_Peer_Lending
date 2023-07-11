package com.sp.fanikiwa.business.SMS;

import java.text.ParseException;
import java.util.Arrays;
import java.util.Date;
import java.util.List;

import com.sp.fanikiwa.Enums.FanikiwaMessageType;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.StringExtension;

public class FanikiwaMessageFactory {

	public static FanikiwaMessage CreateMessage(String OriginatingAddress,
			Date MessageDate, String Body) {
		FanikiwaMessage fmessage = null;
		try {
			/* put condition that identifies a MpesaMessage */
			if (OriginatingAddress.toUpperCase().equals("MPESA")) {
				// convert to MpesaMessage
				fmessage = ParseMpesaMessage(OriginatingAddress, MessageDate,
						Body);
			} else // test if this message is structured
			{
				String delimiters = "\\s+|\\*+|\\#+"; // | delimeted
				List<String> msgParams = Arrays.asList(Body.split(delimiters));

				// Help message syntax = H | HELP <Command>
				if ("H".equals(msgParams.get(0).toUpperCase())
						|| "H2".equals(msgParams.get(0).toUpperCase())
						|| "H3".equals(msgParams.get(0).toUpperCase())
						|| "HELP".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseHelpMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = B|BAL ; try parsing Balance Enquiry
				// <BAL><Password><account>
				else if ("B".equals(msgParams.get(0).toUpperCase())
						|| "BAL".equals(msgParams.get(0).toUpperCase())
						|| "BALANCE".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseBalanceEnquiryMessage(OriginatingAddress,
							MessageDate, Body, msgParams);
				}
				// Message starts with = S|STAT ; try parsing Statement Enquiry
				// <STAT><Password><account><Startdate><EndDate>
				else if ("S".equals(msgParams.get(0).toUpperCase())
						|| "STAT".equals(msgParams.get(0).toUpperCase())
						|| "STATEMENT".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseStatementEnquiryMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				} else if ("M".equals(msgParams.get(0).toUpperCase())
						|| "MS".equals(msgParams.get(0).toUpperCase())
						|| "MINI".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseStatementEnquiryMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = LR|PAY ; LR<Password><OfferID><Amount>
				else if ("LR".equals(msgParams.get(0).toUpperCase())
						|| "PAY".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseEarlyLoanRepaymentMessage(
							OriginatingAddress, MessageDate, Body, msgParams);

				}
				// Message starts with = R|RE|REG|REGISTER ;
				// R<Password><Email><NationalId>
				else if ("R".equals(msgParams.get(0).toUpperCase())
						|| "RE".equals(msgParams.get(0).toUpperCase())
						|| "REG".equals(msgParams.get(0).toUpperCase())
						|| "REGISTER".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseRegisterMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = D|DE|DEREG|DEREGISTER ; D<Password>
				else if ("D".equals(msgParams.get(0).toUpperCase())
						|| "DE".equals(msgParams.get(0).toUpperCase())
						|| "DEREG".equals(msgParams.get(0).toUpperCase())
						|| "DEREGISTER".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseDeRegisterMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = MLO ;
				// MLO<Password><Amount><Term><Interest>
				else if ("MLO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseMakeLendOfferMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = MBO ;
				// MBO<Password><Amount><Term><Interest>
				else if ("MBO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseMakeBorrowOfferMessage(OriginatingAddress,
							MessageDate, Body, msgParams);
				}
				// Message starts with = ALO ; ALO<Password><OfferId><Amount>
				else if ("ALO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseAcceptLendOfferMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = ABO ; ABO<Password><OfferId><Amount>
				else if ("ABO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseAcceptBorrowOfferMessage(
							OriginatingAddress, MessageDate, Body, msgParams);

				}
				// Message starts with = LO ; LO<Password>
				else if ("LO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseLendOffersMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = BO ; BO<Password>
				else if ("BO".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseBorrowOffersMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = AL ; AL<Password>
				else if ("FA".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseAccountsListMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = C|CP ;
				// CP<OldPassword><NewPassword><ConfirmPassword>
				else if ("C".equals(msgParams.get(0).toUpperCase())
						|| "CP".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseChangePinMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				}
				// Message starts with = W|WIT|WITHDRAW ; W<Password><Amount>
				else if ("W".equals(msgParams.get(0).toUpperCase())
						|| "WITHDRAW".equals(msgParams.get(0).toUpperCase())) {
					fmessage = ParseWithdrawMessage(OriginatingAddress,
							MessageDate, Body, msgParams);

				} else {
					// this is not a fanikiwa message -- Do nothing; it will be
					// removed from the sim
					// inform sender with help commands
					fmessage = ParseHelpMessage(OriginatingAddress,
							MessageDate, Body, msgParams);
				}

			}
		} catch (Exception ex) {
			ErrorMessage error = new ErrorMessage();
			// populate generic from abstract
			error.SenderTelno = OriginatingAddress;
			error.MessageDate = MessageDate;
			error.FanikiwaMessageType = FanikiwaMessageType.ErrorMessage;
			error.Body = Body;
			error.Status = "NEW";
			error.Exception = ex;
			return error;
		}
		return fmessage;
	}

	// implementations
	private static BalanceEnquiryMessage ParseBalanceEnquiryMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = BALSymbol*Pwd*<AccountID|AccountLabel>
		BalanceEnquiryMessage balEnq = new BalanceEnquiryMessage();
		// populate generic from abstract
		balEnq.SenderTelno = OriginatingAddress;
		balEnq.MessageDate = MessageDate;
		balEnq.FanikiwaMessageType = FanikiwaMessageType.BalanceEnquiryMessage;
		balEnq.Body = Body;
		balEnq.Status = "NEW";
		balEnq.AccountLabel = "C";

		// parse pwd: Not Optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}

		balEnq.Pwd = msgParams.get(1);

		// parse account: Optional
		if (msgParams.size() > 2) {
			String accstr = msgParams.get(2);
			Long AccId;
			try {
				AccId = Long.parseLong(accstr);
				balEnq.AccountLabel = null;
				balEnq.AccountId = AccId;
			} catch (NumberFormatException e) {
				balEnq.AccountLabel = accstr;
				balEnq.AccountId = (long) 0;
			}
		} else {
			return balEnq;
		}

		return balEnq;
	}

	private static StatementEnquiryMessage ParseStatementEnquiryMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) throws ParseException {
		// nSyntax = STATSymbol*Pwd[*<AccountID|AccountLabel>*StartDate*EndDate]
		StatementEnquiryMessage st = new StatementEnquiryMessage();

		// populate generic from abstract
		st.SenderTelno = OriginatingAddress;
		st.MessageDate = MessageDate;
		st.FanikiwaMessageType = FanikiwaMessageType.StatementEnquiryMessage;
		st.Body = Body;
		st.Status = "NEW";
		st.AccountLabel = "C";
		Date startdate = DateExtension.addMonths(new Date(),
				-1 * Config.GetInt("STATEMENTMONTHS"));
		st.StartDate = startdate;
		Date enddate = new Date();
		st.EndDate = enddate;

		// parse pwd: Not Optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		st.Pwd = msgParams.get(1);

		// parse account: Optional
		if (msgParams.size() > 2) {
			String accstr = msgParams.get(2);
			Long AccId;
			try {
				AccId = Long.parseLong(accstr);
				st.AccountLabel = null;
				st.AccountId = AccId;
			} catch (NumberFormatException e) {
				st.AccountLabel = accstr;
				st.AccountId = (long) 0;
			}
		} else {
			return st;
		}

		// parse startdate: Optional

		if (msgParams.size() > 3) {
			String sdate = msgParams.get(3);
			startdate = DateExtension.parse(sdate);
		} else {
			return st;
		}

		// parse enddate: Optional

		if (msgParams.size() > 4) {
			String edate = msgParams.get(4);
			enddate = DateExtension.parse(edate);
		} else {
			return st;
		}
		return st;
	}

	private static MiniStatementEnquiryMessage ParseMiniStatementEnquiryMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = MS*Pwd*<AccountID|AccountLabel>
		MiniStatementEnquiryMessage st = new MiniStatementEnquiryMessage();

		// populate generic from abstract
		st.SenderTelno = OriginatingAddress;
		st.MessageDate = MessageDate;
		st.FanikiwaMessageType = FanikiwaMessageType.MiniStatementEnquiryMessage;
		st.Body = Body;
		st.Status = "NEW";
		st.AccountLabel = "C";

		// parse pwd: Not Optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		st.Pwd = msgParams.get(1);

		// parse account: Optional
		if (msgParams.size() > 2) {
			String accstr = msgParams.get(2);
			Long AccId;
			try {
				AccId = Long.parseLong(accstr);
				st.AccountLabel = null;
				st.AccountId = AccId;
			} catch (NumberFormatException e) {
				st.AccountLabel = accstr;
				st.AccountId = (long) 0;
			}
		} else {
			return st;
		}

		return st;
	}

	private static EarlyLoanRepaymentMessage ParseEarlyLoanRepaymentMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		EarlyLoanRepaymentMessage lr = new EarlyLoanRepaymentMessage();
		// populate generic from abstract
		lr.SenderTelno = OriginatingAddress;
		lr.MessageDate = MessageDate;
		lr.FanikiwaMessageType = FanikiwaMessageType.EarlyLoanRepaymentMessage;
		lr.Body = Body;
		lr.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Loan Repayment message. ");
		}
		lr.Pwd = pwd;

		// parse offerid: Not optional
		String offer = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(offer)) {
			throw new NullPointerException(
					"Offer id cannot be null in a Loan Repayment message. ");
		}

		lr.OfferId = Long.parseLong(offer);

		// parse amount: Not optional
		String amountstr = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(amountstr)) {
			throw new NullPointerException(
					"Amount cannot be null in a Loan Repayment message. ");
		}

		lr.Amount = Double.parseDouble(amountstr);

		return lr;
	}

	private static RegisterMessage ParseRegisterMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		RegisterMessage rm = new RegisterMessage();

		// populate generic from abstract
		rm.SenderTelno = OriginatingAddress;
		rm.MessageDate = MessageDate;
		rm.FanikiwaMessageType = FanikiwaMessageType.RegisterMessage;
		rm.Body = Body;
		rm.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Register message. ");
		}
		rm.Pwd = pwd;

		// parse email: Not optional
		String email = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(email)) {
			throw new NullPointerException(
					"Email cannot be null in a Register message. ");
		}
		rm.Email = email.toLowerCase();

		// parse nationalId: Not optional
		String nationalId = msgParams.get(3);
		if (StringExtension.isNullOrEmpty(nationalId)) {
			throw new NullPointerException(
					"NationalID cannot be null in a Register message. ");
		}
		rm.NationalID = nationalId;

		// parse Notification method: Optional
		// default Notification method is SMS for SMS requests
		rm.NotificationMethod = "SMS";
		if (msgParams.size() > 4) {
			String param = msgParams.get(4);

			if (!StringExtension.isNullOrEmpty(param)) {
				rm.NotificationMethod = param;
			}
		}
		return rm;
	}

	private static DeRegisterMessage ParseDeRegisterMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		DeRegisterMessage drm = new DeRegisterMessage();

		// populate generic from abstract
		drm.SenderTelno = OriginatingAddress;
		drm.MessageDate = MessageDate;
		drm.FanikiwaMessageType = FanikiwaMessageType.DeRegisterMessage;
		drm.Body = Body;
		drm.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a DeRegister message. ");
		}
		drm.Pwd = pwd;

		// parse email: Not optional
		String email = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(email)) {
			throw new NullPointerException(
					"Email cannot be null in a DeRegister message. ");
		}
		drm.Email = email.toLowerCase();

		return drm;
	}

	private static MakeLendOfferMessage ParseMakeLendOfferMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = MLO*Pwd*Amount*InterestRate*Term
		MakeLendOfferMessage mlo = new MakeLendOfferMessage();

		// populate generic from abstract
		mlo.SenderTelno = OriginatingAddress;
		mlo.MessageDate = MessageDate;
		mlo.FanikiwaMessageType = FanikiwaMessageType.MakeLendOfferMessage;
		mlo.Body = Body;
		mlo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Make Lend Offer message. ");
		}
		mlo.Pwd = pwd;

		// parse Amount: Not optional
		String amountstr = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(amountstr)) {
			throw new NullPointerException(
					"Amount cannot be null in a Make Lend Offer message. ");
		}

		mlo.Amount = Double.parseDouble(amountstr);
		;

		// parse InterestRate: Not optional
		String InterestRatestr = msgParams.get(3);
		if (StringExtension.isNullOrEmpty(InterestRatestr)) {
			throw new NullPointerException(
					"Interest Rate cannot be null in a Make Lend Offer message. ");
		}

		mlo.InterestRate = Double.parseDouble(InterestRatestr);
		;

		// parse Term: Not optional
		String termstr = msgParams.get(4);
		if (StringExtension.isNullOrEmpty(termstr)) {
			throw new NullPointerException(
					"Term cannot be null in a Make Lend Offer message. ");
		}

		mlo.Term = Integer.parseInt(termstr);
		return mlo;
	}

	private static MakeBorrowOfferMessage ParseMakeBorrowOfferMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = MBO*Pwd*Amount*InterestRate*Term
		MakeBorrowOfferMessage mbo = new MakeBorrowOfferMessage();

		// populate generic from abstract
		mbo.SenderTelno = OriginatingAddress;
		mbo.MessageDate = MessageDate;
		mbo.FanikiwaMessageType = FanikiwaMessageType.MakeBorrowOfferMessage;
		mbo.Body = Body;
		mbo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Make Borrow Offer message. ");
		}
		mbo.Pwd = pwd;

		// parse Amount: Not optional
		String amountstr = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(amountstr)) {
			throw new NullPointerException(
					"Amount cannot be null in a Make Borrow Offer message. ");
		}

		mbo.Amount = Double.parseDouble(amountstr);

		// parse InterestRate: Not optional
		String InterestRatestr = msgParams.get(3);
		if (StringExtension.isNullOrEmpty(InterestRatestr)) {
			throw new NullPointerException(
					"Interest Rate cannot be null in a Make Borrow Offer message. ");
		}

		mbo.InterestRate = Double.parseDouble(InterestRatestr);

		// parse Term: Not optional
		String termstr = msgParams.get(4);
		if (StringExtension.isNullOrEmpty(termstr)) {
			throw new NullPointerException(
					"Term cannot be null in a Make Borrow Offer message. ");
		}

		mbo.Term = Integer.parseInt(termstr);

		return mbo;
	}

	private static AcceptLendOfferMessage ParseAcceptLendOfferMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		AcceptLendOfferMessage alo = new AcceptLendOfferMessage();

		// populate generic from abstract
		alo.SenderTelno = OriginatingAddress;
		alo.MessageDate = MessageDate;
		alo.FanikiwaMessageType = FanikiwaMessageType.AcceptLendOfferMessage;
		alo.Body = Body;
		alo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in Accept Lend Offer message. ");
		}
		alo.Pwd = pwd;

		// parse offerid: Not optional
		String offer = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(offer)) {
			throw new NullPointerException(
					"Offer id cannot be null in Accept Lend Offer message. ");
		}

		alo.OfferId = Long.parseLong(offer);

		return alo;
	}

	private static AcceptBorrowOfferMessage ParseAcceptBorrowOfferMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = ABO*Pwd*OfferId
		AcceptBorrowOfferMessage abo = new AcceptBorrowOfferMessage();

		// populate generic from abstract
		abo.SenderTelno = OriginatingAddress;
		abo.MessageDate = MessageDate;
		abo.FanikiwaMessageType = FanikiwaMessageType.AcceptBorrowOfferMessage;
		abo.Body = Body;
		abo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in Accept Borrow Offer message. ");
		}
		abo.Pwd = pwd;

		// parse offerid: Not optional
		String offer = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(offer)) {
			throw new NullPointerException(
					"Offer id cannot be null in a Accept Borrow message. ");
		}

		abo.OfferId = Long.parseLong(offer);

		return abo;
	}

	private static LendOffersMessage ParseLendOffersMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		LendOffersMessage lo = new LendOffersMessage();

		// populate generic from abstract
		lo.SenderTelno = OriginatingAddress;
		lo.MessageDate = MessageDate;
		lo.FanikiwaMessageType = FanikiwaMessageType.LendOffersMessage;
		lo.Body = Body;
		lo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in Lend Offers message. ");
		}
		lo.Pwd = pwd;

		return lo;
	}

	private static BorrowOffersMessage ParseBorrowOffersMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		BorrowOffersMessage bo = new BorrowOffersMessage();

		// populate generic from abstract
		bo.SenderTelno = OriginatingAddress;
		bo.MessageDate = MessageDate;
		bo.FanikiwaMessageType = FanikiwaMessageType.BorrowOffersMessage;
		bo.Body = Body;
		bo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Borrow Offers message. ");
		}
		bo.Pwd = pwd;

		return bo;
	}
	
	private static FanikiwaAccountsMessage ParseAccountsListMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		FanikiwaAccountsMessage acc = new FanikiwaAccountsMessage();

		// populate generic from abstract
		acc.SenderTelno = OriginatingAddress;
		acc.MessageDate = MessageDate;
		acc.FanikiwaMessageType = FanikiwaMessageType.AccountsListMessage;
		acc.Body = Body;
		acc.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in accounts list message. ");
		}
		acc.Pwd = pwd;

		return acc;
	}

	private static ChangePinMessage ParseChangePinMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		ChangePinMessage cp = new ChangePinMessage();

		// populate generic from abstract
		cp.SenderTelno = OriginatingAddress;
		cp.MessageDate = MessageDate;
		cp.FanikiwaMessageType = FanikiwaMessageType.ChangePINMessage;
		cp.Body = Body;
		cp.Status = "NEW";

		// parse oldpwd: Not optional
		String oldpwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(oldpwd)) {
			throw new NullPointerException(
					"Old Password cannot be null in Change Pin message. ");
		}
		cp.OldPassword = oldpwd;

		// parse newpwd: Not optional
		String newpwd = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(newpwd)) {
			throw new NullPointerException(
					"New Password cannot be null in Change Pin message. ");
		}
		cp.NewPassword = newpwd;

		// parse confirmpwd: Not optional
		String confirmpwd = msgParams.get(3);
		if (StringExtension.isNullOrEmpty(confirmpwd)) {
			throw new NullPointerException(
					"Confirm Password cannot be null in Change Pin message. ");
		}
		cp.ConfirmPassword = confirmpwd;

		return cp;
	}

	private static WithdrawMessage ParseWithdrawMessage(
			String OriginatingAddress, Date MessageDate, String Body,
			List<String> msgParams) {
		// Syntax = WithdrawSymbol*Pwd*Amount
		WithdrawMessage mo = new WithdrawMessage();

		// populate generic from abstract
		mo.SenderTelno = OriginatingAddress;
		mo.MessageDate = MessageDate;
		mo.FanikiwaMessageType = FanikiwaMessageType.WithdrawMessage;
		mo.Body = Body;
		mo.Status = "NEW";

		// parse pwd: Not optional
		if (msgParams.size() < 2) {
			throw new NullPointerException("Password required");
		}
		String pwd = msgParams.get(1);
		if (StringExtension.isNullOrEmpty(pwd)) {
			throw new NullPointerException(
					"Password cannot be null in a Withdraw message. ");
		}
		mo.Pwd = pwd;

		// parse Amount: Not optional
		String amountstr = msgParams.get(2);
		if (StringExtension.isNullOrEmpty(amountstr)) {
			throw new NullPointerException(
					"Amount cannot be null in a Withdraw message. ");
		}
		double Amount = Double.parseDouble(amountstr);
		mo.Amount = Amount;

		return mo;
	}

	private static HelpMessage ParseHelpMessage(String OriginatingAddress,
			Date MessageDate, String Body, List<String> msgParams) {
		HelpMessage help = new HelpMessage();
		// populate generic from abstract
		// help.MemberId = GetMemberFromPhone(OriginatingAddress);//get member
		// id from telno
		help.SenderTelno = OriginatingAddress;
		help.MessageDate = MessageDate;
		help.FanikiwaMessageType = FanikiwaMessageType.HelpMessage;
		help.Body = Body;
		help.Status = "NEW";

		if (msgParams.size() > 1)
			help.HelpCommand = msgParams.get(1);
		return help;
	}

	private static MpesaDepositMessage ParseMpesaMessage(
			String OriginatingAddress, Date MessageDate, String Body)
			throws ParseException {
		
		MpesaDepositMessage mo = new MpesaDepositMessage();
		
		mo.SenderTelno = OriginatingAddress;
		mo.FanikiwaMessageType = FanikiwaMessageType.MpesaDepositMessage;
		mo.Body = Body;
		mo.Status = "NEW";
		
		String[] fieldPairs = Body.toUpperCase().trim().split("\\,");
   
		for (String field : fieldPairs) {
			String[] f = field.split("\\:");
			switch (f[0]) {
			case "REF":
				mo.Mpesaref = f[1];
				break;
			case "MPESADATE":
				String dstr = f[1];
				mo.SentDate = DateExtension.parse(dstr);
				break;
			case "AMOUNT":
				mo.Amount = Double.parseDouble(f[1]);
				break;
			case "FROMTEL":
				mo.CustomerTelno = f[1];
				break;
			case "ACCOUNTNO":
				mo.AccountId = Long.parseLong(f[1]);
				break;
			case "BALANCE":
				mo.Bal = Double.parseDouble(f[1]);
				break;

			}
		}
		
		
		/*
		 * Body =
		 * {"Ref":"FX12UB729","MpesaDate":"31/10/14 7:49 PM","Amount":"200",
		 * "FromName":"FRANCIS MURAYA","FromTel":"254715413144",
		 * "Account":"10","Balance":"420"}
		 * 
		 * /* FX12UB729 Confirmed. on 31/10/14 at 7:49 PM Ksh220.00 received
		 * from FRANCIS MURAYA 254715413144. Account Number 10 New Utility
		 * balance is Ksh420.00
		 */
		// populate generic from abstract
		// mo.MemberId = GetMemberFromPhone(OriginatingAddress);//get member id
		// from telno

		// String[] lines = Body.toUpperCase().trim().split("\\s+");
		// String MpesaRef = lines[0].split("\\s+")[2];
		// String date = lines[1].split("\\s+")[1];
		// String time = lines[1].split("\\s+")[3];
		// String ampm = lines[1].split("\\s+")[lines[1].length() - 1];
		// String money = lines[2].split("\\s+")[0].split("KSH")[0];
		// String telno = lines[2].split("\\s+")[lines[2].length() - 1];
		// String accno = lines[3].split("\\s+")[lines[3].length() - 1];
		// String bal = lines[4].split("\\s+")[lines[4].length() -
		// 1].split("KSH")[0];
		//

//		mo.AccountId = Long.parseLong(accno);
//		mo.Amount = Double.parseDouble(money);
//		mo.Bal = Double.parseDouble(bal);
//		mo.Mpesaref = MpesaRef;
//		String d = date + " " + time + ":00 " + ampm;
//		mo.SentDate = DateExtension.parse(d, "dd/MM/yy h:mm:ss tt");
//		mo.MessageDate = MessageDate;
//		mo.FanikiwaMessageType = FanikiwaMessageType.MpesaDepositMessage;
//		mo.Body = Body;
//		mo.Status = "NEW";

		return mo;
	}

}
