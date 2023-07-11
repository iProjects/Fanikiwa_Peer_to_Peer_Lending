package com.sp.fanikiwa.api;

import static com.sp.fanikiwa.api.OfyService.ofy;

import com.sp.fanikiwa.entity.Coa;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Settings;
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
import java.util.List;

import javax.inject.Named;

@Api(name = "coaendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class CoaEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listCoa")
	public CollectionResponse<Coa> listCoa(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Coa> query = ofy().load().type(Coa.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Coa> records = new ArrayList<Coa>();
		QueryResultIterator<Coa> iterator = query.iterator();
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
		return CollectionResponse.<Coa> builder().setItems(records)
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
	@ApiMethod(name = "getCoaByID")
	public Coa getCoaByID(@Named("id") Long id) {
		return findRecord(id);
	}

	@ApiMethod(name = "retrieveCoa")
	public RequestResult retrieveCoa(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Coa coa = findRecord(id);
			if (coa == null) {
				throw new NotFoundException("Coa [ " + id
						+ " ] does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(coa);
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
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Coa
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertCoa")
	public RequestResult insertCoa(Coa coa) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Successful");
		try {
			if (coa.getId() != null) {
				if (findRecord(coa.getId()) != null) {
					throw new ConflictException("Coa already exists");
				}
			}
			ofy().save().entities(coa).now();
			re.setSuccess(true);
			re.setResultMessage("Chart Of Account Created.<br/>Id = "
					+ coa.getId());
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
	 * @param Coa
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateCoa")
	public RequestResult updateCoa(Coa Coa) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Coa coa = findRecord(Coa.getId());
			if (coa == null) {
				throw new NotFoundException("Coa does not exist");
			}
			ofy().save().entities(Coa).now();
			re.setSuccess(true);
			re.setResultMessage("Chart Of Account Updated.<br/>Id = "
					+ coa.getId());
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
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeCoa")
	public RequestResult removeCoa(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Coa record = findRecord(id);
			if (record == null) {
				throw new NotFoundException("Coa [ " + id
						+ " ] does not exist");
			}
			ofy().delete().entity(record).now();
			re.setSuccess(true);
			re.setResultMessage("Chart Of Account Removed");
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	private Coa findRecord(Long id) {
		return ofy().load().type(Coa.class).id(id).now();
	}

}
