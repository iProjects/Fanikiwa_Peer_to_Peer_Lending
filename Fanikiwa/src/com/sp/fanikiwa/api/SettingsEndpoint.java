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
import com.googlecode.objectify.VoidWork;
import com.googlecode.objectify.Work;
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Settings;

import java.util.ArrayList;
import java.util.List;

import javax.inject.Named;

@Api(name = "settingsendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class SettingsEndpoint {
	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listSettings")
	public CollectionResponse<Settings> listSettings(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Settings> query = ofy().load().type(Settings.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Settings> records = new ArrayList<Settings>();
		QueryResultIterator<Settings> iterator = query.iterator();
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
		return CollectionResponse.<Settings> builder().setItems(records)
				.setNextPageToken(cursorString).build();
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Settings
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertSettings")
	public RequestResult insertSettings(Settings Settings) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (Settings.getProperty() != null) {
				if (findRecord(Settings.getProperty()) != null) {
					throw new ConflictException("Setting already exists");
				}
			}
			Settings.setProperty(Settings.getProperty().trim());
			Settings.setValue(Settings.getValue().trim());
			Settings.setGroupName(Settings.getGroupName().trim());
			
			ofy().save().entities(Settings).now();
			re.setSuccess(true);
			re.setResultMessage("Setting Created.<br/>Id = "
					+ Settings.getProperty());
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "getSettingsByKey")
	public Settings getSettingsByKey(@Named("Key") String property) {
		return findRecord(property);

	}

	@ApiMethod(name = "retrieveSettingsByKey")
	public RequestResult retrieveSettingsByKey(@Named("key") String property) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Successful");
		try {
			Settings setting = findRecord(property);
			if (setting == null) {
				throw new NotFoundException("Setting does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(setting);
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
	 * @param Settings
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateSettings")
	public RequestResult updateSettings(Settings Settings) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Settings record = findRecord(Settings.getProperty());
			if (record == null) {
				throw new NotFoundException("Setting does not exist");
			}
			
			Settings.setProperty(Settings.getProperty().trim());
			Settings.setValue(Settings.getValue().trim());
			Settings.setGroupName(Settings.getGroupName().trim());
			
			ofy().save().entities(Settings).now();
			re.setSuccess(true);
			re.setResultMessage("Setting Updated.<br/>Id = "
					+ Settings.getProperty());
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
	@ApiMethod(name = "removeSettings")
	public RequestResult removeSettings(@Named("id") String id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Settings setting = findRecord(id);
			if (setting == null) {
				throw new NotFoundException("Setting does not exist");
			}
			ofy().delete().entity(setting).now();
			re.setSuccess(true);
			re.setResultMessage("Setting Removed");
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	private Settings findRecord(String id) {
		return ofy().load().type(Settings.class).id(id).now();
	}
}
