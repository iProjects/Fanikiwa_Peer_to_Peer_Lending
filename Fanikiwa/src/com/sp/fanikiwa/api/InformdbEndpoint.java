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

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.sp.fanikiwa.entity.Informdb; 
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.pdf.ContractPDF;
import com.sp.fanikiwa.pdf.TestPDF;
import com.sp.utils.MailUtil;

import java.util.ArrayList;
import java.util.List; 

import javax.inject.Named;


@Api(name = "informdbendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class InformdbEndpoint {

	/**
	 * This method lists all the entities inserted in datastore.
	 * It uses HTTP GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 * persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listInformdb")
	public CollectionResponse<Informdb> listInformdb(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Informdb> query = ofy().load().type(Informdb.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Informdb> records = new ArrayList<Informdb>();
		QueryResultIterator<Informdb> iterator = query.iterator();
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
		return CollectionResponse.<Informdb> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This method gets the entity having primary key id. It uses HTTP GET method.
	 *
	 * @param id the primary key of the java bean.
	 * @return The entity with primary key id.
	 */
	@ApiMethod(name = "getInformdb")
	public Informdb getInformdb(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity already
	 * exists in the datastore, an exception is thrown.
	 * It uses HTTP POST method.
	 *
	 * @param informdb the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException 
	 */
	@ApiMethod(name = "insertInformdb")
	public Informdb insertInformdb(Informdb informdb) throws NotFoundException, ConflictException {
		if (informdb.getId() != null) {
			if (findRecord(informdb.getId()) != null) {
				throw new ConflictException("Messsage already exists");
			}
		}
		ofy().save().entities(informdb).now();
		return informdb;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does not
	 * exist in the datastore, an exception is thrown.
	 * It uses HTTP PUT method.
	 *
	 * @param informdb the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException 
	 */
	@ApiMethod(name = "updateInformdb")
	public Informdb updateInformdb(Informdb informdb) throws NotFoundException {
		Informdb record = findRecord(informdb.getId());
		if (record == null) {
			throw new NotFoundException("Message does not exist");
		}
		ofy().save().entities(informdb).now();
		return informdb;
	}

	/**
	 * This method removes the entity with primary key id.
	 * It uses HTTP DELETE method.
	 *
	 * @param id the primary key of the entity to be deleted.
	 * @throws NotFoundException 
	 */
	@ApiMethod(name = "removeInformdb")
	public void removeInformdb(@Named("id") Long id) throws NotFoundException  {
		Informdb record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Message [ " + id
						+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private boolean containsInformdb(Informdb informdb) {
		boolean contains = true;

			Informdb item = findRecord(informdb.getId());
			if (item == null) {
				contains = false;
			}
		return contains;
	}

	private Informdb findRecord(Long id) {
		return ofy().load().type(Informdb.class).id(id).now();
	}

	@ApiMethod(name = "sendMail")
	public RequestResult sendMail(@Named("to") String to,
			@Named("subject") String subject, @Named("body") String body)
			throws NotFoundException {

		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		try {
//			MailUtil.sendEmailWithPDF(to, subject,body, "Test.PDF", TestPDF.class);
			MailUtil.sendEmailWithPDF(to, subject,body, "FanikiwaContract.PDF", ContractPDF.class);
			re.setSuccess(true);
			re.setResultMessage("Successful");
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
