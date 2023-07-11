package com.sp.fanikiwa.business.MakeOffer;

import com.sp.fanikiwa.api.MemberEndpoint;
import com.sp.fanikiwa.entity.Member;

public class MemberOfferee extends Offeree {
	private Long MemberId;
	private Member Member = null;
	
	public MemberOfferee(Long memberId) {
		setMemberId( memberId);
	}

	public Member getMember() {
		return Member;
	}
	public Long getMemberId() {
		return MemberId;
	}

	public void setMemberId(Long memberId) {
		MemberId = memberId;
		MemberEndpoint mep = new MemberEndpoint();
		this.Member = mep.getMemberByID(memberId);
	}

}
