package com.sp.utils;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.Enums.OfferStatus;
import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.api.OfferEndpoint;
import com.sp.fanikiwa.api.WithdrawalMessageEndpoint;
import com.sp.fanikiwa.entity.Member;
import com.sp.fanikiwa.entity.Offer;
import com.sp.fanikiwa.entity.WithdrawalMessage;

public class PeerLendingUtil {
	
	public static Member GetMember(Long id)
	{
		MemberEndpoint mep = new MemberEndpoint();
		return mep.getMemberByID(id);
	}

	public static Member GetMember(String email) {
		MemberEndpoint mep = new MemberEndpoint();
		return mep.GetMemberByEmail(email);
	}
	
	public static void SetOfferStatus(Offer offer, OfferStatus status)
			throws NotFoundException {
		OfferEndpoint oep = new OfferEndpoint();
		offer.setStatus(status.toString());
		oep.updateOffer(offer);
	}
	public static void SetWithdrawalStatus(WithdrawalMessage wm, String status)
			throws NotFoundException {
		SetWithdrawalStatus( wm,  status, "");
	}
	
	public static void SetWithdrawalStatus(WithdrawalMessage wm, String status, String info)
			throws NotFoundException {
		WithdrawalMessageEndpoint oep = new WithdrawalMessageEndpoint();
		wm.setStatus(status);
		if(!StringExtension.isNullOrEmpty(info)) wm.setRemarks(info);
		oep.updateWithdrawalMessage(wm);
	}
	public static Offer GetOffer(Long Id)
	{
		OfferEndpoint oep = new OfferEndpoint();
		return oep.getOfferByID(Id);
	}

}
