package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.sp.fanikiwa.entity.Coa;
import com.sp.fanikiwa.entity.Coadet;
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

@Api(name = "tiereddetendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class TieredDetEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listTieredtable")
	public CollectionResponse<TieredDet> listTieredtable(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<TieredDet> query = ofy().load().type(TieredDet.class);
		return listTieredtableFromQuery(query, cursorString, count);
	}

	private CollectionResponse<TieredDet> listTieredtableFromQuery(
			Query<TieredDet> query, String cursorString, Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<TieredDet> records = new ArrayList<TieredDet>();
		QueryResultIterator<TieredDet> iterator = query.iterator();
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
		return CollectionResponse.<TieredDet> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "selectTieredtableDets")
	public CollectionResponse<TieredDet> selectTieredtableDets(
			@Named("tieredtableid") Long tieredTableId,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<TieredDet> query = ofy().load().type(TieredDet.class)
				.filter("tieredID", tieredTableId);
		return listTieredDetFromQuery(query, cursorString, count);
	}

	private CollectionResponse<TieredDet> listTieredDetFromQuery(
			Query<TieredDet> query, String cursorString, Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<TieredDet> records = new ArrayList<TieredDet>();
		QueryResultIterator<TieredDet> iterator = query.iterator();
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
		return CollectionResponse.<TieredDet> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new <code>TieredDet</code> object.
	 * 
	 * @param TieredDet
	 *            The object to be added.
	 * @return The object to be added.
	 */
	@ApiMethod(name = "insertTieredDet")
	public TieredDet insertTieredDet(TieredDet TieredDet)
			throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (TieredDet.getId() != null) {
			if (findRecord(TieredDet.getId()) != null) {
				throw new ConflictException("TieredDet already exists");
			}
		}
		// Since our @Id field is a Long, Objectify will generate a unique value
		// for us
		// when we use put
		ofy().save().entity(TieredDet).now();
		return TieredDet;
	}

	/**
	 * This updates an existing <code>TieredDet</code> object.
	 * 
	 * @param TieredDet
	 *            The object to be added.
	 * @return The object to be updated.
	 */
	@ApiMethod(name = "updateTieredDet")
	public TieredDet updateTieredDet(TieredDet TieredDet)
			throws NotFoundException {
		if (findRecord(TieredDet.getId()) == null) {
			throw new NotFoundException("TieredDet does not exist");
		}
		ofy().save().entity(TieredDet).now();
		return TieredDet;
	}

	/**
	 * This deletes an existing <code>TieredDet</code> object.
	 * 
	 * @param id
	 *            The id of the object to be deleted.
	 */
	@ApiMethod(name = "removeTieredDet")
	public void removeTieredDet(@Named("id") Long id) throws NotFoundException {
		TieredDet record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("TieredDet does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private TieredDet findRecord(Long id) {
		return ofy().load().type(TieredDet.class).id(id).now();
	}

	public Collection<TieredDet> getTieredtableId(
			@Named("tableid") Long tieredTableId) {
		Query<TieredDet> query = ofy().load().type(TieredDet.class)
				.filter("TieredID", tieredTableId);
		return listTieredtableFromQuery(query, null, null).getItems();
	}

	@ApiMethod(name = "getTieredDetById")
	public TieredDet getTieredDetById(@Named("id") Long id) {
		return findRecord(id);
	}

}
