package com.sp.fanikiwa.entity;

import java.util.Date;
import java.util.List;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;


public class OfferModel {


	public OfferModel() {
		super();
	}

	private Offer offer;
 
	private List<Member> offerees;

	public Offer getOffer() {
		return offer;
	}

	public void setOffer(Offer offer) {
		this.offer = offer;
	}

	public List<Member> getOfferees() {
		return offerees;
	}

	public void setOfferees(List<Member> offerees) {
		this.offerees = offerees;
	}

	


}