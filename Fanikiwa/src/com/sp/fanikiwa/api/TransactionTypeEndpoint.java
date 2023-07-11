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
import com.sp.fanikiwa.entity.AccountDTO;
import com.sp.fanikiwa.entity.AccountType;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.CustomerDTO;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.fanikiwa.entity.Userprofile;
import com.sp.utils.GLUtil;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.logging.Logger;

import javax.inject.Named;

@Api(name = "transactiontypeendpoint", namespace = @ApiNamespace(ownerDomain = "sp.com", ownerName = "sp.com", packagePath = "fanikiwa.entity"))
public class TransactionTypeEndpoint {

	public TransactionTypeEndpoint() {

	}

	/**
	 * This method lists all the entities inserted in datastore. It uses HTTP
	 * GET method and paging support.
	 *
	 * @return A CollectionResponse class containing the list of all entities
	 *         persisted and a cursor to the next page.
	 */
	@SuppressWarnings({ "unchecked", "unused" })
	@ApiMethod(name = "listTransactionType")
	public CollectionResponse<TransactionType> listTransactionType(
			@Nullable @Named("cursor") String cursorString,
			@Nullable @Named("count") Integer count) {

		Query<TransactionType> query = ofy().load().type(TransactionType.class);
		if (count != null)
			query.limit(count);
		if (cursorString != null && cursorString != "") {
			query = query.startAt(Cursor.fromWebSafeString(cursorString));
		}

		List<TransactionType> records = new ArrayList<TransactionType>();
		QueryResultIterator<TransactionType> iterator = query.iterator();
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
		return CollectionResponse.<TransactionType> builder().setItems(records)
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
	@ApiMethod(name = "retrieveTransactionType")
	public RequestResult retrieveTransactionType(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			TransactionType transactionType = findRecord(id);
			if (transactionType == null) {
				throw new NotFoundException("TransactionType [ " + id
						+ " ] does not exist");
			}
			re.setSuccess(true);
			re.setClientToken(transactionType);
			return re;
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			re.setSuccess(false);
			re.setResultMessage(e.getMessage().toString());
		}
		return re;
	}

	@ApiMethod(name = "getTransactionType")
	public TransactionType getTransactionType(@Named("id") Long id) {
		return findRecord(id);
	}

	/**
	 * This inserts a new entity into App Engine datastore. If the entity
	 * already exists in the datastore, an exception is thrown. It uses HTTP
	 * POST method.
	 *
	 * @param TransactionType
	 *            the entity to be inserted.
	 * @return The inserted entity.
	 * @throws ConflictException
	 */
	@ApiMethod(name = "insertTransactionType")
	public RequestResult insertTransactionType(TransactionType transactionType) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			if (transactionType.getTransactionTypeID() != null) {
				if (findRecord(transactionType.getTransactionTypeID()) != null) {
					throw new ConflictException(
							"TransactionType already exists");
				}
			}
			if (transactionType.getShortCode() != null) {
				if (findRecord(transactionType.getShortCode()) != null) {
					throw new ConflictException("Short Code already exists");
				}
			}
			RequestResult validator = ValidateTransactionType(transactionType);
			if (!validator.isSuccess()) {
				return validator;
			}
			ofy().save().entities(transactionType).now();
			re.setResultMessage("Transaction Type Created.<br/>Id = "
					+ transactionType.getTransactionTypeID());
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

