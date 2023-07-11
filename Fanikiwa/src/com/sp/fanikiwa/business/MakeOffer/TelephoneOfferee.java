package com.sp.fanikiwa.business.MakeOffer;

import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.entity.Member;

public class TelephoneOfferee extends Offeree {

	private String Telephone;
	public TelephoneOfferee(String telephone) {
		super();
		setTelephone(telephone);
	}
	private Member Member = null;
	public Member getMember() {
		return Member;
	}
	public String getTelephone() {
		return Telephone;
	}
	public void setTelephone(String telephone) {
		this.Telephone = telephone;
		MemberEndpoint mep = new MemberEndpoint();
		this.Member = mep.txnlessGetMemberByEmail(telephone);
	}
	
}
