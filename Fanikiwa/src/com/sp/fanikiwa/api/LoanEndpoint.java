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
import com.sp.fanikiwa.business.LoanComponent;
import com.sp.fanikiwa.entity.Installment;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.LoanDTO;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.utils.DateExtension;
import com.sp.utils.PeerLendingUtil;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.inject.Named;

@Api(name = "loanendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class LoanEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listLoan")
	public CollectionResponse<Loan> listLoan(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Loan> query = ofy().load().type(Loan.class);
		return listLoanByQuery(query, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectMyLoans")
	public CollectionResponse<Loan> selectMyLoans(@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);

		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("borrowerId", member.getMemberId());
		return listLoanByQuery(query, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectMyInvestments")
	public CollectionResponse<Loan> selectMyInvestments(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);

		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("lenderId", member.getMemberId());
		return listLoanByQuery(query, cursorString, count);
	}

	private CollectionResponse<Loan> listLoanByQuery(Query<Loan> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Loan> records = new ArrayList<Loan>();
		QueryResultIterator<Loan> iterator = query.iterator();
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
		return CollectionResponse.<Loan> builder().setItems(records)
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
	@ApiMethod(name = "getLoanByID")
	public Loan getLoanByID(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "prepayLoan")
	public RequestResult prepayLoan(@Named("id") Long id,
			@Named("amount") Double Amount) throws Exception {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not successful");

		Loan loan = findRecord(id);
		if (loan == null) {
			re.setResultMessage("Loan is null");
			re.setSuccess(true);
			return re;
		}

		LoanComponent lc = new LoanComponent();
		return lc.PrepayLoan(loan, Amount, new Date());
	}

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Loan
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateLoan")
	public Loan updateLoan(Loan Loan) throws NotFoundException {
		Loan record = findRecord(Loan.getId());
		if (record == null) {
			throw new NotFoundException("Loan does not exist");
		}
		ofy().save().entities(Loan).now();
		return Loan;
	}

	@ApiMethod(name = "zerolizeAccruedIntLoan")
	public Loan zerolizeAccruedIntLoan(Loan loan) throws NotFoundException {

		loan.setAccruedInterest(0);
		Loan updatedloan = updateLoan(loan);
		return updatedloan;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeLoan")
	public void removeLoan(@Named("id") Long id) throws NotFoundException {
		Loan record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Loan [ " + id + " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private Loan findRecord(Long id) {
		return ofy().load().type(Loan.class).id(id).now();
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Loan
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertLoan")
	public Loan insertLoan(Loan loan) throws NotFoundException,
			ConflictException {
		if (loan.getId() != null) {
			if (findRecord(loan.getId()) != null) {
				throw new ConflictException("Loan already exists");
			}
		}
		ofy().save().entities(loan).now();
		return loan;
	}

	@ApiMethod(name = "NextIntAccrualLoanByDate")
	public CollectionResponse<Loan> NextIntAccrualLoanByDate(
			@Named("date") Date date) {
		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("nextIntAccrualDate <=", date);
		return listLoanByQuery(query, null, null);
	}

	@ApiMethod(name = "NextIntAppLoanByDate")
	public CollectionResponse<Loan> NextIntAppLoanByDate(
			@Named("date") Date date) {
		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("nextIntAppDate <=", date);
		// .filter("nextIntAppDate >=",Loan.getLastIntAppDate())
		// .filter("nextIntAppDate <=",date)
		return listLoanByQuery(query, null, null);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectDtoLoans")
	public CollectionResponse<LoanDTO> selectDtoLoans(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {

		Query<Loan> query = ofy().load().type(Loan.class);
		return selectDtoLoanByQuery(query, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectDtoMyLoans")
	public CollectionResponse<LoanDTO> selectDtoMyLoans(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {

		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);

		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("borrowerId", member.getMemberId());
		return selectDtoLoanByQuery(query, cursorString, count);
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectDtoMyInvestments")
	public CollectionResponse<LoanDTO> selectDtoMyInvestments(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {

		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);

		Query<Loan> query = ofy().load().type(Loan.class)
				.filter("lenderId", member.getMemberId());
		return selectDtoLoanByQuery(query, cursorString, count);
	}

	private CollectionResponse<LoanDTO> selectDtoLoanByQuery(Query<Loan> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) throws Exception {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<LoanDTO> records = new ArrayList<LoanDTO>();
		QueryResultIterator<Loan> iterator = query.iterator();
		int num = 0;
		while (iterator.hasNext()) {
			LoanDTO dto = createDTOFromLoan(iterator.next());
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
		return CollectionResponse.<LoanDTO> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	private Loan createLoanFromDTO(LoanDTO loanDto) throws Exception {

		Loan loan = new Loan();
		loan.setId(loanDto.getId());
		loan.setAmount(loanDto.getAmount());
		loan.setCreatedDate(loanDto.getCreatedDate());
		loan.setMaturityDate(loanDto.getMaturityDate());
		loan.setBorrowerId(loanDto.getBorrowerId());
		loan.setLenderId(loanDto.getLenderId());
		loan.setOfferId(loanDto.getOfferId());
		loan.setPartialPay(loanDto.isPartialPay());
		loan.setTerm(loanDto.getTerm());
		loan.setAccruedInterest(loanDto.getAccruedInterest());
		loan.setInterestRate(loanDto.getInterestRate());
		loan.setInterestRateSusp(loanDto.getInterestRateSusp());
		loan.setAccruedIntInSusp(loanDto.getAccruedIntInSusp());
		loan.setInterestAccrualInterval(loanDto.getInterestAccrualInterval());
		// loan.setLastIntAccrualDate(loanDto.getLastIntAccrualDate());
		loan.setNextIntAccrualDate(loanDto.getNextIntAccrualDate());
		loan.setAccrueInSusp(loanDto.isAccrueInSusp());
		loan.setInterestComputationMethod(loanDto
				.getInterestComputationMethod());
		loan.setInterestComputationTerm(loanDto.getInterestComputationTerm());
		loan.setInterestApplicationMethod(loanDto
				.getInterestApplicationMethod());
		// loan.setLastIntAppDate(loanDto.getLastIntAppDate());
		loan.setNextIntAppDate(loanDto.getNextIntAppDate());
		loan.setIntPayingAccount(loanDto.getIntPayingAccount());
		loan.setIntPaidAccount(loanDto.getIntPaidAccount());
		loan.setTransactionType(loanDto.getTransactionType());

		return loan;
	}

	private LoanDTO createDTOFromLoan(Loan loan) throws Exception {

		LoanDTO loanDto = new LoanDTO();
		loanDto.setId(loan.getId());
		loanDto.setAmount(loan.getAmount());
		loanDto.setCreatedDate(loan.getCreatedDate());
		loanDto.setMaturityDate(loan.getMaturityDate());
		loanDto.setBorrowerId(loan.getBorrowerId());
		loanDto.setLenderId(loan.getLenderId());
		loanDto.setBorrower(PeerLendingUtil.GetMember(loan.getBorrowerId())
				.getSurname());
		loanDto.setLender(PeerLendingUtil.GetMember(loan.getLenderId())
				.getSurname());
		loanDto.setOfferId(loan.getOfferId());
		loanDto.setPartialPay(loan.isPartialPay());
		loanDto.setTerm(loan.getTerm());
		loanDto.setAccruedInterest(loan.getAccruedInterest());
		loanDto.setInterestRate(loan.getInterestRate());
		loanDto.setInterestRateSusp(loan.getInterestRateSusp());
		loanDto.setAccruedIntInSusp(loan.getAccruedIntInSusp());
		loanDto.setInterestAccrualInterval(loan.getInterestAccrualInterval());
		loanDto.setLastIntAccrualDate(loan.getLastIntAccrualDate());
		loanDto.setNextIntAccrualDate(loan.getNextIntAccrualDate());
		loanDto.setAccrueInSusp(loan.isAccrueInSusp());
		loanDto.setInterestComputationMethod(loan
				.getInterestComputationMethod());
		loanDto.setInterestComputationTerm(loan.getInterestComputationTerm());
		loanDto.setInterestApplicationMethod(loan
				.getInterestApplicationMethod());
		loanDto.setLastIntAppDate(loan.getLastIntAppDate());
		loanDto.setNextIntAppDate(loan.getNextIntAppDate());
		loanDto.setIntPayingAccount(loan.getIntPayingAccount());
		loanDto.setIntPaidAccount(loan.getIntPaidAccount());
		loanDto.setTransactionType(loan.getTransactionType());

		return loanDto;
	}

	@ApiMethod(name = "editLoan")
	public RequestResult editLoan(LoanDTO loanDTO) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Successful");
		try {
			// create loan from dto
			Loan loanFromDTO = createLoanFromDTO(loanDTO);
			// check if loan actually exists
			Loan loanExists = findRecord(loanFromDTO.getId());
			if (loanExists == null) {
				throw new NotFoundException("Account does not exist");
			}
			if (loanFromDTO.getLastIntAccrualDate() == null) {
				loanFromDTO.setLastIntAccrualDate(loanExists
						.getLastIntAccrualDate());
			}
			if (loanFromDTO.getLastIntAppDate() == null) {
				loanFromDTO.setLastIntAppDate(loanExists.getLastIntAppDate());
			}
			ofy().save().entity(loanFromDTO).now();
			re.setSuccess(true);
			re.setResultMessage("Loan Updated.<br/>Id = " + loanFromDTO.getId());
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "loanRepaymentSchedule")
	public RequestResult LoanRepaymentSchedule(@Named("id") Long id,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Loan loan = findRecord(id);
			if (loan == null) {
				throw new NotFoundException("Loan  [ " + id
						+ " ] does not exist");
			}
			List<Installment> installments = new ArrayList<Installment>();
			int term = loan.getTerm();
			double instamount = loan.getAmount();
			double balance = loan.getAmount();
			if (term > 0) {
				instamount = (double) loan.getAmount()
						/ (double) loan.getTerm();
			}

			for (int i = 0; i < (term + 1); i++) {
				Installment inst = new Installment();
				inst.setRepaymentDate(DateExtension.addMonths(
						loan.getCreatedDate(), i));

				inst.setAmount(instamount);
				inst.setBalance(balance);

				balance -= instamount;

				installments.add(inst);
			}
			re.setSuccess(true);
			re.setResultMessage("Successful");
			re.setClientToken(CollectionResponse.<Installment> builder()
					.setItems(installments).setNextPageToken(cursorString)
					.build());
			return re;

		} catch (Exception e) {
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "retrieveLoan")
	public RequestResult retrieveLoan(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Successful");
		try {
			Loan loan = findRecord(id);
			if (loan == null) {
				throw new NotFoundException("Loan [ " + id
						+ " ]  does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(loan);
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

}
