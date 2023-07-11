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

import com.sp.fanikiwa.entity.Customer;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Named;

@Api(name = "customerendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class CustomerEndpoint {

	/**
	 * This method lists all the entities inserted in datastore.
	 * It uses HTTP GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 * persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listCustomer")
	public CollectionResponse<Customer> listCustomer(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Customer> query = ofy().load().type(Customer.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Customer> records = new ArrayList<Customer>();
		QueryResultIterator<Customer> iterator = query.iterator();
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
		return CollectionResponse.<Customer> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This method gets the entity having primary key id. It uses HTTP GET method.
	 *
	 * @param id the primary key of the java bean.
	 * @return The entity with primary key id.
	 */
	@ApiMethod(name = "getCustomer")
	public Customer getCustomer(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity already
	 * exists in the datastore, an exception is thrown.
	 * It uses HTTP POST method.
	 *
	 * @param customer the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException 
	 */
	@ApiMethod(name = "insertCustomer")
	public Customer insertCustomer(Customer customer) throws NotFoundException, ConflictException {
		if (customer.getCustomerId() != null) {
			if (findRecord(customer.getCustomerId()) != null) {
				throw new ConflictException("Customer already exists");
			}
		}
		ofy().save().entities(customer).now();
		return customer;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does not
	 * exist in the datastore, an exception is thrown.
	 * It uses HTTP PUT method.
	 *
	 * @param customer the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException 
	 */
	@ApiMethod(name = "updateCustomer")
	public Customer updateCustomer(Customer customer) throws NotFoundException {
		Customer record = findRecord(customer.getCustomerId());
		if (record == null) {
			throw new NotFoundException("Customer does not exist");
		}
		ofy().save().entities(customer).now();
		return customer;
	}

	/**
	 * This method removes the entity with primary key id.
	 * It uses HTTP DELETE method.
	 *
	 * @param id the primary key of the entity to be deleted.
	 * @throws NotFoundException 
	 */
	@ApiMethod(name = "removeCustomer")
	public void removeCustomer(@Named("id") Long id) throws NotFoundException  {
		Customer record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Customer [ " + id
						+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}
 
	private Customer findRecord(Long id) {
		return ofy().load().type(Customer.class).id(id).now();
	}

}
