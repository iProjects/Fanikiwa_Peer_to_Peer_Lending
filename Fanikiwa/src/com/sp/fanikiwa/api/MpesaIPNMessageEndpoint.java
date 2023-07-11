package com.sp.fanikiwa.api;

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
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.MpesaIPNMessage;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferReceipient;

import static com.sp.fanikiwa.api.OfyService.ofy;

import java.util.ArrayList;
import java.util.List;
 



import javax.inject.Named;

@Api(name = "mpesaipnmessageendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class MpesaIPNMessageEndpoint {

	/**
	 * This method lists all the entities inserted in datastore.
	 * It uses HTTP GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 * persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listMpesaIPNMessage")
	public CollectionResponse<MpesaIPNMessage> listMpesaIPNMessage(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<MpesaIPNMessage> query = ofy().load().type(MpesaIPNMessage.class);
		return GetMpesaIPNMessagesFromQuery(query,cursorString,count);
	}
	/**
	 * This method gets the entity having primary key id. It uses HTTP GET method.
	 *
	 * @param id the primary key of the java bean.
	 * @return The entity with primary key id.
	 */
	@ApiMethod(name = "getMpesaIPNMessage")
	public MpesaIPNMessage getMpesaIPNMessage(@Named("id") String id) {
		return findRecord(id);
	}
 
	/**
	 * This inserts a new entity into App Engine datastore. If the entity already
	 * exists in the datastore, an exception is thrown.
	 * It uses HTTP POST method.
	 *
	 * @param mpesaIPNMessage the entity to be inserted.
	 * @return The inserted entity.
	 */

	@ApiMethod(name = "insertMpesaIPNMessage")
	public MpesaIPNMessage insertMpesaIPNMessage(MpesaIPNMessage mpesaIPNMessage) throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (mpesaIPNMessage.getMpesa_code() != null) {
			if (findRecord(mpesaIPNMessage.getMpesa_code()) != null) {
				throw new ConflictException("Message already exists");
			}
		}
		// Since our @Id field is a Long, Objectify will generate a unique value
		// for us
		// when we use put
		ofy().save().entity(mpesaIPNMessage).now();
		return mpesaIPNMessage;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does not
	 * exist in the datastore, an exception is thrown.
	 * It uses HTTP PUT method.
	 *
	 * @param mpesaIPNMessage the entity to be updated.
	 * @return The updated entity.
	 */
	@ApiMethod(name = "updateMpesaIPNMessage")
	public MpesaIPNMessage updateMpesaIPNMessage(MpesaIPNMessage mpesaIPNMessage) throws NotFoundException {
		if (findRecord(mpesaIPNMessage.getMpesa_code()) == null) {
			throw new NotFoundException("Message does not exist");
		}
		ofy().save().entity(mpesaIPNMessage).now();
		return mpesaIPNMessage;
	}


	/**
	 * This method removes the entity with primary key id.
	 * It uses HTTP DELETE method.
	 *
	 * @param id the primary key of the entity to be deleted.
	 */
	@ApiMethod(name = "removeMpesaIPNMessage")
	public void removeMpesaIPNMessage(@Named("id") String id) throws NotFoundException {
		MpesaIPNMessage record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Message does not exist");
		}
		ofy().delete().entity(record).now();
	}

	
	// Private method to retrieve a <code>Quote</code> record
	private MpesaIPNMessage findRecord(String id) {
		return ofy().load().type(MpesaIPNMessage.class).id(id).now();
	}
	
	
	@ApiMethod(name = "isMpesaIPNMessageExists")
	public MpesaIPNMessage isMpesaIPNMessageExists(MpesaIPNMessage msg) {
		// TODO Auto-generated method stub
		return findRecord(msg.getMpesa_code());
	
	}
	
	@ApiMethod(name = "listNewMpesaIPNMessages")
	public CollectionResponse<MpesaIPNMessage> ListNewMpesaIPNMessages(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
 
		Query<MpesaIPNMessage> query = ofy().load().type(MpesaIPNMessage.class)
				.filter("status", "New");
		return GetMpesaIPNMessagesFromQuery(query, cursorString, count);
	}
	
	private CollectionResponse<MpesaIPNMessage> GetMpesaIPNMessagesFromQuery(Query<MpesaIPNMessage> query,
			String cursorString, Integer count) {

		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<MpesaIPNMessage> records = new ArrayList<MpesaIPNMessage>();
		QueryResultIterator<MpesaIPNMessage> iterator = query.iterator();
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
		return CollectionResponse.<MpesaIPNMessage> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	 
	
}
