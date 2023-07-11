package com.sp.fanikiwa.business.MakeOffer;

import com.sp.fanikiwa.Enums.OffereeIdType;

public abstract class Offeree {
	protected Offeree(){};

    private String IconSource;
    private OffereeIdType OffereeType;
    private String Value;
	public String getIconSource() {
		return IconSource;
	}
	public void setIconSource(String iconSource) {
		IconSource = iconSource;
	}
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
