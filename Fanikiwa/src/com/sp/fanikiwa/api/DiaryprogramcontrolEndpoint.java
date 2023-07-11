package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.Enums.AccountLimitStatus;
import com.sp.fanikiwa.Enums.PassFlag;
import com.sp.fanikiwa.entity.Diaryprogramcontrol;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.fanikiwa.entity.ValueDatedTransaction;
import com.google.api.server.spi.config.Api;
import com.google.api.server.spi.config.ApiMethod;
import com.google.api.server.spi.config.ApiNamespace;
import com.google.api.server.spi.config.Nullable;
import com.google.api.server.spi.response.CollectionResponse;
import com.google.api.server.spi.response.ConflictException;
import com.google.api.server.spi.response.NotFoundException;
import com.google.appengine.api.datastore.Cursor;
import com.google.appengine.api.datastore.QueryResultIterator;
import com.googlecode.objectify.VoidWork;
import com.googlecode.objectify.Work;
import com.googlecode.objectify.cmd.Query;

import java.util.ArrayList;
import java.util.List;

import javax.inject.Named;


@Api(name = "diaryprogramcontrolendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class DiaryprogramcontrolEndpoint {

	/**
	 * This method lists all the entities inserted in datastore.
	 * It uses HTTP GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 * persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listDiaryprogramcontrol")
	public CollectionResponse<Diaryprogramcontrol> listDiaryprogramcontrol(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Diaryprogramcontrol> query = ofy().load().type(Diaryprogramcontrol.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Diaryprogramcontrol> records = new ArrayList<Diaryprogramcontrol>();
		QueryResultIterator<Diaryprogramcontrol> iterator = query.iterator();
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
		return CollectionResponse.<Diaryprogramcontrol> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new <code>Diaryprogramcontrol</code> object.
	 * 
	 * @param Diaryprogramcontrol
	 *            The object to be added.
	 * @return The object to be added.
	 */
	@ApiMethod(name = "insertDiaryprogramcontrol")
	public Diaryprogramcontrol insertDiaryprogramcontrol(Diaryprogramcontrol Diaryprogramcontrol) throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (Diaryprogramcontrol.getId() != null) {
			if (findRecord(Diaryprogramcontrol.getId()) != null) {
				throw new ConflictException("DiaryProgramControl already exists");
			}
		}
		// Since our @Id field is a Long, Objectify will generate a unique value
		// for us
		// when we use put
		ofy().save().entity(Diaryprogramcontrol).now();
		return Diaryprogramcontrol;
	}

	/**
	 * This updates an existing <code>Diaryprogramcontrol</code> object.
	 * 
	 * @param Diaryprogramcontrol
	 *            The object to be added.
	 * @return The object to be updated.
	 */
	@ApiMethod(name = "updateDiaryprogramcontrol")
	public Diaryprogramcontrol updateDiaryprogramcontrol(Diaryprogramcontrol Diaryprogramcontrol) throws NotFoundException {
		if (findRecord(Diaryprogramcontrol.getId()) == null) {
			throw new NotFoundException("Diaryprogramcontrol does not exist");
		}
		ofy().save().entity(Diaryprogramcontrol).now();
		return Diaryprogramcontrol;
	}

	/**
	 * This deletes an existing <code>Diaryprogramcontrol</code> object.
	 * 
	 * @param id
	 *            The id of the object to be deleted.
	 */
	@ApiMethod(name = "removeDiaryprogramcontrol")
	public void removeDiaryprogramcontrol(@Named("id") Long id) throws NotFoundException {
		Diaryprogramcontrol record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Diaryprogramcontrol [ " + id
						+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	// Private method to retrieve a <code>Diaryprogramcontrol</code> record
	private Diaryprogramcontrol findRecord(Long id) {
		return ofy().load().type(Diaryprogramcontrol.class).id(id).now();
		// or return ofy().load().type(Diaryprogramcontrol.class).filter("id",id).first.now();
	}

}