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
import com.google.gson.JsonPrimitive;
import com.googlecode.objectify.cmd.Query;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.ActivationDTO;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.Settings;
import com.sp.fanikiwa.entity.Userprofile;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.PasswordHash;
import com.sp.utils.SessionIdentifierGenerator;
import com.sp.utils.TokenUtil;

import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.inject.Named;

@Api(name = "userprofileendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class UserprofileEndpoint {

	public UserprofileEndpoint() {

	}

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listUserprofile")
	public CollectionResponse<Userprofile> listUserprofile(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<Userprofile> query = ofy().load().type(Userprofile.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<Userprofile> records = new ArrayList<Userprofile>();
		QueryResultIterator<Userprofile> iterator = query.iterator();
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
		return CollectionResponse.<Userprofile> builder().setItems(records)
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
	@ApiMethod(name = "getUserprofileByUserID")
	public Userprofile getUserprofileByUserID(@Named("id") String id) {
		return findRecord(id);
	}

	@ApiMethod(name = "retrieveUser")
	public RequestResult retrieveUser(@Named("id") String id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Userprofile user = findRecord(id);
			if (user == null) {
				throw new NotFoundException("User [ " + id
						+ " ]  does not exist");
			}
			re.setClientToken(user);
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
	 * This method is used for updating an existing entity. If the entity does
	 * not exist in the datastore, an exception is thrown. It uses HTTP PUT
	 * method.
	 *
	 * @param Userprofile
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateUserprofile")
	public RequestResult updateUserprofile(Userprofile Userprofile) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Userprofile user = findRecord(Userprofile.getUserId());
			if (user == null) {
				throw new NotFoundException("User does not exist");
			}
			ofy().save().entities(Userprofile).now();
			re.setSuccess(true);
			re.setResultMessage("User Updated.<br/>Id = " + user.getUserId());
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
	@ApiMethod(name = "removeUserprofile")
	public RequestResult removeUserprofile(@Named("id") String id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Userprofile user = findRecord(id);
			if (user == null) {
				throw new NotFoundException("User [ " + id
						+ " ]  does not exist");
			}
			ofy().delete().entity(user).now();
			re.setResultMessage("Setting Removed");
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
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param Userprofile
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertUserprofile")
	public Userprofile insertUserprofile(Userprofile userprofile)
			throws NotFoundException, ConflictException {
		if (userprofile.getUserId() != null) {
			if (findRecord(userprofile.getUserId().toLowerCase()) != null) {
				throw new ConflictException("User already exists");
			}
		}
		userprofile.setUserId(userprofile.getUserId().toLowerCase()); // set all
																		// email
																		// ids
																		// to
																		// lower
		ofy().save().entities(userprofile).now();
		return userprofile;
	}

	@ApiMethod(name = "createUserprofile")
	public RequestResult createUserprofile(Userprofile userprofile) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (userprofile.getUserId() != null) {
				if (findRecord(userprofile.getUserId()) != null) {
					throw new ConflictException("User["
							+ userprofile.getUserId() + "] already exists");
				}
			}
			ofy().save().entities(userprofile).now();
			re.setResultMessage("User Created.<br/>Id = "
					+ userprofile.getUserId());
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

	@ApiMethod(name = "activate")
	public RequestResult activate(ActivationDTO activateDTO) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		activateDTO.setActivatedDate(new Date());

		try {
			Userprofile user = null;
			user = findRecord(activateDTO.getEmail());

			if (user == null) {
				re.setSuccess(false);
				re.setResultMessage("User[" + activateDTO.getEmail()
						+ "] does not exist");
				return re;
			}

			if (user.getToken() == null) {
				re.setSuccess(false);
				re.setResultMessage("User token not valid: Token is null ");
				return re;
			}

			if (DateExtension.DateExpired(user.getActivationTokenExpiryDate(),
					new Date())) {
				re.setSuccess(false);
				re.setResultMessage("User token not valid: Expired; Please regenerate new token and use it within "
						+ Config.GetInt("TOKENEXPIRYDURATION", 30)
						+ " minutes ");
				return re;
			}

			if (!user.getToken().equals(activateDTO.getToken())) {
				re.setSuccess(false);
				re.setResultMessage("User token not valid: Illegal token used ");
				return re;
			} else {
				user.setActivatedDate(activateDTO.getActivatedDate());
				user.setActivationMethod(activateDTO.getActivationMethod());
				user.setStatus("A"); // Activated
				ofy().save().entities(user).now();

				// update member
				MemberEndpoint mep = new MemberEndpoint();
				RequestResult re2 = mep.activate(activateDTO);

				if (re2.isSuccess()) {
					re.setSuccess(true);
					re.setResultMessage("User successfuly activated ");
					return re;
				} else
					return re;
			}

		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.toString());
		}
		return re;
	}

	@ApiMethod(name = "requestToken")
	public RequestResult requestToken(ActivationDTO activateDTO) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Userprofile user = null;
			user = findRecord(activateDTO.getEmail());
			if (user == null) {
				re.setSuccess(false);
				re.setResultMessage("User cannot be null");
				return re;
			}
			// set activation token
			TokenUtil.SetUserToken(user, new Date());

			ofy().save().entities(user).now();
			TokenUtil.SendToken(activateDTO.getActivationMethod(), user,
					user.getToken());

			re.setSuccess(true);
			re.setResultMessage("Token Sent to " + user.getUserId()
					+ "<br/>Check your email.");
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			return re;
		}

	}

	@ApiMethod(name = "changePassword")
	public RequestResult changePassword(@Named("userId") String userId,
			@Named("pwd") String pwd) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			Userprofile user = null;
			user = findRecord(userId);
			user.setPwd(pwd);

			updateUserprofile(user);
			re.setSuccess(true);
			re.setResultMessage("Password Changed.");
			return re;
		} catch (Exception e) {
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
			return re;
		}
	}

	@ApiMethod(name = "login")
	public RequestResult Login(@Named("userId") String userID,
			@Named("pwd") String PWD) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		String pwd = PWD.trim();
		String userId = userID.trim().toLowerCase();
		Userprofile user = null;
		user = findRecord(userId);
		if (user == null) {
			re.setSuccess(false);
			re.setResultMessage("User with email [ " + userId
					+ " ] does not exist!");
			return re;
		}
		boolean authenticated = AuthenticateUser(user, pwd);
		boolean activated = Activated(user);
		if (authenticated && activated) {
			re.setSuccess(true);
			SimpleDateFormat sdf = new SimpleDateFormat("dd-MMM-yyyy");
			re.setResultMessage("created date:"
					+ sdf.format(user.getCreateDate()));
			re.setClientToken(user);
			return re;
		} else if (authenticated) {
			re.setClientToken("authenticated");
			re.setSuccess(false);
			re.setResultMessage("Authenticated but not authorized.");
			return re;
		} else if (activated) {
			re.setClientToken("activated");
			re.setSuccess(false);
			re.setResultMessage("Check your login credentials.");
			return re;
		}
		return re;
	}

	public RequestResult UserExists(@Named("userId") String userId) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");

		Userprofile user = null;
		user = findRecord(userId);
		if (user == null) {
			re.setSuccess(false);
			re.setResultMessage("User with email [ " + userId
					+ " ] does not exist!");
			return re;
		}
		re.setSuccess(true);
		re.setClientToken(user);
		return re;
	}

	private boolean AuthenticateUser(Userprofile user, String pwd) {
		if (user == null)
			return false;
		try {
			return PasswordHash.validatePassword(pwd, user.getPwd());
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			return false;
		}

	}

	private boolean Activated(Userprofile user) {
		if (user == null)
			return false;

		// check active
		if (!user.getStatus().equals("A"))
			return false;

		// check other conditions

		return true;

	}

	private Userprofile findRecord(String id) {
		return ofy().load().type(Userprofile.class).id(id).now();
	}

	public Userprofile LoginByPhone(@Named("telephone") String telephone,
			@Named("pwd") String pwd) {
		// TODO Auto-generated method stub
		Userprofile user = ofy().load().type(Userprofile.class)
				.filter("telephone", telephone).first().now();

		if (AuthenticateUser(user, pwd)) {
			return user;
		} else
			return null;
	}

}
