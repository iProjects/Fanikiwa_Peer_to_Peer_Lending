package com.sp.fanikiwa.business.MakeOffer;

import com.sp.fanikiwa.Enums.OffereeIdType;

public class OffereeToken {
    private OffereeIdType OffereeType;
    public OffereeToken(OffereeIdType offereeType, String value) {
		super();
		OffereeType = offereeType;
		Value = value;
	}
	private String Value;
	public OffereeIdType getOffereeType() {
		return OffereeType;
	}
	public void setOffereeType(OffereeIdType offereeType) {
		OffereeType = offereeType;
	}
	public String getValue() {
		return Value;
	}
	public void setValue(String value) {
		Value = value;
	}
}
