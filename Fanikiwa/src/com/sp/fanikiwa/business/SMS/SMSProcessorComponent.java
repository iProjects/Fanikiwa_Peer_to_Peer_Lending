package com.sp.fanikiwa.business.SMS;

import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collection;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.google.api.server.spi.response.CollectionResponse;
import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.ForbiddenException;
import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.Enums.OfferStatus;
import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.api.OfferEndpoint;
import com.sp.fanikiwa.business.AcceptOfferComponent;

import com.sp.fanikiwa.business.RegistrationComponent;
import com.sp.fanikiwa.business.WithdrawalComponent;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.UserDTO;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferDTO;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.StatementModel;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.MailUtil;
import com.sp.utils.PasswordValidator;
import com.sp.utils.StringExtension;

public class SMSProcessorComponent {

	Map<String, FCommand> Commands = new HashMap<String, FCommand>();

	public SMSProcessorComponent() {

		InitializeCommands();
	}

	public String ProcessFanikiwaMessage(FanikiwaMessage message) {
		String info = "Unknown";
		// Get Processed
		info = GetProcessedMessage(message);

		return info;
	}

	private void InitializeCommands() {

		Commands.put(
				"R",
				new FCommand(
						"Register",
						"Register command enables registration in Fanikiwa.\nSyntax = RegSymbol*Pwd*Email*NationalID\nWhere RegSymbol = R|REG|REGISTER, Email = your Email, NationalID = your National ID, Pwd = your password"));
		Commands.put(
				"B",
				new FCommand(
						"Balance",
						"Balance enquiry on various accounts\nSyntax = BALSymbol*Pwd*<AccountID|AccountLabel>\nWhere BALSymbol = B|BAL|BALANCE \nAccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
		Commands.put(
				"S",
				new FCommand(
						"Statement",
						"Statement enquiry on various accounts\nSyntax = STATSymbol*Pwd*<AccountID|AccountLabel>*StartDate*EndDate\nWhere STATSymbol = S|STAT|STATEMENT \nAccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
		Commands.put(
				"LR",
				new FCommand(
						"Loan Repayment",
						"Early Loan Repaymenton on various loans\nSyntax = LOANREPAYSymbol*Pwd*LoanID*Amount\nWhere LOANREPAYSymbol = LR|PAY \nLoanID = Id sent to you after Accepting an Offer\nPwd = your password"));
		Commands.put(
				"M",
				new FCommand(
						"Mini Statement",
						"Last 5 transactions\nSyntax = MS*Pwd*<AccountID|AccountLabel>\nWhere AccountID = Id of either Current, Investment or Loans account sent to you after registration\nAccountLabel=[C|I|L] for Current or Investment or Loan account respectively\nPwd = your password"));
		Commands.put(
				"MLO",
				new FCommand(
						"Make Lend Offer",
						"Make a Lend Offer\nSyntax = MLO*Pwd*Amount*InterestRate*Term\nWhere Amount = your Amount to Lend, InterestRate = your Interest Rate, Term = Number of Months to Repay the Loan, Pwd = your password"));
		Commands.put(
				"MBO",
				new FCommand(
						"Make Borrow Offer",
						"Make a Borrow Offer\nSyntax = MBO*Pwd*Amount*InterestRate*Term\nWhere Amount = your Amount to Borrow, InterestRate = your Interest Rate, Term = Number of Months to Repay the Loan, Pwd = your password"));
		Commands.put(
				"ALO",
				new FCommand(
						"Accept Lend Offer",
						"Accept a Lend Offer and get a Loan\nSyntax = ALO*Pwd*OfferId\nWhere OfferId = your Lend Offer Id, Pwd = your password"));
		Commands.put(
				"ABO",
				new FCommand(
						"Accept Borrow Offer",
						"Accept a Borrow Offer and Make an Investment\nSyntax = ABO*Pwd*OfferId\nWhere OfferId = your Borrow Offer Id, Pwd = your password"));
		Commands.put(
				"LO",
				new FCommand("Lend Offers",
						"Get your Lend Offers\nSyntax = LO*Pwd\nWhere Pwd = your password"));
		Commands.put(
				"BO",
				new FCommand("Borrow Offers",
						"Get your Borrow Offers\nSyntax = BO*Pwd\nWhere Pwd = your password"));
		Commands.put(
				"FA",
				new FCommand("Fanikiwa Accounts",
						"Get your Fanikiwa Accounts \nSyntax = FA*Pwd\nWhere Pwd = your password"));
		Commands.put(
				"C",
				new FCommand(
						"Change Password",
						"Change Your Password\nSyntax = ChangePasswordSymbol*OldPwd*NewPwd*ConfirmPwd\nWhere ChangePasswordSymbol = C|CP\nOldPwd = your Old password, NewPwd = your New password, ConfirmPwd = your Confirm password"));
		Commands.put(
				"W",
				new FCommand(
						"Withdraw",
						"Withdraw Cash from your Current Account\nSyntax = WithdrawSymbol*Pwd*Amount\nWhere WithdrawSymbol = W|WITHDRAW \nPwd = your Amount to withdraw, Pwd = your password"));
		Commands.put(
				"D",
				new FCommand(
						"DeRegister",
						"DeRegister\nSyntax = DeRegisterSymbol*Pwd*Email\nWhere DeRegisterSymbol = D|DE|DREG|DEREGISTER\nEmail = your Email during registration, Pwd = your password  during registration"));
		Commands.put(
				"G",
				new FCommand(
						"Mailing Group",
						"Create a mailing group\nSyntax = MGSymbol*Pwd*GroupName*[Members]\nWhere MGSymbol = G|GRP|MG\nGroupName = group name. must be unique in the system, Pwd = your password during registration\nMembers=comma separated telephone|email|groupname"));
		Commands.put(
				"AG",
				new FCommand(
						"Add Member",
						"Add member to Mailing Group\nSyntax = MGSymbol*Pwd*GroupName*[Members]\nWhere MGSymbol = AG|AGRP|AMG\nGroupName = group name. must be unique in the system, Pwd = your password during registration\nMembers=comma separated telephone|email|groupname"));

		List<String> cmds = new ArrayList<String>();
		cmds = Arrays.asList(Commands.keySet().toArray(new String[0]));
		// cmds = (from c in Commands
		// select c.Key).ToList();

		Commands.put("H", new FCommand("Help",
				"H|Help - Help command\nUsage: H*<Command>"
						+ "\nCommands are [" + StringExtension.join(cmds,",")
						+ "]\nE.g. Send H*R for help on registration "));
	}

	private String GetProcessedMessage(FanikiwaMessage message) {
		String msg = "Send Help";
		try {
			if (message instanceof HelpMessage)
				return ProcessHelpMessage((HelpMessage) message);
			if (message instanceof BalanceEnquiryMessage)
				return ProcessBalanceEnquiryMessage((BalanceEnquiryMessage) message);
			if (message instanceof StatementEnquiryMessage)
				return ProcessStatementEnquiryMessage((StatementEnquiryMessage) message);
			if (message instanceof MiniStatementEnquiryMessage)
				return ProcessMiniStatementEnquiryMessage((MiniStatementEnquiryMessage) message);

			if (message instanceof MpesaDepositMessage)
				return ProcessMpesaDepositMessage((MpesaDepositMessage) message);
			if (message instanceof RegisterMessage)
				return ProcessRegisterMessage((RegisterMessage) message);
			if (message instanceof DeRegisterMessage)
				return ProcessDeRegisterMessage((DeRegisterMessage) message);
			if (message instanceof MakeLendOfferMessage)
				return ProcessMakeLendOfferMessage((MakeLendOfferMessage) message);
			if (message instanceof MakeBorrowOfferMessage)
				return ProcessMakeBorrowOfferMessage((MakeBorrowOfferMessage) message);
			if (message instanceof AcceptLendOfferMessage)
				return ProcessAcceptLendOfferMessage((AcceptLendOfferMessage) message);
			if (message instanceof AcceptBorrowOfferMessage)
				return ProcessAcceptBorrowOfferMessage((AcceptBorrowOfferMessage) message);
			if (message instanceof LendOffersMessage)
				return ProcessListLendOffersMessage((LendOffersMessage) message);
			if (message instanceof BorrowOffersMessage)
				return ProcessListBorrowOffersMessage((BorrowOffersMessage) message);
			if (message instanceof FanikiwaAccountsMessage)
				return ProcessFanikiwaAccountsMessage((FanikiwaAccountsMessage) message);
			if (message instanceof ChangePinMessage)
				return ProcessChangePinMessage((ChangePinMessage) message);
			if (message instanceof WithdrawMessage)
				return ProcessWithdrawMessage((WithdrawMessage) message);

			if (message instanceof ErrorMessage)
				return ProcessErrorMessage((ErrorMessage) message);
		} catch (Exception e) {
			msg = e.getMessage();

		}

		return msg;
	}

	private Long GetAccountIDFromLabel(Member member, String Acclabel) {
		String label = Acclabel.toUpperCase();
		if ("C".equals(label) || "CUR".equals(label)) {
			return member.getCurrentAccount().getAccountID();
		}

		if ("I".equals(label) || "INV".equals(label)) {
			return member.getInvestmentAccount().getAccountID();
		}

		if ("L".equals(label) || "LOAN".equals(label)) {
			return member.getLoanAccount().getAccountID();
		}

		return member.getCurrentAccount().getAccountID();

	}

	private Long GetAccountIDFromMessage(FanikiwaMessage message) {
		Long AccId = (long) 0;
		MemberEndpoint rc = new MemberEndpoint();
		if (!StringExtension.isNullOrEmpty(message.SenderTelno)) {
			Member member = rc.GetMemberByTelephone(message.SenderTelno);
			if (member == null) {
				throw new NullPointerException(MessageFormat.format(
						"Sender Telno [{0}] is not registered. ",
						message.SenderTelno));
			}

			if (message instanceof BalanceEnquiryMessage) {
				BalanceEnquiryMessage msg = ((BalanceEnquiryMessage) message);
				if (!StringExtension.isNullOrEmpty(msg.AccountLabel))
					return GetAccountIDFromLabel(member, msg.AccountLabel);

				AccId = msg.AccountId;
			}
			if (message instanceof MiniStatementEnquiryMessage) {
				MiniStatementEnquiryMessage msg = ((MiniStatementEnquiryMessage) message);
				if (!StringExtension.isNullOrEmpty(msg.AccountLabel))
					return GetAccountIDFromLabel(member, msg.AccountLabel);

				AccId = msg.AccountId;
			}
			if (message instanceof StatementEnquiryMessage) {
				StatementEnquiryMessage msg = ((StatementEnquiryMessage) message);
				if (!StringExtension.isNullOrEmpty(msg.AccountLabel))
					return GetAccountIDFromLabel(member, msg.AccountLabel);

				AccId = msg.AccountId;
			}
		}
		return AccId;
	}

	private String ProcessMpesaDepositMessage(MpesaDepositMessage message)
			throws Exception {
		String narr = message.CustomerTelno + " deposited on "
				+ message.MessageDate.toString();
		List<Transaction> txns = TransactionFactory.MpesaDeposit(
				message.AccountId, message.Amount, narr, message.Mpesaref);
		TransactionPost.Post(txns);
		return "Deposit Successful";
	}

	private String ProcessHelpMessage(HelpMessage message) {
		String hlp = "";
		if (!StringExtension.isNullOrEmpty(message.HelpCommand)) {
			hlp = ProcessHelpMessageCommand(message.HelpCommand);
			return hlp;
		} else {
			hlp = ProcessHelpMessageCommand("ALL");
			return hlp;
		}
	}

	String GetHelpMessage(String Key) {
		FCommand cmd = Commands.get(Key);
		return cmd.Usage;
	}

	private String ProcessHelpMessageCommand(String cmd) {
		String hlp = GetHelpMessage("H");

		switch (cmd.toUpperCase()) {
		case "H":
		case "HELP":
			hlp = GetHelpMessage("H");
			break;
		case "B":
		case "BAL":
		case "BALANCE":
			hlp = GetHelpMessage("B");
			break;
		case "S":
		case "STAT":
		case "STATEMENT":
			hlp = GetHelpMessage("S");
			break;
		case "M":
		case "MS":
		case "MINI":
			hlp = GetHelpMessage("M");
			break;
		case "LR":
		case "PAY":
			hlp = GetHelpMessage("LR");
			break;
		case "R":
		case "RE":
		case "REG":
		case "REGISTER":
			hlp = GetHelpMessage("R");
			break;
		case "D":
		case "DE":
		case "DEREG":
		case "DEREGISTER":
			hlp = GetHelpMessage("D");
			break;
		case "MLO":
			hlp = GetHelpMessage("MLO");
			break;
		case "MBO":
			hlp = GetHelpMessage("MBO");
			break;
		case "ALO":
			hlp = GetHelpMessage("ALO");
			break;
		case "ABO":
			hlp = GetHelpMessage("ABO");
			break;
		case "LO":
			hlp = GetHelpMessage("LO");
			break;
		case "BO":
			hlp = GetHelpMessage("BO");
			break;
		case "FA":
			hlp = GetHelpMessage("FA");
			break;
		case "C":
		case "CP":
			hlp = GetHelpMessage("C");
			break;
		case "W":
		case "WITHDRAW":
			hlp = GetHelpMessage("W");
			break;
		case "ALL":
			return hlp;
		default:
			return hlp;
		}
		return hlp;
	}

	private String ProcessErrorMessage(ErrorMessage message) {
		String error = "An error occurred while processing your request. Please contact Administrator on "
				+ Config.GetString("FANIKIWAADMIN");
		if (message.Exception instanceof NullPointerException) {
			error = message.Exception.getMessage() + "\nPlease send Help to "
					+ Config.GetString("FANIKIWAMESSAGESTELNO");
		}

		return error;
	}

	private String ProcessBalanceEnquiryMessage(BalanceEnquiryMessage message) {

		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not Authenticated or authorized. please check your credentials and status";

		Long AccId = GetAccountIDFromMessage(message);
		if (!GLUtil.AccountExists(AccId))
			throw new NullPointerException("Account [" + AccId
					+ "] does not exist");
		Account account = GLUtil.GetAccount(AccId);

		return MessageFormat
				.format("Balance for [{0}]: {1}\nBook balance = {2}\nAvailable balance = {3}",

				account.getAccountID().toString(), account.getAccountName(),
						account.getBookBalance(), account.getClearedBalance()
								- account.getLimit());
	}

	private String ProcessStatementEnquiryMessage(
			StatementEnquiryMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		Long AccId = GetAccountIDFromMessage(message);
		if (!GLUtil.AccountExists(AccId))
			throw new NullPointerException("Account [" + AccId
					+ "] does not exist");

		return GetStatement(AccId, message.StartDate, message.EndDate);
	}

	private String ProcessMiniStatementEnquiryMessage(
			MiniStatementEnquiryMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		Long AccId = GetAccountIDFromMessage(message);
		if (!GLUtil.AccountExists(AccId))
			throw new NullPointerException("Account [" + AccId
					+ "] does not exist");

		return GetMiniStatement(AccId, 5);
	}

	private String GetMiniStatement(Long AccId, int count) // for statement
	{
		AccountEndpoint aep = new AccountEndpoint();
		Collection<StatementModel> txns = aep.GetMiniStatement(AccId, null,
				count).getItems();
		return ConvertStatementToString(txns);
	}

	private String GetStatement(Long AccId, Date sdate, Date enddate) // for
																		// statement
	{
		AccountEndpoint aep = new AccountEndpoint();
		Collection<StatementModel> txns = aep.GetStatement(AccId, sdate,
				enddate, null, null).getItems();
		return ConvertStatementToString(txns);
	}

	private String ConvertStatementToString(Collection<StatementModel> txns) {
		if (txns.size() == 0)
			return "No transactions found";

		String statement = "";
		for (StatementModel sm : txns) {
			statement += sm.getPostDate().toString() + " " + sm.getAmount()
					+ (sm.getAmount() > 0 ? "DR" : "CR") + "\n";

		}
		return statement;
	}

	private String ProcessEarlyLoanRepaymentMessage(
			EarlyLoanRepaymentMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		Long AccId = (long) 0;
		Member member;
		double bal;
		String Accdes = "";

		if (message.OfferId != 0
				&& !StringExtension.isNullOrEmpty(message.SenderTelno)) {
			MemberEndpoint rc = new MemberEndpoint();
			member = rc.GetMemberByTelephone(message.SenderTelno);
			if (member == null) {
				throw new NullPointerException(MessageFormat.format(
						"Sender Telno [{0}] is not registered. ",
						message.SenderTelno));
			}
		}
		bal = GLUtil.GetAvailableBalance(GLUtil.GetAccount(AccId));
		return MessageFormat.format("Your {0} balance is {1}", Accdes, bal);
	}

	private UserDTO SMSToMember(RegisterMessage message) {
		UserDTO userDTO = new UserDTO();
		userDTO.setEmail(message.Email.toLowerCase());
		userDTO.setPwd(message.Pwd);
		userDTO.setNationalID(message.NationalID);
		userDTO.setStatus("New");//A - Active; NA- Not Active
		userDTO.setDateActivated(new Date());
		userDTO.setDateJoined(new Date());
		userDTO.setInformBy("SMS");
		userDTO.setTelephone(message.SenderTelno);
		userDTO.setPwd(message.Pwd);
		userDTO.setUserType("Member");
		userDTO.setRegistrationMethod("SMS");

		// member.DateOfBirth = DateTime.MinValue;

		String delimiters = "\\@"; // | delimeted
		String[] emailParams = userDTO.getEmail().split(delimiters);
		String surname = emailParams[0];
		userDTO.setSurname(surname);
		return userDTO;
	}

	private String ProcessRegisterMessage(RegisterMessage message)
			throws ConflictException, NotFoundException {
		// check command parameters R*Email*NationalID*pwd
		if (StringExtension.isNullOrEmpty(message.Email))
			throw new NullPointerException("Email is required for registration");
		if (StringExtension.isNullOrEmpty(message.NationalID))
			throw new NullPointerException(
					"NationalID is required for registration");
		if (StringExtension.isNullOrEmpty(message.Pwd))
			throw new NullPointerException(
					"Password is required for registration");

		// check email
		if (!MailUtil.isValidEmailAddress(message.Email)) {
			throw new NullPointerException(
					MessageFormat
							.format("The Email [{0}]  provided is invalid. Please enter a valid Email. ",
									message.Email));
		}

		// check pwd
		if (message.Pwd.length() < 3) {
			throw new NullPointerException(
					MessageFormat
							.format("The Password provided is invalid. Must be longer than 3 characters",
									message.Email));
		}
		// if (!PasswordValidator.validate(message.Pwd))
		// {
		// throw new
		// NullPointerException(MessageFormat.format("The Password provided is invalid. must contains one digit from 0-9 "+
		// "must contains one lowercase characters "+
		// "must contains one uppercase characters "+
		// "must contains one special symbols in the list '@$%' "+
		// "match anything with previous condition checking "+
		// "length at least 6 characters and maximum of 20	 ", message.Pwd));
		// }

		// register now
		RegistrationComponent rc = new RegistrationComponent();
		if (!rc.IsRegistered(message.Email, message.SenderTelno,
				message.NationalID)
				&& !rc.IsEmailRegistered(message.Email)
				&& !rc.IsPhoneRegistered(message.SenderTelno)
				&& !rc.IsNationalIDRegistered(message.NationalID)) {

			RequestResult re = rc.Register(SMSToMember(message));
			if (re.isSuccess() != false) {
				return re.getResultMessage();
			} else
				return "Member registration was not successful";
		} else {
			return "Member is already registered. Telno|Email|NationalID already registered";
		}
	}

	private String ProcessDeRegisterMessage(DeRegisterMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		String msg = "";

		if (!StringExtension.isNullOrEmpty(message.Pwd)
				&& !StringExtension.isNullOrEmpty(message.SenderTelno)) {
			RegistrationComponent rc = new RegistrationComponent();

			Member member = rc.SelectMemberByPhone(message.SenderTelno);
			if (member == null) {
				throw new NullPointerException(MessageFormat.format(
						"Sender Telno [{0}] is not registered. ",
						message.SenderTelno));
			}

		}
		return msg;
	}

	private String ProcessMakeLendOfferMessage(MakeLendOfferMessage message)
			throws NotFoundException, ConflictException, ForbiddenException {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		OfferDTO offer = new OfferDTO();
		offer.setAmount(message.Amount);
		offer.setPrivateOffer(false);
		offer.setTerm(message.Term);
		offer.setInterest(message.InterestRate);
		offer.setOfferType("L");
		offer.setEmail(member.getEmail());
		offer.setStatus(OfferStatus.Open.toString());
		offer.setCreatedDate(new Date());
		offer.setExpiryDate(DateExtension.addMonths(offer.getCreatedDate(),
				Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS")));

		try {
			OfferEndpoint mo = new OfferEndpoint();
			mo.saveOffer(offer);
		} catch (Exception e) {
			return e.getMessage();
		}
		return "Offer successfully made";
	}

	private String ProcessMakeBorrowOfferMessage(MakeBorrowOfferMessage message)
			throws NotFoundException, ConflictException, ForbiddenException {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		String msg = "";
		RegistrationComponent rc = new RegistrationComponent();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		OfferDTO offer = new OfferDTO();
		offer.setAmount(message.Amount);
		offer.setPrivateOffer(false);
		offer.setTerm(message.Term);
		offer.setInterest(message.InterestRate);
		offer.setOfferType("B");
		offer.setEmail(member.getEmail());
		offer.setStatus(OfferStatus.Open.toString());
		offer.setCreatedDate(new Date());
		offer.setExpiryDate(DateExtension.addMonths(offer.getCreatedDate(),
				Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS")));

		try {
			OfferEndpoint mo = new OfferEndpoint();
			mo.saveOffer(offer);;
		} catch (Exception e) {
			return e.getMessage();
		}
		return "Borrow offer successfully done";
	}

	private String ProcessAcceptLendOfferMessage(AcceptLendOfferMessage message)
			throws Exception {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();
		AcceptOfferComponent ac = new AcceptOfferComponent();

		// this is a lend offer so the offerer is the lender and the acceptor
		// the borrower
		Member borrower = rc.GetMemberByPhone(message.SenderTelno);
		if (borrower == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		OfferEndpoint oep = new OfferEndpoint();
		Offer aLendOffer = oep.getOfferByID(message.OfferId);
		if (aLendOffer == null) {
			throw new NullPointerException(
					MessageFormat.format(
							"Offer [{0}] does not exist in Fanikiwa. ",
							message.OfferId));
		}

		ac.AcceptLendOffer(borrower, aLendOffer);

		return ("Successful");
	}

	private String ProcessAcceptBorrowOfferMessage(
			AcceptBorrowOfferMessage message) throws Exception {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();
		AcceptOfferComponent ac = new AcceptOfferComponent();

		// this is a borrow offer so the lender is the acceptor
		Member lender = rc.SelectMemberByPhone(message.SenderTelno);
		if (lender == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		OfferEndpoint oep = new OfferEndpoint();
		Offer aBorrowOffer = oep.getOfferByID(message.OfferId);
		if (aBorrowOffer == null) {
			throw new NullPointerException(
					MessageFormat.format(
							"Offer [{0}] does not exist in Fanikiwa. ",
							message.OfferId));
		}

		ac.AcceptBorrowOffer(lender, aBorrowOffer);

		return ("Successful");
	}

	private String ProcessListLendOffersMessage(LendOffersMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();
		OfferEndpoint oep = new OfferEndpoint();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		Collection<Offer> offers = oep.ListLendOffers(member.getMemberId(),
				null, 5).getItems();

		if (offers.size() > 0) {
			String msg = "";
			for (Offer c : offers) {
				msg += c.getId().toString() + " Amt=" + c.getAmount()
						+ " Term=" + c.getTerm() + " Interest="
						+ c.getInterest();
			}
			return msg;
		} else
			return "No offers found";
	}

	private String ProcessListBorrowOffersMessage(BorrowOffersMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();
		OfferEndpoint oep = new OfferEndpoint();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		Collection<Offer> offers = oep.ListBorrowOffers(member.getMemberId(),
				null, 5).getItems();

		if (offers.size() > 0) {
			String msg = "";
			for (Offer c : offers) {
				msg += c.getId().toString() + " Amt=" + c.getAmount()
						+ " Term=" + c.getTerm() + " Interest="
						+ c.getInterest();
			}
			return msg;
		} else
			return "No offers found";

	}

	private String ProcessFanikiwaAccountsMessage(
			FanikiwaAccountsMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not authenticated";

		RegistrationComponent rc = new RegistrationComponent();
		MemberEndpoint mep = new MemberEndpoint();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}

		Collection<Account> accounts = mep.listMemberAccountMobile(member,
				null, 5).getItems();

		if (accounts.size() > 0) {
			String msg = "";
			for (Account c : accounts) {
				msg += c.getAccountName() + " Id="
						+ c.getAccountID().toString() + " BookBalance="
						+ c.getBookBalance() + " ClearedBalance="
						+ c.getClearedBalance() + " Limit=" + c.getLimit();
			}
			return msg;
		} else
			return "No Accounts found";

	}

	private String ProcessChangePinMessage(ChangePinMessage message) {
		if (!this.AuthenticateAndAuthorize(message.SenderTelno,
				message.OldPassword))
			return "Not authenticated";

		String msg = "";
		if (!StringExtension.isNullOrEmpty(message.OldPassword)
				&& !StringExtension.isNullOrEmpty(message.SenderTelno)) {
			RegistrationComponent rc = new RegistrationComponent();

			if (message.NewPassword != message.ConfirmPassword) {
				throw new NullPointerException(MessageFormat.format(
						"NewPassword does not match ConfirmPassword ",
						message.SenderTelno));
			}

			Member member = rc.SelectMemberByPhone(message.SenderTelno);
			if (member == null) {
				throw new NullPointerException(MessageFormat.format(
						"Sender Telno [{0}] is not registered. ",
						message.SenderTelno));
			}

		}
		return msg;
	}

	private String ProcessWithdrawMessage(WithdrawMessage message) {
		String msg = "Successful";
		if (!this.AuthenticateAndAuthorize(message.SenderTelno, message.Pwd))
			return "Not Authenticated or authorized. please check your credentials and status";

		RegistrationComponent rc = new RegistrationComponent();

		Member member = rc.SelectMemberByPhone(message.SenderTelno);
		if (member == null) {
			throw new NullPointerException(MessageFormat.format(
					"Sender Telno [{0}] is not registered. ",
					message.SenderTelno));
		}
		try {
			/*
			 * 1. convert WithdrawMessage to WithdrawalMessage
			 * 2. save WithdrawalMessage
			 * 3. get the saved message and pass it to WithdrawalComponent.Withdraw
			 * */

		} catch (Exception we) {
			msg = we.getMessage();
		}

		return msg;
	}

	private boolean AuthenticateAndAuthorize(String tel, String pwd) {
		if (StringExtension.isNullOrEmpty(tel))
			throw new NullPointerException("Telno required");

		if (StringExtension.isNullOrEmpty(pwd))
			throw new NullPointerException("Password required");

		RegistrationComponent rc = new RegistrationComponent();
		return rc.Authenticated(tel, pwd) && rc.Authorized(tel, pwd);

	}

	class FCommand {
		public String Name;
		public String Usage;

		public FCommand(String name, String usage) {
			Name = name;
			Usage = usage;
		}
	}

}