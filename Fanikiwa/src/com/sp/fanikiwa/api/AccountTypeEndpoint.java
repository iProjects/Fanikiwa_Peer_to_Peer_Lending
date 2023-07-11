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

import com.sp.fanikiwa.entity.AccountType;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Settings;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Named;

@Api(name = "accounttypeendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class AccountTypeEndpoint {

	/**
	 * Return a collection of quotes
	 *
	 * @param count
	 *            The number of quotes
	 * @return a list of Quotes
	 */

	@ApiMethod(name = "listAccountType")
	public CollectionResponse<AccountType> listAccountType(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<AccountType> query = ofy().load().type(AccountType.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<AccountType> records = new ArrayList<AccountType>();
		QueryResultIterator<AccountType> iterator = query.iterator();
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
		return CollectionResponse.<AccountType> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new <code>AccountType</code> object.
	 * 
	 * @param AccountType
	 *            The object to be added.
	 * @return The object to be added.
	 */
	@ApiMethod(name = "insertAccountType")
	public RequestResult insertAccountType(AccountType AccountType) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (AccountType.getId() != null) {
				if (findRecord(AccountType.getId()) != null) {
					throw new ConflictException("AccountType "
							+ AccountType.getDescription() + "already exists");
				}
			}
			ofy().save().entity(AccountType).now();
			re.setSuccess(true);
			re.setResultMessage("Account Type Created.<br/>Id = "
					+ AccountType.getId());
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "getAccountType")
	public AccountType getAccountType(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "retrieveAccountType")
	public RequestResult retrieveAccountType(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			AccountType accountType = findRecord(id);
			if (accountType == null) {
				throw new NotFoundException("AccountType [ " + id
						+ " ] does not exist");
			}
			re.setSuccess(true);
			re.setResultMessage("Successful");
			re.setClientToken(accountType);
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
	 * This updates an existing <code>AccountType</code> object.
	 * 
	 * @param AccountType
	 *            The object to be added.
	 * @return The object to be updated.
	 */
	@ApiMethod(name = "updateAccountType")
	public RequestResult updateAccountType(AccountType AccountType) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (findRecord(AccountType.getId()) == null) {
				throw new NotFoundException("AccountType does not exist");
			}
			ofy().save().entity(AccountType).now();
			re.setSuccess(true);
			re.setResultMessage("Account Type Updated.<br/>Id = "
					+ AccountType.getId());
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
	 * This deletes an existing <code>AccountType</code> object.
	 * 
	 * @param id
	 *            The id of the object to be deleted.
	 */
	@ApiMethod(name = "removeAccountType")
	public RequestResult removeAccountType(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			AccountType accounttype = findRecord(id);
			if (accounttype == null) {
				throw new NotFoundException("AccountType [ " + id
						+ " ] does not exist");
			}
			ofy().delete().entity(accounttype).now();
			re.setSuccess(true); 
			re.setResultMessage("Account Type Deleted.");
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	// Private method to retrieve a <code>AccountType</code> record
	private AccountType findRecord(Long id) {
		return ofy().load().type(AccountType.class).id(id).now();
	}

}