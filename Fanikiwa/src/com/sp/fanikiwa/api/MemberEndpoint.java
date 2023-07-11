package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.google.api.server.spi.config.Api;
import com.google.api.server.spi.config.ApiMethod;
import com.google.api.server.spi.config.ApiNamespace;
import com.google.api.server.spi.config.Nullable;
import com.google.api.server.spi.response.CollectionResponse;
import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.google.appengine.api.datastore.Cursor;
import com.google.appengine.api.datastore.QueryResultIterator;
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.MpesaIPNServlet;
import com.sp.fanikiwa.Enums.AccountLimitStatus;
import com.sp.fanikiwa.Enums.PassFlag;
import com.sp.fanikiwa.Enums.UserType;
import com.sp.fanikiwa.business.WithdrawalComponent;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.AccountDTO;
import com.sp.fanikiwa.entity.AccountType;
import com.sp.fanikiwa.entity.ActivationDTO;
import com.sp.fanikiwa.entity.Coadet;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.MemberDTO;
import com.sp.fanikiwa.entity.UserDTO;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.Organization;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Userprofile;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.MailUtil;
import com.sp.utils.PasswordHash;
import com.sp.utils.SMSUtil;
import com.sp.utils.SessionIdentifierGenerator;
import com.sp.utils.TokenUtil;

import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import javax.inject.Named;

