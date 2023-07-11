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
import com.sp.fanikiwa.entity.MpesaTestIPNMessage;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferReceipient;

import static com.sp.fanikiwa.api.OfyService.ofy;

import java.util.ArrayList;
import java.util.List;
 



import javax.inject.Named;

@Api(name = "mpesaipnmessageendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class MpesaIPNTestMessageEndpoint {

	/**
	 * This method lists all the entities inserted in datastore.
	 * It uses HTTP GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 * persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listMpesaTestIPNMessage")
	public CollectionResponse<MpesaTestIPNMessage> listMpesaTestIPNMessage(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<MpesaTestIPNMessage> query = ofy().load().type(MpesaTestIPNMessage.class);
		return GetMpesaTestIPNMessagesFromQuery(query,cursorString,count);
	}
	/**
	 * This method gets the entity having primary key id. It uses HTTP GET method.
	 *
	 * @param id the primary key of the java bean.
	 * @return The entity with primary key id.
	 */
	@ApiMethod(name = "getMpesaTestIPNMessage")
	public MpesaTestIPNMessage getMpesaTestIPNMessage(@Named("id") Long id) {
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

	@ApiMethod(name = "insertMpesaTestIPNMessage")
	public MpesaTestIPNMessage insertMpesaTestIPNMessage(MpesaTestIPNMessage mpesaIPNMessage) throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (mpesaIPNMessage.getId() != null) {
			if (findRecord(mpesaIPNMessage.getId()) != null) {
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
	@ApiMethod(name = "updateMpesaTestIPNMessage")
	public MpesaTestIPNMessage updateMpesaTestIPNMessage(MpesaTestIPNMessage mpesaIPNMessage) throws NotFoundException {
		if (findRecord(mpesaIPNMessage.getId()) == null) {
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
	@ApiMethod(name = "removeMpesaTestIPNMessage")
	public void removeMpesaTestIPNMessage(@Named("id") Long id) throws NotFoundException {
		MpesaTestIPNMessage record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Message  [ " + id
						+ " ] does not exist");
		}
		ofy().delete().entity(record).now();
	}

	
	// Private method to retrieve a <code>Quote</code> record
	private MpesaTestIPNMessage findRecord(Long id) {
		return ofy().load().type(MpesaTestIPNMessage.class).id(id).now();
	}
	
	
	@ApiMethod(name = "isMpesaTestIPNMessageExists")
	public MpesaTestIPNMessage isMpesaTestIPNMessageExists(MpesaTestIPNMessage msg) {
		// TODO Auto-generated method stub
		return ofy().load().type(MpesaTestIPNMessage.class)
				.filter("mpesaIPNMessageID", msg.getMpesaIPNMessageID()) 
				.first()
				.now();
	
	}
	
	@ApiMethod(name = "listNewMpesaTestIPNMessages")
	public CollectionResponse<MpesaTestIPNMessage> ListNewMpesaTestIPNMessages(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
 
		Query<MpesaTestIPNMessage> query = ofy().load().type(MpesaTestIPNMessage.class)
				.filter("status", "New");
		return GetMpesaTestIPNMessagesFromQuery(query, cursorString, count);
	}
	
	private CollectionResponse<MpesaTestIPNMessage> GetMpesaTestIPNMessagesFromQuery(Query<MpesaTestIPNMessage> query,
			String cursorString, Integer count) {

		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<MpesaTestIPNMessage> records = new ArrayList<MpesaTestIPNMessage>();
		QueryResultIterator<MpesaTestIPNMessage> iterator = query.iterator();
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
		return CollectionResponse.<MpesaTestIPNMessage> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	
	
	
	
	
	
	
}
