package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.google.api.server.spi.config.Api;
import com.google.api.server.spi.config.ApiMethod;
import com.google.api.server.spi.config.ApiNamespace;
import com.google.api.server.spi.config.Nullable;
import com.google.api.server.spi.response.CollectionResponse;
import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.ForbiddenException;
import com.google.api.server.spi.response.NotFoundException;
import com.google.appengine.api.datastore.Cursor;
import com.google.appengine.api.datastore.QueryResultIterator;
//import com.googlecode.objectify.VoidWork;
import com.googlecode.objectify.Work;
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.Enums.OffereeIdType;
import com.sp.fanikiwa.Enums.PostingCheckFlag;
import com.sp.fanikiwa.business.AcceptOfferComponent;

import com.sp.fanikiwa.business.MakeOffer.EmailOfferee;
import com.sp.fanikiwa.business.MakeOffer.GroupOfferee;
import com.sp.fanikiwa.business.MakeOffer.MemberOfferee;
import com.sp.fanikiwa.business.MakeOffer.Offeree;
import com.sp.fanikiwa.business.MakeOffer.OffereeToken;
import com.sp.fanikiwa.business.MakeOffer.TelephoneOfferee;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferDTO;
import com.sp.fanikiwa.entity.OfferModel;
import com.sp.fanikiwa.entity.OfferReceipient;
import com.sp.fanikiwa.Enums.OfferStatus;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Settings;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.MailUtil;

import com.sp.utils.PeerLendingUtil;
import com.sp.utils.StringExtension;

import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;

import javax.inject.Named;

