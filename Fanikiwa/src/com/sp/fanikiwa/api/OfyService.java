package com.sp.fanikiwa.api;

import com.googlecode.objectify.Objectify;
import com.googlecode.objectify.ObjectifyFactory;
import com.googlecode.objectify.ObjectifyService;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.AccountType;
import com.sp.fanikiwa.entity.Coa;
import com.sp.fanikiwa.entity.Coadet;
import com.sp.fanikiwa.entity.Customer;
import com.sp.fanikiwa.entity.Diaryprogramcontrol;
import com.sp.fanikiwa.entity.Lendinggroup;
import com.sp.fanikiwa.entity.Lendinggroupmember;
import com.sp.fanikiwa.entity.Loan;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.MpesaIPNMessage;
import com.sp.fanikiwa.entity.MpesaTestIPNMessage;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.OfferReceipient;
import com.sp.fanikiwa.entity.Offergroup;
import com.sp.fanikiwa.entity.Offergroupemailaddress;
import com.sp.fanikiwa.entity.Organization;
import com.sp.fanikiwa.entity.Quote;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.Settings;
import com.sp.fanikiwa.entity.TieredDet;
import com.sp.fanikiwa.entity.Tieredtable;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.StatementModel;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.fanikiwa.entity.Userprofile;
import com.sp.fanikiwa.entity.ValueDatedTransaction;
import com.sp.fanikiwa.entity.WithdrawalMessage;

/**
 * Objectify service wrapper so we can statically register our persistence
 * classes More on Objectify here :
 * https://code.google.com/p/objectify-appengine/
 *
 */
public class OfyService {

	static {
		ObjectifyService.register(Quote.class);
		ObjectifyService.register(Organization.class);
		ObjectifyService.register(Customer.class);
		ObjectifyService.register(Transaction.class);
		ObjectifyService.register(TransactionType.class);
		ObjectifyService.register(ValueDatedTransaction.class);
		ObjectifyService.register(Account.class);
		ObjectifyService.register(AccountType.class);
		ObjectifyService.register(Member.class);
		ObjectifyService.register(Coadet.class);
		ObjectifyService.register(Coa.class);
		ObjectifyService.register(Offer.class);
		ObjectifyService.register(OfferReceipient.class);
		ObjectifyService.register(Offergroupemailaddress.class);
		ObjectifyService.register(Offergroup.class);
		ObjectifyService.register(StatementModel.class);
		ObjectifyService.register(Userprofile.class);
		ObjectifyService.register(Diaryprogramcontrol.class);
		ObjectifyService.register(Loan.class);
		ObjectifyService.register(Settings.class);
		ObjectifyService.register(Tieredtable.class);
		ObjectifyService.register(TieredDet.class);
		ObjectifyService.register(STO.class);
		ObjectifyService.register(MpesaIPNMessage.class);
		ObjectifyService.register(MpesaTestIPNMessage.class);
		ObjectifyService.register(Lendinggroupmember.class);
		ObjectifyService.register(Lendinggroup.class);
		ObjectifyService.register(WithdrawalMessage.class);
	}

	public static Objectify ofy() {
		return ObjectifyService.ofy();
	}

	public static ObjectifyFactory factory() {
		return ObjectifyService.factory();
	}
}