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

import com.sp.fanikiwa.entity.Organization;

import static com.sp.fanikiwa.api.OfyService.ofy;

import java.util.ArrayList;
import java.util.List;

import javax.inject.Named;

@Api(name = "organizationendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class OrganizationEndpoint {

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listOrganization")
	public CollectionResponse<Organization> listOrganization(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Organization> query = ofy().load().type(Organization.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Organization> records = new ArrayList<Organization>();
		QueryResultIterator<Organization> iterator = query.iterator();
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
		return CollectionResponse.<Organization> builder().setItems(records)
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
	@ApiMethod(name = "getOrganization")
	public Organization getOrganization(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param organization
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 */

	@ApiMethod(name = "insertOrganization")
	public Organization insertOrganization(Organization organization)
			throws ConflictException {
		// If if is not null, then check if it exists. If yes, throw an
		// Exception
		// that it is already present
		if (organization.getOrganizationID() != null) {
			if (findRecord(organization.getOrganizationID()) != null) {
				throw new ConflictException("Organization already exists");
			}
		}
		// Since our @Id field is a Long, Objectify will generate a unique value
		// for us
		// when we use put
		ofy().save().entity(organization).now();
		return organization;
	}

	/**
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param organization
	 *            the entity to be updated.
	 * @return The updated entity.
	 */
	@ApiMethod(name = "updateOrganization")
	public Organization updateOrganization(Organization organization)
			throws NotFoundException {
		if (findRecord(organization.getOrganizationID()) == null) {
			throw new NotFoundException("Organization does not exist");
		}
		ofy().save().entity(organization).now();
		return organization;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 */
	@ApiMethod(name = "removeOrganization")
	public void removeOrganization(@Named("id") Long id)
			throws NotFoundException {
		Organization record = findRecord(id);
		if (record == null) {
			throw new NotFoundException("Organization [ " + id
					+ " ]  does not exist");
		}
		ofy().delete().entity(record).now();
	}

	// Private method to retrieve a <code>Quote</code> record
	private Organization findRecord(Long id) {
		return ofy().load().type(Organization.class).id(id).now();
	}

}