@Api(name = "memberendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class MemberEndpoint {

	private static final int TOKENEXPIRYDURATION = 30; // Half an hour
	private static final Logger log = Logger.getLogger(MemberEndpoint.class
			.getName());

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listMember")
	public CollectionResponse<Member> listMember(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Member> query = ofy().load().type(Member.class);
		return listMemberByQuery(query, cursorString, count);
	}

	private CollectionResponse<Member> listMemberByQuery(Query<Member> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Member> records = new ArrayList<Member>();
		QueryResultIterator<Member> iterator = query.iterator();
		int num = 0;
		while (iterator.hasNext()) {
			records.add(iterator.next());
			if (count != null) {
				num++;
				if (num == count)
					break;
			}
		}

		// Find the next cursor
		if (cursorString != null && cursorString != "") {
			Cursor cursor = iterator.getCursor();
			if (cursor != null) {
				cursorString = cursor.toWebSafeString();
			}
		}
		return CollectionResponse.<Member> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This method gets the entity having primary key id. It uses HTTP GET
	 * method.
	 *
	 * @param id
	 *            the primary key of the java bean.
	 * @return The entity with primary key id.
	 */
	@ApiMethod(name = "getMemberByID")
	public Member getMemberByID(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Member
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	// @ApiMethod(name = "insertMember")

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Member
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateMember")
	public RequestResult updateMember(Member member) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {

			Member existingmember = findRecord(member.getMemberId());
			if (existingmember == null) {
				throw new NotFoundException("Member does not exist");
			}
			existingmember
					.setCurrentAccount(existingmember.getCurrentAccount());
			existingmember.setCustomer(existingmember.getCustomer());
			existingmember.setDateActivated(existingmember.getDateActivated());
			existingmember.setDateJoined(existingmember.getDateJoined());
			existingmember.setDateOfBirth(member.getDateOfBirth());
			existingmember.setEmail(existingmember.getEmail());
			existingmember.setGender(member.getGender());
			existingmember.setInformBy(member.getInformBy());
			existingmember.setInterestExpAccount(existingmember
					.getInterestExpAccount());
			existingmember.setInterestIncAccount(existingmember
					.getInterestIncAccount());
			existingmember.setInvestmentAccount(existingmember
					.getInvestmentAccount());
			existingmember.setLoanAccount(existingmember.getLoanAccount());
			existingmember.setMaxRecordsToDisplay(member
					.getMaxRecordsToDisplay());
			existingmember.setMemberId(existingmember.getMemberId());
			existingmember.setNationalID(member.getNationalID());
			existingmember.setOtherNames(member.getOtherNames());
			existingmember.setPhoto(existingmember.getPhoto());
			existingmember.setRefferedBy(member.getRefferedBy());
			existingmember.setStatus(existingmember.getStatus());
			existingmember.setSurname(member.getSurname());
			existingmember.setTelephone(existingmember.getTelephone());

			ofy().save().entities(existingmember).now();
			re.setSuccess(true);
			re.setResultMessage(MessageFormat
					.format("Details:<br/>Member Id: {0}, <br/>Current Account Id: {1}, <br/>Loan Account Id: {2}, <br/>Investment Account Id: {3}, <br/>Interest Income Account Id: {4}, <br/>Interest Expense Account Id: {5}",
							existingmember.getMemberId().toString(),
							existingmember.getCurrentAccount().getAccountID()
									.toString(),
							existingmember.getLoanAccount().getAccountID()
									.toString(), existingmember
									.getInvestmentAccount().getAccountID()
									.toString(), existingmember
									.getInterestIncAccount().getAccountID()
									.toString(), existingmember
									.getInterestExpAccount().getAccountID()
									.toString()));
			return re;

		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeMember")
	public void removeMember(@Named("id") Long id) throws NotFoundException {
		Member record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Member does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private Member findRecord(Long id) {
		return ofy().load().type(Member.class).id(id).now();
	}

	private Member insertMember(Member member) throws ConflictException {
		if (member.getMemberId() != null) {
			if (findRecord(member.getMemberId()) != null) {
				throw new ConflictException("Member already exists");
			}
		}
		ofy().save().entities(member).now();
		return member;
	}

	// Non CRUD
	@ApiMethod(name = "getMemberByEmailWeb")
	public RequestResult GetMemberByEmailWeb(@Named("email") String email) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Member member = ofy().load().type(Member.class)
					.filter("email", email).first().now();
			if (member == null) {
				re.setSuccess(false);
				re.setResultMessage("Member does not exist");
				return re;
			}
			re.setSuccess(true);
			re.setClientToken(member);
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	@ApiMethod(name = "retrieveMember")
	public RequestResult retrieveMember(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Member member = findRecord(id);
			if (member == null) {
				re.setSuccess(false);
				re.setResultMessage("Member does not exist");
				return re;
			}
			MemberDTO memberDto = createDTOFromMember(member);
			re.setSuccess(false);
			re.setClientToken(memberDto);
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	@ApiMethod(name = "obtainMemberByEmail")
	public RequestResult obtainMemberByEmail(@Named("email") String email) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Member member = ofy().load().type(Member.class)
					.filter("email", email).first().now();
			if (member == null) {
				re.setSuccess(false);
				re.setResultMessage("Member does not exist");
				return re;
			}
			MemberDTO memberDto = createDTOFromMember(member);
			re.setSuccess(false);
			re.setClientToken(memberDto);
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	@ApiMethod(name = "getMemberByEmail")
	public Member GetMemberByEmail(@Named("email") String email) {
		return ofy().load().type(Member.class).filter("email", email).first()
				.now();
	}

	@ApiMethod(name = "txnlessGetMemberByEmail")
	public Member txnlessGetMemberByEmail(@Named("email") String email) {
		return ofy().transactionless().load().type(Member.class)
				.filter("email", email).first().now();
	}

	public RequestResult activate(ActivationDTO activateDTO) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		Member member = txnlessGetMemberByEmail(activateDTO.getEmail());
		if (member == null) {
			re.setSuccess(false);
			re.setResultMessage("Not Successful: Member with email["
					+ activateDTO.getEmail() + "] is null");
		}
		member.setStatus("A");
		member.setDateActivated(activateDTO.getActivatedDate());
		ofy().save().entities(member).now();

		re.setSuccess(true);
		re.setResultMessage("Successful");
		return re;
	}

	@ApiMethod(name = "register")
	public RequestResult Register(UserDTO userDTO) {
		String userType = userDTO.getUserType();

		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		Userprofile userReturned = null;

		// STEP 1: Create the user
		UserprofileEndpoint upep = new UserprofileEndpoint();
		if (!upep.UserExists(userDTO.getEmail()).isSuccess()) {
			Userprofile user = new Userprofile();
			user.setCreateDate(new Date());
			String HashedPwd;

			try {
				HashedPwd = PasswordHash.createHash(userDTO.getPwd());
				user.setPwd(HashedPwd); // think of encrypting
			} catch (NoSuchAlgorithmException | InvalidKeySpecException e1) {
				// TODO Auto-generated catch block
				re.setSuccess(false);
				re.setResultMessage("Error Creating User! " + e1.getMessage());
				return re;
			}

			user.setUserId(userDTO.getEmail());
			user.setTelephone(userDTO.getTelephone());
			user.setUserType(userType);
			user.setStatus(userDTO.getStatus());

			// set activation token
			TokenUtil.SetUserToken(user, user.getCreateDate());

			try {
				userReturned = upep.insertUserprofile(user);
				if (userReturned == null) {
					re.setSuccess(false);
					re.setResultMessage("Error Creating User!");
					return re;
				}
			} catch (NotFoundException | ConflictException e) {
				re.setSuccess(false);
				re.setResultMessage(e.getMessage());
				log.log(Level.SEVERE, e.getMessage(), e);
				return re;
			}
		}
		// Continue only if you are creating a member user
		if (!userType.equals(UserType.Member.name())) {
			re.setSuccess(true);
			re.setResultMessage("Non member registration successfull");
			return re;
		}

		// Create a member user
		try {

			Member emailexists = ofy().load().type(Member.class)
					.filter("email", userDTO.getEmail()).first().now();
			if (emailexists != null) {
				re.setSuccess(false);
				re.setResultMessage("Email  [ " + userDTO.getEmail()
						+ " ] is already Registered in Fanikiwa!");
				return re;
			}
			Member telephoneexists = ofy().load().type(Member.class)
					.filter("telephone", userDTO.getTelephone()).first().now();
			if (telephoneexists != null) {
				re.setSuccess(false);
				re.setResultMessage("Telephone [ " + userDTO.getTelephone()
						+ " ]  is already Registered in Fanikiwa!");
				return re;
			}

			Customer customer = new Customer();
			// at this point, fill the customer with the details from the UI
			customer.setName(userDTO.getSurname());
			customer.setEmail(userDTO.getEmail());
			customer.setTelephone(userDTO.getTelephone());
			customer.setCreatedDate(new Date());
			Long lng = Config.GetLong("CURRENT_ORG");
			Organization org = new Organization(lng);
			customer.setOrganization(org);
			customer.setBillToEmail(userDTO.getEmail());
			customer.setBillToTelephone(userDTO.getTelephone());

			CustomerEndpoint cep = new CustomerEndpoint();
			Customer customerReturned = cep.insertCustomer(customer);
			if (customerReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Customer!");
				return re;
			}

			// Step 3. Create 3 accounts. Currentaccount, loanaccount and
			// invesmentaccount using the customerReturned
			Account currentAccount = new Account();
			currentAccount.setAccountName(customerReturned.getName()
					+ " Curr A/c");
			currentAccount.setCustomer(customerReturned);
			currentAccount.setCoadet(new Coadet(Config
					.GetLong("CURRENT_ACC_COA_ID")));
			currentAccount.setAccounttype(new AccountType(Config
					.GetLong("CURRENT_ACC_TYPE_ID")));
			currentAccount.setBookBalance(0.00);
			currentAccount.setClearedBalance(0.00);
			currentAccount.setLimit(0.00);
			currentAccount.setInterestRate(0.00);
			currentAccount.setAccruedInt(0.00);
			currentAccount
					.setLimitFlag(AccountLimitStatus.PostingOverDrawingProhibited
							.name());
			currentAccount.setPassFlag(PassFlag.Ok.name());

			Account loanaccount = new Account();
			loanaccount
					.setAccountName(customerReturned.getName() + " Loan A/c");
			loanaccount.setCustomer(customerReturned);
			loanaccount
					.setCoadet(new Coadet(Config.GetLong("LOAN_ACC_COA_ID")));
			loanaccount.setAccounttype(new AccountType(Config
					.GetLong("LOAN_ACC_COA_ID")));
			loanaccount.setBookBalance(0.00);
			loanaccount.setClearedBalance(0.00);
			loanaccount.setLimit(0.00);
			loanaccount.setInterestRate(0.00);
			loanaccount.setAccruedInt(0.00);
			loanaccount.setLimitFlag(AccountLimitStatus.PostingNoLimitChecking
					.name());
			loanaccount.setPassFlag(PassFlag.Ok.name());

			Account interestexpenseaccount = new Account();
			interestexpenseaccount.setAccountName(customerReturned.getName()
					+ " Interest Exp A/c");
			interestexpenseaccount.setCustomer(customerReturned);
			interestexpenseaccount.setCoadet(new Coadet(Config
					.GetLong("INTEREST_EXP_ACC_COA_ID")));
			interestexpenseaccount.setAccounttype(new AccountType(Config
					.GetLong("INTEREST_EXP_ACC_TYPE_ID")));
			interestexpenseaccount.setBookBalance(0.00);
			interestexpenseaccount.setClearedBalance(0.00);
			interestexpenseaccount.setLimit(0.00);
			interestexpenseaccount.setInterestRate(0.00);
			interestexpenseaccount.setAccruedInt(0.00);
			interestexpenseaccount
					.setLimitFlag(AccountLimitStatus.PostingNoLimitChecking
							.name());
			interestexpenseaccount.setPassFlag(PassFlag.Ok.name());

			Account invesmentaccount = new Account();
			invesmentaccount.setAccountName(customerReturned.getName()
					+ " Investment A/c");
			invesmentaccount.setCustomer(customerReturned);
			invesmentaccount.setCoadet(new Coadet(Config
					.GetLong("INVESTMENT_ACC_COA_ID")));
			invesmentaccount.setAccounttype(new AccountType(Config
					.GetLong("INVESTMENT_ACC_TYPE_ID")));
			invesmentaccount.setBookBalance(0.00);
			invesmentaccount.setClearedBalance(0.00);
			invesmentaccount.setLimit(0.00);
			invesmentaccount.setInterestRate(0.00);
			invesmentaccount.setAccruedInt(0.00);
			invesmentaccount
					.setLimitFlag(AccountLimitStatus.PostingOverDrawingProhibited
							.name());
			invesmentaccount.setPassFlag(PassFlag.Ok.name());

			Account interestincomeaccount = new Account();
			interestincomeaccount.setAccountName(customerReturned.getName()
					+ " Interest Inc A/c");
			interestincomeaccount.setCustomer(customerReturned);
			interestincomeaccount.setCoadet(new Coadet(Config
					.GetLong("INTEREST_INC_ACC_COA_ID")));
			interestincomeaccount.setAccounttype(new AccountType(Config
					.GetLong("INTEREST_INC_ACC_TYPE_ID")));
			interestincomeaccount.setBookBalance(0.00);
			interestincomeaccount.setClearedBalance(0.00);
			interestincomeaccount.setLimit(0.00);
			interestincomeaccount.setInterestRate(0.00);
			interestincomeaccount.setAccruedInt(0.00);
			interestincomeaccount.setLimitFlag(AccountLimitStatus.Ok.name());
			interestincomeaccount.setPassFlag(PassFlag.Ok.name());

			AccountEndpoint aep = new AccountEndpoint();
			Account currentAccountReturned = aep.insertAccount(currentAccount);
			Account loanAccountReturned = aep.insertAccount(loanaccount);
			Account investmentAccountReturned = aep
					.insertAccount(invesmentaccount);

			Account intexpAccountReturned = aep
					.insertAccount(interestexpenseaccount);
			Account intincAccountReturned = aep
					.insertAccount(interestincomeaccount);

			if (currentAccountReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Current Account!");
				return re;
			}
			if (loanAccountReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Loan Account!");
				return re;
			}
			if (intexpAccountReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Interest Expense Account!");
				return re;
			}
			if (investmentAccountReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Investment Account!");
				return re;
			}
			if (intincAccountReturned == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Interet Income Account!");
				return re;
			}

			// Step 4. create the member with the three accounts created in
			// step1
			Member member = new Member();
			member.setEmail(userDTO.getEmail());
			member.setNationalID(userDTO.getNationalID());
			member.setTelephone(userDTO.getTelephone());
			member.setSurname(userDTO.getSurname());
			member.setDateJoined(new Date());
			member.setStatus(userDTO.getStatus());
			member.setMaxRecordsToDisplay(5);
			member.setCurrentAccount(currentAccountReturned);
			member.setLoanAccount(loanAccountReturned);
			member.setInvestmentAccount(investmentAccountReturned);
			member.setInterestExpAccount(intexpAccountReturned);
			member.setInterestIncAccount(intincAccountReturned);
			member.setCustomer(customerReturned);
			Member newMember = insertMember(member);
			if (newMember == null) {
				re.setSuccess(false);
				re.setResultMessage("Error Creating Member!");
				return re;
			}

			// Step 5. Update the customer with memberid
			customerReturned.setMemberId(newMember.getMemberId());
			cep.updateCustomer(customerReturned);

			// Step 6. Create ROOT Mailing Group for member
			LendingGroupEndpoint mc = new LendingGroupEndpoint();
			mc.CreateRootMailingGroup(newMember);

			re.setSuccess(true);
			re.setResultMessage("Registration successful. See email/sms for details on how to activate your account");
			// re.setResultMessage(MessageFormat
			// .format("Registration Details:Member Id: {0}, Current Account Id: {1}, Loan Account Id: {2}, Investment Account Id: {3}, Interest Income Account Id: {4}, Interest Expense Account Id: {5}",
			// newMember.getMemberId().toString(), newMember
			// .getCurrentAccount().getAccountID()
			// .toString(), newMember.getLoanAccount()
			// .getAccountID().toString(), newMember
			// .getInvestmentAccount().getAccountID()
			// .toString(), newMember
			// .getinterestIncAccount().getAccountID()
			// .toString(), newMember
			// .getinterestExpAccount().getAccountID()
			// .toString()));
			// send activation token
			TokenUtil.SendToken(userDTO.getRegistrationMethod(), userReturned,
					userReturned.getToken());
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
			return re;
		}

	}

	@ApiMethod(name = "deRegister")
	public RequestResult deRegister(@Named("email") String email) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Member member = GetMemberByEmail(email);
			if (member == null) {
				throw new NotFoundException("Member [ " + email
						+ " ] does not exist");
			}
			if (GLUtil.GetAvailableBalance(member.getCurrentAccount()) > 0) {
				re.setSuccess(false);
				re.setResultMessage("Your Account has money.<br/>Available balance is [ "
						+ GLUtil.GetAvailableBalance(member.getCurrentAccount())
						+ " ].<br/>It is advised you withdraw your money first.");
				return re;
			}
			// 1. delete all offers
			// 2. delete offerrecepients
			// 3. reverse loans
			re.setSuccess(true);
			re.setResultMessage("Deregistration successful.");
		} catch (Exception e) {
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	@ApiMethod(name = "getMemberByTelephone")
	public Member GetMemberByTelephone(@Named("telephone") String telephone) {
		return ofy().load().type(Member.class).filter("telephone", telephone)
				.first().now();
	}

	@ApiMethod(name = "getMemberByNationalID", path = "member/nationalid")
	public Member getMemberByNationalID(@Named("nationalId") String nationalId) {
		return ofy().load().type(Member.class).filter("nationalID", nationalId)
				.first().now();
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectMemberAccounts")
	public CollectionResponse<Account> listMemberAccountMobile(Member member,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		return listAccountByQuery(member, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listMemberAccountWeb")
	public CollectionResponse<Account> listMemberAccountWeb(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = GetMemberByEmail(email);
		return listAccountByQuery(member, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectAccountsByMemberID")
	public RequestResult selectAccountsByMemberID(@Named("id") Long id,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Member member = getMemberByID(id);
			if (member == null) {
				throw new NotFoundException("Member does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(listAccountByQuery(member, cursorString, count));
		} catch (Exception e) {
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	private CollectionResponse<Account> listAccountByQuery(Member member,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		List<Account> memberAccounts = new ArrayList<Account>();
		memberAccounts.add(member.getCurrentAccount());
		memberAccounts.add(member.getInterestExpAccount());
		memberAccounts.add(member.getInterestIncAccount());
		memberAccounts.add(member.getInvestmentAccount());
		memberAccounts.add(member.getLoanAccount());

		return CollectionResponse.<Account> builder().setItems(memberAccounts)
				.setNextPageToken(cursorString).build();
	}

	@ApiMethod(name = "isEmailValid")
	public RequestResult isEmailValid(@Named("email") String email) {

		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		try {

			String EMAIL_REGEX = "^[\\w-_\\.+]*[\\w-_\\.]\\@([\\w]+\\.)+[\\w]+[\\w]$";
			Boolean isemailvalid = email.matches(EMAIL_REGEX);
			if (isemailvalid) {
				re.setSuccess(isemailvalid);
				re.setResultMessage("email validation successful...");
				return re;
			} else {
				re.setSuccess(isemailvalid);
				re.setResultMessage("Validation failed! Please check Email.<br/>Valid format is user@domain.com");
				return re;
			}

		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			log.log(Level.SEVERE, e.getMessage(), e);
		}
		return re;
	}

	private MemberDTO createDTOFromMember(Member member) throws Exception {
		// Construct dto
		MemberDTO memberDto = new MemberDTO();
		memberDto.setMemberId(member.getMemberId());
		memberDto.setDateActivated(member.getDateActivated());
		memberDto.setDateJoined(member.getDateJoined());
		memberDto.setDateOfBirth(member.getDateOfBirth());
		memberDto.setEmail(member.getEmail());
		memberDto.setGender(member.getGender());
		memberDto.setInformBy(member.getInformBy());
		memberDto.setMaxRecordsToDisplay(member.getMaxRecordsToDisplay());
		memberDto.setNationalID(member.getNationalID());
		memberDto.setOtherNames(member.getOtherNames());
		memberDto.setPhoto(member.getPhoto());
		memberDto.setRefferedBy(member.getRefferedBy());
		memberDto.setStatus(member.getStatus());
		memberDto.setSurname(member.getSurname());
		memberDto.setTelephone(member.getTelephone());
		memberDto.setInvestmentAccount(member.getInvestmentAccount()
				.getAccountID());
		memberDto.setCurrentAccount(member.getCurrentAccount().getAccountID());
		memberDto.setInterestIncAccount(member.getInterestIncAccount()
				.getAccountID());
		memberDto.setInterestExpAccount(member.getInterestExpAccount()
				.getAccountID());
		memberDto.setLoanAccount(member.getLoanAccount().getAccountID());
		memberDto.setCustomer(member.getCustomer().getCustomerId());

		return memberDto;
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectDtoMembers")
	public CollectionResponse<MemberDTO> selectDtoMembers(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {

		Query<Member> query = ofy().load().type(Member.class);
		return selectDtoMemberByQuery(query, cursorString, count);
	}

	private CollectionResponse<MemberDTO> selectDtoMemberByQuery(
			Query<Member> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<MemberDTO> records = new ArrayList<MemberDTO>();
		QueryResultIterator<Member> iterator = query.iterator();
		int num = 0;
		while (iterator.hasNext()) {
			MemberDTO dto = createDTOFromMember(iterator.next());
			records.add(dto);
			if (count != null) {
				num++;
				if (num == count)
					break;
			}
		}

		// Find the next cursor
		if (cursorString != null && cursorString != "") {
			Cursor cursor = iterator.getCursor();
			if (cursor != null) {
				cursorString = cursor.toWebSafeString();
			}
		}
		return CollectionResponse.<MemberDTO> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	private Member createMemberFromDTO(MemberDTO memberDTO) throws Exception {

		Member member = new Member();
		member.setMemberId(memberDTO.getMemberId());
		member.setDateActivated(memberDTO.getDateActivated());
		member.setDateJoined(memberDTO.getDateJoined());
		member.setDateOfBirth(memberDTO.getDateOfBirth());
		member.setEmail(memberDTO.getEmail());
		member.setGender(memberDTO.getGender());
		member.setInformBy(memberDTO.getInformBy());
		member.setMaxRecordsToDisplay(memberDTO.getMaxRecordsToDisplay());
		member.setNationalID(memberDTO.getNationalID());
		member.setOtherNames(memberDTO.getOtherNames());
		member.setPhoto(memberDTO.getPhoto());
		member.setRefferedBy(memberDTO.getRefferedBy());
		member.setStatus(memberDTO.getStatus());
		member.setSurname(memberDTO.getSurname());
		member.setTelephone(memberDTO.getTelephone());
		member.setInvestmentAccount(GLUtil.GetAccount(memberDTO
				.getInvestmentAccount()));
		member.setLoanAccount(GLUtil.GetAccount(memberDTO.getLoanAccount()));
		member.setCurrentAccount(GLUtil.GetAccount(memberDTO
				.getCurrentAccount()));
		member.setInterestExpAccount(GLUtil.GetAccount(memberDTO
				.getInterestExpAccount()));
		member.setInterestIncAccount(GLUtil.GetAccount(memberDTO
				.getInterestIncAccount()));
		member.setCustomer(GLUtil.GetCustomer(memberDTO.getCustomer()));

		return member;
	}

}