@Api(name = "offerendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class OfferEndpoint {

	final int MAXRETRIES = 3;
	final double MINIMUM_OFFER_AMOUNT = 1000.00;

	public OfferEndpoint() {

	}

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listOffer")
	public CollectionResponse<Offer> listOffer(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Offer> query = ofy().load().type(Offer.class);
		return GetOffersFromQuery(query, cursorString, count);
	}

	@ApiMethod(name = "retrieveMyOffers")
	public CollectionResponse<Offer> retrieveMyOffers(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = SearchMemberByEmail(email);
		return ListMyOffers(member.getMemberId(), cursorString, count);
	}

	@ApiMethod(name = "selectMyOffers")
	public CollectionResponse<Offer> ListMyOffers(
			@Named("memberid") long MemberId,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = SearchMember(MemberId);
		Query<Offer> query = ofy().load().type(Offer.class)
				.filter("member", member);
		return GetOffersFromQuery(query, cursorString, count);
	}

	@ApiMethod(name = "retrieveLendOffers")
	public CollectionResponse<Offer> retrieveLendOffers(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = SearchMemberByEmail(email);
		return ListLendOffers(member.getMemberId(), cursorString, count);
	}

	@ApiMethod(name = "ListLendOffers")
	public CollectionResponse<Offer> ListLendOffers(
			@Named("memberid") long MemberId,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		OfferReceipientEndpoint ore = new OfferReceipientEndpoint();
		CollectionResponse<Offer> offers = ore.listOfferReceipient(MemberId,
				cursorString, count);
		return offers;
	}

	@ApiMethod(name = "retrieveBorrowOffers")
	public CollectionResponse<Offer> retrieveBorrowOffers(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = SearchMemberByEmail(email);
		return ListBorrowOffers(member.getMemberId(), cursorString, count);
	}

	@ApiMethod(name = "ListBorrowOffers")
	public CollectionResponse<Offer> ListBorrowOffers(
			@Named("memberid") long MemberId,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		OfferReceipientEndpoint ore = new OfferReceipientEndpoint();
		CollectionResponse<Offer> offers = ore.listOfferReceipient(MemberId,
				cursorString, count);

		return offers;
	}

	// @ApiMethod(name = "PrivateOffersToMember")
	// public CollectionResponse<Offer> PrivateOffersToMember(
	// @Named("memberid") long MemberId,
	// @Nullable @Named("cursor") String cursorString,
	// @Nullable @Named("count") Integer count) {
	//
	// Member member = ofy().load().type(Member.class).id(MemberId).now();
	// Query<Offer> query = ofy().load().type(Offer.class)
	// .filter("privateOffer", false).filter("offerrees", member)
	// .filter("status", "Open").filter("expiryDate >", new Date());
	// return GetOffersFromQuery(query, cursorString, count);
	// }

	@ApiMethod(name = "retrievePublicOffers")
	public CollectionResponse<Offer> RetrievePublicOffers(
			@Named("email") String email, @Named("offerType") String offerType,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Member member = PeerLendingUtil.GetMember(email);

		Query<Offer> query = ofy().load().type(Offer.class)
				.filter("member !=", member).filter("offerType", offerType)
				.filter("privateOffer", false).filter("status", "Open");
		return GetOffersFromQuery(query, cursorString, count);
	}

	private CollectionResponse<Offer> GetOffersFromQuery(Query<Offer> query,
			String cursorString, Integer count) {

		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Offer> records = new ArrayList<Offer>();
		QueryResultIterator<Offer> iterator = query.iterator();
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
		return CollectionResponse.<Offer> builder().setItems(records)
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
	@ApiMethod(name = "getOfferByID")
	public Offer getOfferByID(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "selectOffer")
	public RequestResult selectOffer(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Offer offer = findRecord(id);
			if (offer == null) {
				throw new NotFoundException("Offer [ " + id
						+ " ]  does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(offer);
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Offer
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateOffer")
	public Offer updateOffer(Offer Offer) throws NotFoundException {
		Offer record = findRecord(Offer.getId());
		if (record == null) {
			throw new NotFoundException("Offer does not exist");
		}
		ofy().save().entities(Offer).now();
		return Offer;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeOffer")
	public RequestResult removeOffer(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Offer offer = findRecord(id);
			if (offer == null) {
				throw new NotFoundException("Offer [ " + id
						+ " ]  does not exist");
			}

			re = ValidateOfferForDeleting(offer);
			if (!re.isSuccess())
				return re;

			Member member = offer.getMember();
			PeerLendingUtil.SetOfferStatus(offer, OfferStatus.Deleting);

			// 1.Unblock funds
			if (offer.getOfferType().toUpperCase().equals("L")) {
				AccountEndpoint aep = new AccountEndpoint();
				Account lenderCurr = member.getCurrentAccount();
				aep.UnBlockFunds(lenderCurr, offer.getAmount());
			}

			// remove the recepients
			OfferReceipientEndpoint orep = new OfferReceipientEndpoint();
			orep.DeleteOfferReciepients(offer.getId());

			// remove the offer
			ofy().delete().entity(offer).now();
			re.setSuccess(true);
			re.setResultMessage("Offer deleted.");
			return re;
		} catch (Exception e) {
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	private Offer findRecord(Long id) {
		return ofy().load().type(Offer.class).id(id).now();
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Offer
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertOffer")
	public Offer insertOffer(Offer offer) throws NotFoundException,
			ConflictException {
		if (offer.getId() != null) {
			if (findRecord(offer.getId()) != null) {
				throw new ConflictException("Offer already exists");
			}
		}
		ofy().save().entities(offer).now();
		return offer;
	}

	@ApiMethod(name = "saveOffer")
	public RequestResult saveOffer(final OfferDTO offerDto) {
		final RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		RequestResult ore = ValidateOffer(offerDto);

		if (!ore.isSuccess()) // the offer is not valid
		{
			return ore;
		}

		Offer offer = ofy().transactNew(MAXRETRIES, new Work<Offer>() {
			public Offer run() {
				// This work must be Idempotent
				Offer offer = null;
				try {
					offer = createOfferFromDTO(offerDto);
					re.setSuccess(true);
					re.setResultMessage("Offer Id = "
							+ offer.getId().toString());
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
					re.setSuccess(false);
					re.setResultMessage(e.getMessage().toString());
				}
				return offer;
			}
		});
		return re;
	}

	private Offer createOfferFromDTO(OfferDTO offerDto) throws Exception {
		// Construct offer
		Offer o = new Offer();
		o.setAmount(offerDto.getAmount());
		o.setCreatedDate(new Date());
		o.setDescription(offerDto.getDescription());
		o.setExpiryDate(DateExtension.addMonths(o.getCreatedDate(),
				Config.GetInt("OFFEREXPIRYTIMESPANINMONTHS")));
		o.setInterest(offerDto.getInterest());

		// MemberEndpoint mep = new MemberEndpoint();
		// Member member = mep.GetMemberByEmail(offerDto.getEmail());
		Member member = ofy().transactionless().load().type(Member.class)
				.filter("email", offerDto.getEmail()).first().now();

		o.setMember(member);
		// o.setOfferees(offerDto.getOfferees());
		o.setOfferType(offerDto.getOfferType());
		o.setPartialPay(offerDto.isPartialPay());
		o.setPrivateOffer(offerDto.isPrivateOffer());
		o.setStatus(offerDto.getStatus());
		o.setTerm(offerDto.getTerm());

		if (offerDto.getOfferType().toUpperCase().equals("L")) {
			// Step 1 - Block funds.
			// calls GLs funds block service.
			AccountEndpoint sPostingClient = new AccountEndpoint();
			if (GLUtil.GetAvailableBalance(member.getCurrentAccount()) < offerDto
					.getAmount())
				throw new ForbiddenException("Insufficient funds");

			// BlockFunds function checks all account status before the actual
			// block
			sPostingClient.BlockFunds(member.getCurrentAccount(),
					offerDto.getAmount());
		}

		// save it
		Offer savedOffer = this.insertOffer(o);

		// Create Offerees using the returned/saved offer
		String delimiters = "\\,+|\\;+"; // | delimeted
		for (String s : offerDto.getOfferees().split(delimiters)) {
			if (!StringExtension.isNullOrEmpty(s)) {
				List<Member> members = GetRecipient(s);

				if (members != null) {
					for (Member m : members) {
						// save receipient
						OfferReceipient or = new OfferReceipient(m, savedOffer);
						OfferReceipientEndpoint oep = new OfferReceipientEndpoint();
						oep.insertOfferReceipient(or);
					}
				}
			}
		}

		return savedOffer;
	}

	private Member SearchMember(Long MemberId) {
		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.getMemberByID(MemberId);
		return member;
	}

	private Member SearchMemberByEmail(String email) {
		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);
		return member;
	}

	private List<Member> GetRecipient(String s) throws Exception {
		List<Member> members = new ArrayList<Member>();
		// create
		Offeree of = CreateOffereeFromString(s);

		// save
		if (of == null)
			return null;

		if (of instanceof GroupOfferee) {
			GroupOfferee go = (GroupOfferee) of;
			for (Member m : go.getMembers()) {
				if (m != null)
					members.add(m);
			}
		} else if (of instanceof EmailOfferee) {
			EmailOfferee eo = (EmailOfferee) of;
			Member m = eo.getMember();
			if (m != null)
				members.add(m);
		} else if (of instanceof TelephoneOfferee) {
			TelephoneOfferee to = (TelephoneOfferee) of;
			Member m = to.getMember();
			if (m != null)
				members.add(m);
		} else if (of instanceof MemberOfferee) {
			MemberOfferee mo = (MemberOfferee) of;
			Member m = mo.getMember();
			if (m != null)
				members.add(m);
		}

		return members;
	}

	private Offeree CreateOffereeFromString(String s) throws Exception {
		OffereeToken ot = CreateOfferTokenFromString(s);
		if (ot != null) {
			switch (ot.getOffereeType()) {
			case Email:
				return StringExtension.parseObjectFromString(ot.getValue(),
						EmailOfferee.class);
			case Telephone:
				return StringExtension.parseObjectFromString(ot.getValue(),
						TelephoneOfferee.class);
			case Member:
				return StringExtension.parseObjectFromString(ot.getValue(),
						MemberOfferee.class);
			case Group:
				return StringExtension.parseObjectFromString(ot.getValue(),
						GroupOfferee.class);
			}
		}
		return null;
	}

	private OffereeToken CreateOfferTokenFromString(String s) {
		// checking simple syntaz
		// Scanner sc = new Scanner(s);
		// Email
		if (MailUtil.isValidEmailRegex(s))
			return new OffereeToken(OffereeIdType.Email, s);

		// Test telephone - startswith(T(Number)| 0 | +254 )
		if (StringExtension.isKenyaMobileNumber(s))
			return new OffereeToken(OffereeIdType.Telephone, s);

		// Test Member - M(Id)
		if (StringExtension.isNumeric(s))
			return new OffereeToken(OffereeIdType.Member, s);

		// Test Group - Any alphanumeric
		if (StringExtension.isAlphaNumeric(s))
			return new OffereeToken(OffereeIdType.Group, s);

		return null;
	}

	@ApiMethod(name = "acceptOffer")
	public RequestResult AcceptOffer(@Named("id") Long id,
			@Named("email") String email) throws Exception {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {

			AcceptOfferComponent aoc = new AcceptOfferComponent();

			Offer offer = findRecord(id);
			if (offer == null) {
				throw new NotFoundException("Offer does not exist");
			}

			MemberEndpoint mep = new MemberEndpoint();
			Member member = mep.GetMemberByEmail(email);
			if (member == null) {
				throw new NotFoundException("Member does not exist");
			}

			if (offer.getOfferType().toUpperCase().equals("L")) {
				re.setSuccess(true);
				re.setResultMessage("Loan Id = "
						+ aoc.AcceptLendOffer(member, offer).getId().toString());
				return re;
			} else {
				re.setSuccess(true);
				re.setResultMessage("Loan Id = "
						+ aoc.AcceptBorrowOffer(member, offer).getId()
								.toString());
				return re;
			}

		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "acceptPartialBorrowOffer")
	public RequestResult AcceptPartialBorrowOffer(@Named("id") Long id,
			@Named("email") String email) throws Exception {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {

			AcceptOfferComponent aoc = new AcceptOfferComponent();

			Offer offer = findRecord(id);
			if (offer == null) {
				throw new NotFoundException("Offer does not exist");
			}

			MemberEndpoint mep = new MemberEndpoint();
			Member member = mep.GetMemberByEmail(email);
			if (member == null) {
				throw new NotFoundException("Member does not exist");
			}
			re.setSuccess(true);
			re.setResultMessage("Loan Id = "
					+ aoc.AcceptPartialBorrowOffer(member, offer).getId()
							.toString());
			return re;

		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	/* Private region */
	private RequestResult ValidateOffer(OfferDTO offerDto) {
		RequestResult re = new RequestResult();
		re.setSuccess(true);
		re.setResultMessage("Successful");

		if (offerDto.getAmount() < MINIMUM_OFFER_AMOUNT) {
			re.setSuccess(false);
			re.setResultMessage("Offer amount is less than minimum amount of KES["
					+ MINIMUM_OFFER_AMOUNT + "]");
			return re;
		}
		if (offerDto.getTerm() < 0) {
			re.setSuccess(false);
			re.setResultMessage("Term[" + offerDto.getTerm()
					+ "] cannot be less than zero");
			return re;
		}
		if (offerDto.getInterest() < 0) {
			re.setSuccess(false);
			re.setResultMessage("Interest rate[" + offerDto.getInterest()
					+ "] cannot be less than zero");
			return re;
		}

		return re;
	}

	private RequestResult ValidateOfferForDeleting(Offer offer) {

		RequestResult re = new RequestResult();
		re.setSuccess(true);
		re.setResultMessage("Successful");

		if (offer.getStatus().equals("Processing")) {
			re.setSuccess(false);
			re.setResultMessage(MessageFormat.format(
					"Cannot delete Offer [{0}], Status is Processing. ", offer
							.getId().toString()));
			return re;
		}
		if (offer.getStatus().equals("Closed")) {
			re.setSuccess(false);
			re.setResultMessage(MessageFormat.format(
					"Cannot delete Offer [{0}], Status is Closed. ", offer
							.getId().toString()));
			return re;
		}
		if (offer.getStatus().equals("Deleting")) {
			re.setSuccess(false);
			re.setResultMessage(MessageFormat.format(
					"Cannot delete Offer [{0}], Status is Deleting. ", offer
							.getId().toString()));
			return re;
		}

		return re;
	}

}
