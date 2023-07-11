package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import java.util.ArrayList;
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
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.entity.WithdrawalMessage;
import com.sp.fanikiwa.entity.RequestResult;

@Api(name = "withdrawalmessageendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class WithdrawalMessageEndpoint {

	/**
	 * Return a collection of quotes
	 *
	 * @param count
	 *            The number of quotes
	 * @return a list of Quotes
	 */

	@ApiMethod(name = "listWithdrawalMessage")
	public CollectionResponse<WithdrawalMessage> listWithdrawalMessage(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<WithdrawalMessage> query = ofy().load().type(
				WithdrawalMessage.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<WithdrawalMessage> records = new ArrayList<WithdrawalMessage>();
		QueryResultIterator<WithdrawalMessage> iterator = query.iterator();
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
		return CollectionResponse.<WithdrawalMessage> builder()
				.setItems(records).setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new <code>WithdrawalMessage</code> object.
	 * 
	 * @param WithdrawalMessage
	 *            The object to be added.
	 * @return The object to be added.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertWithdrawalMessage")
	public WithdrawalMessage insertWithdrawalMessage(WithdrawalMessage wm)
			throws ConflictException {
		if (wm.getId() != null) {
			if (findRecord(wm.getId()) != null) {
				throw new ConflictException("Message already exists");
			}
		}
		ofy().save().entity(wm).now(); // .now() is synchronous and generated id
										// on wm

		return wm;
	}

	@ApiMethod(name = "getWithdrawalMessage")
	public WithdrawalMessage getWithdrawalMessage(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "retrieveWithdrawalMessage")
	public RequestResult retrieveWithdrawalMessage(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			WithdrawalMessage accountType = findRecord(id);
			if (accountType == null) {
				throw new NotFoundException("Message [ " + id
						+ " ]  does not exist");
			}
			re.setSuccess(true);
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
	 * This updates an existing <code>WithdrawalMessage</code> object.
	 * 
	 * @param WithdrawalMessage
	 *            The object to be added.
	 * @return The object to be updated.
	 */
	@ApiMethod(name = "updateWithdrawalMessage")
	public RequestResult updateWithdrawalMessage(
			WithdrawalMessage WithdrawalMessage) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (findRecord(WithdrawalMessage.getId()) == null) {
				throw new NotFoundException("Messsage does not exist");
			}
			ofy().save().entity(WithdrawalMessage).now();
			re.setResultMessage("Account Type Updated.<br/>Id = "
					+ WithdrawalMessage.getId());
			re.setSuccess(true);
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
	 * This deletes an existing <code>WithdrawalMessage</code> object.
	 * 
	 * @param id
	 *            The id of the object to be deleted.
	 */
	@ApiMethod(name = "removeWithdrawalMessage")
	public RequestResult removeWithdrawalMessage(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			WithdrawalMessage accounttype = findRecord(id);
			if (accounttype == null) {
				throw new NotFoundException("Message [ " + id
						+ " ]  does not exist");
			}
			ofy().delete().entity(accounttype).now();
			re.setResultMessage("Message Deleted.");
			re.setSuccess(true);
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	// Private method to retrieve a <code>WithdrawalMessage</code> record
	private WithdrawalMessage findRecord(Long id) {
		return ofy().load().type(WithdrawalMessage.class).id(id).now();
	}

}
