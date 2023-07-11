package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

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
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferModel;
import com.sp.fanikiwa.entity.OfferReceipient;
import com.sp.fanikiwa.Enums.OfferStatus;
import com.sp.fanikiwa.entity.TransactionType;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.inject.Named;

@Api(name = "stoendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class STOEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listSTO")
	public CollectionResponse<STO> listSTO(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<STO> query = ofy().load().type(STO.class);
		return getSTOByQuery(query, cursorString, count);

	}

	@ApiMethod(name = "SelectSTOByDateFrom")
	public CollectionResponse<STO> SelectSTOByDateFrom(@Named("date") Date date) {
		Query<STO> query = ofy().load().type(STO.class)
				.filter("startDate <", date);
		return getSTOByQuery(query, null, null);

	}

	public CollectionResponse<STO> getSTOByLoanId(@Named("loanid") Long loanid) {
		Query<STO> query = ofy().load().type(STO.class)
				.filter("loanId", loanid);
		return getSTOByQuery(query, null, null);

	}

	public RequestResult deleteSTOByLoanId(@Named("loanid") Long loanid) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		for (STO s : getSTOByLoanId(loanid).getItems()) {
			try {
				this.removeSTO(s.getId());
			} catch (NotFoundException e) {
				re.setSuccess(false);
				re.setResultMessage("Not Successful: " + e.getMessage());
				return re;
			}
		}
		re.setSuccess(true);
		re.setResultMessage("Successful");
		return re;

	}

	private CollectionResponse<STO> getSTOByQuery(Query<STO> query,
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<STO> records = new ArrayList<STO>();
		QueryResultIterator<STO> iterator = query.iterator();
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
		return CollectionResponse.<STO> builder().setItems(records)
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
	@ApiMethod(name = "getSTOByID")
	public STO getSTOByID(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "retrieveSTO")
	public RequestResult retrieveSTO(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			STO sto = findRecord(id);
			if (sto == null) {
				throw new NotFoundException("STO does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(sto);
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
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param STO
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateSTO")
	public STO updateSTO(STO STO) throws NotFoundException {
		STO record = findRecord(STO.getId());
		if (record == null) {
			throw new NotFoundException("STO does not exist");
		}
		ofy().save().entities(STO).now();
		return STO;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeSTO")
	public void removeSTO(@Named("id") Long id) throws NotFoundException {
		STO record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("STO [ " + id + " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	private STO findRecord(Long id) {
		return ofy().load().type(STO.class).id(id).now();
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param STO
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertSTO")
	public STO insertSTO(STO sTO) throws ConflictException {
		if (sTO.getId() != null) {
			if (findRecord(sTO.getId()) != null) {
				throw new ConflictException("STO already exists");
			}
		}
		ofy().save().entities(sTO).now();
		return sTO;
	}

}
