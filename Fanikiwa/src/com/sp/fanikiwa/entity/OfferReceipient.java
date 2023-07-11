package com.sp.fanikiwa.entity;

 
import com.googlecode.objectify.Ref;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;

@Entity
public class OfferReceipient {

	@Id
	Long offerReceipientId;

	@Index private Ref<Member> member;
	@Index private Ref<Offer> offer;

	public OfferReceipient() {
	}
	public OfferReceipient(Member m, Offer o) {
		setMember(m);
		setOffer(o);
	}
	
	public Long getOfferReceipientId() {
		return offerReceipientId;
	}

	public void setOfferReceipientId(Long offerReceipientId) {
		this.offerReceipientId = offerReceipientId;
	}

	public Member getMember() {
		return member.get();
	}

	public void setMember(Member member) {
		this.member = Ref.create(member);
	}

	public Offer getOffer() {
		return offer.get();
	}

	public void setOffer(Offer offer) {
		this.offer = Ref.create(offer);
	}



}