	private RequestResult ValidateTransactionType(
			TransactionType transactionType) {
		RequestResult re = new RequestResult();
		re.setSuccess(true);
		re.setResultMessage("Successful");

		try {
			if (transactionType.getSuspenseDrAccount() != null) {
				if (GLUtil
						.AccountExists(transactionType.getSuspenseDrAccount())) {
					throw new NotFoundException(
							"Suspense Debit Account does not exist");
				}
			}
			if (transactionType.getSuspenseCrAccount() != null) {
				if (GLUtil
						.AccountExists(transactionType.getSuspenseCrAccount())) {
					throw new NotFoundException(
							"Suspense Credit Account does not exist");
				}
			}
			if (transactionType.getCommissionDrAccount() != null) {
				if (GLUtil.AccountExists(transactionType
						.getCommissionDrAccount())) {
					throw new NotFoundException(
							"Commission Debit Account does not exist");
				}
			}
			if (transactionType.getCommissionCrAccount() != null) {
				if (GLUtil.AccountExists(transactionType
						.getCommissionCrAccount())) {
					throw new NotFoundException(
							"Commission Credit Account does not exist");
				}
			}
			if (transactionType.getDefaultMainAccount() != null) {
				if (GLUtil.AccountExists(transactionType
						.getDefaultMainAccount())) {
					throw new NotFoundException(
							"Default Main Account does not exist");
				}
			}
			if (transactionType.getDefaultContraAccount() != null) {
				if (GLUtil.AccountExists(transactionType
						.getDefaultContraAccount())) {
					throw new NotFoundException(
							"Default Contra Account does not exist");
				}
			}

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
	 * @param TransactionType
	 *            the entity to be updated.
	 * @return The updated entity.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "updateTransactionType")
	public RequestResult updateTransactionType(TransactionType transactionType) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			TransactionType transactionTypeexists = findRecord(transactionType
					.getTransactionTypeID());
			if (transactionTypeexists == null) {
				throw new NotFoundException("TransactionType does not exist");
			}
			transactionTypeexists = PopulateTransactionTypeForUpdate(transactionType);
			RequestResult validator = ValidateTransactionType(transactionTypeexists);
			if (!validator.isSuccess()) {
				return validator;
			}
			ofy().save().entities(transactionType).now();
			re.setResultMessage("Transaction Type Updated.<br/>Id = "
					+ transactionType.getTransactionTypeID());
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

	private TransactionType PopulateTransactionTypeForUpdate(
			TransactionType txnType) {

		TransactionType transactionType = new TransactionType();

		transactionType.setTransactionTypeID(transactionType
				.getTransactionTypeID());
		transactionType.setAbsolute(txnType.getAbsolute());
		transactionType.setAmountExpression(txnType.getAmountExpression());
		transactionType.setCanSuspend(txnType.getCanSuspend());
		transactionType.setChargeCommission(txnType.getChargeCommission());
		transactionType.setChargeCommissionToTransaction(txnType
				.getChargeCommissionToTransaction());
		transactionType.setChargeWho(txnType.getChargeWho());
		transactionType.setCommComputationMethod(txnType
				.getCommComputationMethod());
		transactionType.setCommissionAmount(txnType.getCommissionAmount());
		transactionType.setCommissionAmountExpression(txnType
				.getCommissionAmountExpression());
		transactionType.setCommissionContraNarrative(txnType
				.getCommissionContraNarrative());
		transactionType
				.setCommissionCrAccount(txnType.getCommissionCrAccount());
		transactionType
				.setCommissionDrAccount(txnType.getCommissionDrAccount());
		transactionType.setCommissionDrAnotherAccount(txnType
				.getCommissionDrAnotherAccount());
		transactionType.setCommissionMainNarrative(txnType
				.getCommissionMainNarrative());
		transactionType.setCommissionNarrativeFlag(txnType
				.getCommissionNarrativeFlag());
		transactionType.setCommissionTransactionType(txnType
				.getCommissionTransactionType());
		transactionType.setCrCommCalcMethod(txnType.getCrCommCalcMethod());
		transactionType.setDebitCredit(txnType.getDebitCredit());
		transactionType.setDefaultAmount(txnType.getDefaultAmount());
		transactionType.setDefaultContraAccount(txnType
				.getDefaultContraAccount());
		transactionType.setDefaultContraNarrative(txnType
				.getDefaultContraNarrative());
		transactionType.setDefaultMainAccount(txnType.getDefaultMainAccount());
		transactionType.setDefaultMainNarrative(txnType
				.getDefaultMainNarrative());
		transactionType.setDescription(txnType.getDescription());
		transactionType.setDialogFlag(txnType.getDialogFlag());
		transactionType.setDrCommCalcMethod(txnType.getDrCommCalcMethod());
		transactionType.setForcePost(txnType.getForcePost());
		transactionType.setNarrativeFlag(txnType.getNarrativeFlag());
		transactionType.setShortCode(txnType.getShortCode());
		transactionType.setStatFlag(txnType.getStatFlag());
		transactionType.setSuspenseCrAccount(txnType.getSuspenseCrAccount());
		transactionType.setSuspenseDrAccount(txnType.getSuspenseDrAccount());
		transactionType.setTieredTableId(txnType.getTieredTableId());
		transactionType.setTxnClass(txnType.getTxnClass());
		transactionType.setTxnTypeView(txnType.getTxnTypeView());
		transactionType.setValueDateOffset(txnType.getValueDateOffset());

		return transactionType;
	}

	/**
	 * This method removes the entity with primary key id. It uses HTTP DELETE
	 * method.
	 *
	 * @param id
	 *            the primary key of the entity to be deleted.
	 * @throws NotFoundException
	 */
	@ApiMethod(name = "removeTransactionType")
	public RequestResult removeTransactionType(@Named("id") Long id) {
		RequestResult re = new RequestResult();
		re.setSuccess(false);
		re.setResultMessage("Not Successful");
		try {
			TransactionType transactiontype = findRecord(id);
			if (transactiontype == null) {
				throw new NotFoundException("TransactionType [ " + id
						+ " ] does not exist");
			}
			ofy().delete().entity(transactiontype).now();
			re.setResultMessage("Transaction Type Removed");
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

	private TransactionType findRecord(Long id) {
		return ofy().load().type(TransactionType.class).id(id).now();
	}

	private TransactionType findRecord(String shortcode) {
		return ofy().load().type(TransactionType.class)
				.filter("shortCode", shortcode).first().now();
	}

}
