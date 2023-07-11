package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;

import javax.inject.Named;

import com.google.api.server.spi.config.Api;
import com.google.api.server.spi.config.ApiMethod;
import com.google.api.server.spi.config.ApiNamespace;
import com.google.api.server.spi.config.Nullable;
import com.google.api.server.spi.response.CollectionResponse;
import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.google.appengine.api.datastore.Cursor;
import com.google.appengine.api.datastore.QueryResultIterator;
import com.googlecode.objectify.Work;
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.LendingGroupDTO;
import com.sp.fanikiwa.entity.Lendinggroup;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.RequestResult;

@Api(name = "lendinggroupendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class LendingGroupEndpoint {

	final int MAXRETRIES = 3;

	public LendingGroupEndpoint() {

	}

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listLendinggroup")
	public CollectionResponse<Lendinggroup> listLendinggroup(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Lendinggroup> query = ofy().load().type(Lendinggroup.class);
		return listLendinggroupsByQuery(query, cursorString, count);
	}

	@ApiMethod(name = "retrieveLendinggroupsByCreator")
	public CollectionResponse<Lendinggroup> retrieveLendinggroupsByCreator(
			@Named("email") String email,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		MemberEndpoint mep = new MemberEndpoint();
		Member member = mep.GetMemberByEmail(email);
		Query<Lendinggroup> query = ofy().load().type(Lendinggroup.class)
				.filter("creator", member);
		return listLendinggroupsByQuery(query, cursorString, count);
	}

	public CollectionResponse<Lendinggroup> retrieveSubgroups(
			@Named("groupname") String groupname,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Lendinggroup> query = ofy().load().type(Lendinggroup.class)
				.filter("parentGroup", groupname);
		return listLendinggroupsByQuery(query, cursorString, count);
	}

	private CollectionResponse<Lendinggroup> listLendinggroupsByQuery(
			Query<Lendinggroup> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Lendinggroup> records = new ArrayList<Lendinggroup>();
		QueryResultIterator<Lendinggroup> iterator = query.iterator();
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
		return CollectionResponse.<Lendinggroup> builder().setItems(records)
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
	@ApiMethod(name = "getLendinggroupByID")
	public Lendinggroup getLendinggroupByID(@Named("groupId") String id) {
		return findRecord(id);
	}

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Lendinggroup
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateLendinggroup")
	public Lendinggroup updateLendinggroup(Lendinggroup Lendinggroup)
			throws NotFoundException {
		Lendinggroup record = findRecord(Lendinggroup.getGroupName());
		if (record == null) {
			throw new NotFoundException("Group does not exist");
		}
		ofy().save().entities(Lendinggroup).now();
		return Lendinggroup;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeLendinggroup")
	public RequestResult removeLendinggroup(@Named("id") String id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		try {
			Lendinggroup record = findRecord(id);
			if (record == null) {
				throw new NotFoundException("Lending Group does not exist");
			}
			if (record.getGroupName().equals("ROOT")) {
				throw new Exception(
						"You are not allowed to delete ROOT Lending Group.");
			}
			Collection<Lendinggroup> subgroups = this.retrieveSubgroups(id,
					null, null).getItems();
			if (subgroups.size() > 0) {
				throw new Exception("This Group has [ " + subgroups.size()
						+ " ] sub group(s). Delete sub group(s) first.");
			}
			ofy().delete().entity(record).now();
			re.setSuccess(true);
			re.setResultMessage("Lending Group Removed.");
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	private Lendinggroup findRecord(String id) {
		return ofy().load().type(Lendinggroup.class).id(id).now();
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Lendinggroup
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	// @ApiMethod(name = "insertLendinggroup")
	public Lendinggroup insertLendinggroup(Lendinggroup Lendinggroup)
			throws NotFoundException, ConflictException {
		if (Lendinggroup.getGroupName() != null) {
			if (findRecord(Lendinggroup.getGroupName()) != null) {
				throw new ConflictException("Group already exists");
			}
		}
		ofy().save().entities(Lendinggroup).now();
		return Lendinggroup;
	}

	@ApiMethod(name = "saveLendinggroup")
	public RequestResult saveLendinggroup(final LendingGroupDTO lendingGroupDTO) {
		final RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		Lendinggroup group = ofy().transactNew(MAXRETRIES,
				new Work<Lendinggroup>() {
					public Lendinggroup run() {
						// This work must be Idempotent
						Lendinggroup group = null;
						try {
							group = createLendinggroupDTO(lendingGroupDTO);
							re.setSuccess(true);
							re.setResultMessage("Group Created.");
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
							re.setSuccess(false);
							re.setResultMessage(e.getMessage().toString());
						}
						return group;
					}
				});
		return re;
	}

	private Lendinggroup createLendinggroupDTO(LendingGroupDTO lendingGroupDTO)
			throws Exception {
		// Construct Lendinggroup
		Lendinggroup group = new Lendinggroup();
		group.setGroupName(lendingGroupDTO.getGroupName());
		group.setCreatedOn(new Date());
		group.setLastModified(new Date());
		group.setParentGroup(lendingGroupDTO.getParentGroup());

		Member member = ofy().transactionless().load().type(Member.class)
				.filter("email", lendingGroupDTO.getCreatorEmail()).first()
				.now();
		if (member != null)
			group.setCreator(member);

		// save it
		Lendinggroup savedgroup = this.insertLendinggroup(group);

		return savedgroup;
	}

	public Lendinggroup CreateRootMailingGroup(Member member)
			throws NotFoundException, ConflictException {
		// Construct Lendinggroup
		Lendinggroup group = new Lendinggroup();
		group.setGroupName(member.getEmail());
		group.setCreatedOn(new Date());
		group.setLastModified(new Date());
		group.setCreator(member);
		group.setParentGroup("ROOT"); // ROOT Has no parent

		// save it
		Lendinggroup savedgroup = this.insertLendinggroup(group);

		return savedgroup;
	}

}
