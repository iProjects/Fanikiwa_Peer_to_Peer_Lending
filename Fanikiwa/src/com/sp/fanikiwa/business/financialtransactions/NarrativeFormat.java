package com.sp.fanikiwa.business.financialtransactions;

import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.StringExtension;


public class NarrativeFormat
{
  

    TransactionType TType;

    private String DrNarrativeFommatter = "${ShortCode}";
    private String CrNarrativeFommatter = "${ShortCode}";
    private String DrNarrativeCommissionFommatter = "${ShortCode} Comm";
    private String CrNarrativeCommissionFommatter = "${ShortCode} Comm ${DebitAccount}";


    public NarrativeFormat(TransactionType TransactionType) throws NullPointerException
    {
        if (TransactionType == null) throw new NullPointerException("TransactionType cannot be null");
        TType = TransactionType;
            //NarrativeFormatter
        if (!StringExtension.isNullOrEmpty(TType.getDefaultMainNarrative()))
            DrNarrativeFommatter = TType.getDefaultMainNarrative();
        if (!StringExtension.isNullOrEmpty(TType.getDefaultContraNarrative()))
            CrNarrativeFommatter = TType.getDefaultContraNarrative();

        //CommNarrativeFormatter
        if (!StringExtension.isNullOrEmpty(TType.getCommissionMainNarrative())) 
            DrNarrativeCommissionFommatter = TType.getCommissionMainNarrative();
        if (!StringExtension.isNullOrEmpty(TType.getCommissionContraNarrative())) 
            CrNarrativeCommissionFommatter = TType.getCommissionContraNarrative();
    }


	public TransactionType getTType() {
		return TType;
	}


	public void setTType(TransactionType tType) {
		TType = tType;
	}


	public String getDrNarrativeFommatter() {
		return DrNarrativeFommatter;
	}


	public void setDrNarrativeFommatter(String drNarrativeFommatter) {
		DrNarrativeFommatter = drNarrativeFommatter;
	}


	public String getCrNarrativeFommatter() {
		return CrNarrativeFommatter;
	}


	public void setCrNarrativeFommatter(String crNarrativeFommatter) {
		CrNarrativeFommatter = crNarrativeFommatter;
	}


	public String getDrNarrativeCommissionFommatter() {
		return DrNarrativeCommissionFommatter;
	}


	public void setDrNarrativeCommissionFommatter(
			String drNarrativeCommissionFommatter) {
		DrNarrativeCommissionFommatter = drNarrativeCommissionFommatter;
	}


	public String getCrNarrativeCommissionFommatter() {
		return CrNarrativeCommissionFommatter;
	}


	public void setCrNarrativeCommissionFommatter(
			String crNarrativeCommissionFommatter) {
		CrNarrativeCommissionFommatter = crNarrativeCommissionFommatter;
	}
   
}