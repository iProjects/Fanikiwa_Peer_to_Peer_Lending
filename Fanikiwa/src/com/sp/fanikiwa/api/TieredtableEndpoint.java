package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.sp.fanikiwa.entity.TieredDet;
import com.sp.fanikiwa.entity.Tieredtable;
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

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import javax.inject.Named;

@Api(name = "tieredtableendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class TieredtableEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listTieredtable")
	public CollectionResponse<Tieredtable> listTieredtable(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Tieredtable> query = ofy().load().type(Tieredtable.class);
		return listTieredtableFromQuery(query, cursorString, count);
	}

	private CollectionResponse<Tieredtable> listTieredtableFromQuery(
			Query<Tieredtable> query, String cursorString, Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Tieredtable> records = new ArrayList<Tieredtable>();
		QueryResultIterator<Tieredtable> iterator = query.iterator();
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
		return CollectionResponse.<Tieredtable> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new <code>Tieredtable</code> object.
	 * 
	 * @param Tieredtable
	 *            The object to be added.
	 * @return The object to be added.
	 */
	@ApiMethod(name = "insertTieredtable")
	public Tieredtable insertTieredtable(Tieredtable Tieredtable)
			throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (Tieredtable.getId() != null) {
			if (findRecord(Tieredtable.getId()) != null) {
				throw new ConflictException("Tiered Table already exists");
			}
		}
		// Since our @Id field is a Long, Objectify will generate a unique value
		// for us
		// when we use put
		ofy().save().entity(Tieredtable).now();
		return Tieredtable;
	}

	/**
	 * This updates an existing <code>Tieredtable</code> object.
	 * 
	 * @param Tieredtable
	 *            The object to be added.
	 * @return The object to be updated.
	 */
	@ApiMethod(name = "updateTieredtable")
	public Tieredtable updateTieredtable(Tieredtable Tieredtable)
			throws NotFoundException {
		if (findRecord(Tieredtable.getId()) == null) {
			throw new NotFoundException("Tieredtable does not exist");
		}
		ofy().save().entity(Tieredtable).now();
		return Tieredtable;
	}

	/**
	 * This deletes an existing <code>Tieredtable</code> object.
	 * 
	 * @param id
	 *            The id of the object to be deleted.
	 */
	@ApiMethod(name = "removeTieredtable")
	public void removeTieredtable(@Named("id") Long id)
			throws NotFoundException {
		Tieredtable record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Tieredtable [ " + id
					+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private Tieredtable findRecord(Long id) {
		return ofy().load().type(Tieredtable.class).id(id).now();
	}

	public Collection<Tieredtable> getTieredTableId(
			@Named("tableid") Long tieredTableId) {
		Query<Tieredtable> query = ofy().load().type(Tieredtable.class)
				.filter("TieredID", tieredTableId);
		return listTieredtableFromQuery(query, null, null).getItems();
	}

	@ApiMethod(name = "getTieredtableById")
	public Tieredtable getTieredtableById(@Named("id") Long id) {
		return findRecord(id);
	}

}