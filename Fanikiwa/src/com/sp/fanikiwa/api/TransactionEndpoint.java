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

import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.StatementModel;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.utils.GLUtil;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Named;

@Api(name = "transactionendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class TransactionEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listTransaction")
	public CollectionResponse<Transaction> listTransaction(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Transaction> query = ofy().load().type(Transaction.class);
		return listTransactionByQuery(query, cursorString, count);
	}

	public CollectionResponse<Transaction> GetStatement(
			@Named("sdate") Date sdate, @Named("edate") Date edate,
			Account account, @Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Transaction> query = ofy().load().type(Transaction.class)
				.order("postDate").filter("postDate >=", sdate)
				.filter("postDate <=", edate).filter("account", account);
		return listTransactionByQuery(query, cursorString, count);

	}

	public CollectionResponse<Transaction> GetTransactionsBeforeDate(
			@Named("sdate") Date sdate, Account account,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Transaction> query = ofy().load().type(Transaction.class)
				.order("postDate").filter("postDate >", sdate)
				.filter("account", account);
		return listTransactionByQuery(query, cursorString, count);

	}

	public CollectionResponse<Transaction> SelectByAccountDateRange(
			@Named("sdate") Date sdate, Account account,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Transaction> query = ofy().load().type(Transaction.class)
				.order("postDate").filter("postDate >", sdate)
				.filter("account", account);
		return listTransactionByQuery(query, cursorString, count);

	}

	public CollectionResponse<Transaction> GetMiniStatement(
			@Named("accountId") Long accountId,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Account account = GLUtil.GetAccount(accountId);
		Query<Transaction> query = ofy().load().type(Transaction.class)
				.order("-postDate").filter("account", account);
		return listTransactionByQuery(query, cursorString, count);
	}

	private CollectionResponse<Transaction> listTransactionByQuery(
			Query<Transaction> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Transaction> records = new ArrayList<Transaction>();
		QueryResultIterator<Transaction> iterator = query.iterator();
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
		return CollectionResponse.<Transaction> builder().setItems(records)
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
	@ApiMethod(name = "getTransaction")
	public Transaction getTransaction(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Transaction
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertTransaction")
	public Transaction insertTransaction(Transaction Transaction)
			throws ConflictException {
		if (Transaction.getTransactionID() != null) {
			if (findRecord(Transaction.getTransactionID()) != null) {
				throw new ConflictException("Transaction already exists");
			}
		}
		ofy().save().entities(Transaction).now();
		return Transaction;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Transaction
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateTransaction")
	public Transaction updateTransaction(Transaction Transaction)
			throws NotFoundException {
		Transaction record = findRecord(Transaction.getTransactionID());
		if (record == null) {
			throw new NotFoundException("Transaction does not exist");
		}
		ofy().save().entities(Transaction).now();
		return Transaction;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeTransaction")
	public void removeTransaction(@Named("id") Long id)
			throws NotFoundException {
		Transaction record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Transaction [ " + id
					+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private Transaction findRecord(Long id) {
		return ofy().load().type(Transaction.class).id(id).now();
	}

}
