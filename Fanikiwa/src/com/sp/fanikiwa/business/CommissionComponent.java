package com.sp.fanikiwa.business;

import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

import com.sp.fanikiwa.Enums.*;
import com.sp.fanikiwa.api.TieredDetEndpoint;
import com.sp.fanikiwa.api.TieredtableEndpoint;
import com.sp.fanikiwa.api.TransactionTypeEndpoint;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.TieredDet;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.StringExtension;

public class CommissionComponent {

	// Define embedded classes

	public abstract class CommissionRule {
	}

	public class FlatRateRule extends CommissionRule {
		private int Id;
		private boolean Absolute; // Flat rate rule can be absolute or %
		private double Rate;

		public FlatRateRule(boolean absolute, double rate) {
			Absolute = absolute;
			Rate = rate;
		}

		public FlatRateRule() {

		}

		public int getId() {
			return Id;
		}

		public void setId(int id) {
			Id = id;
		}

		public boolean isAbsolute() {
			return Absolute;
		}

		public void setAbsolute(boolean absolute) {
			Absolute = absolute;
		}

		public double getRate() {
			return Rate;
		}

		public void setRate(double rate) {
			Rate = rate;
		}
	}

	public class TieredRule extends CommissionRule {
		public List<LookupRow> LookupTable;

		public TieredRule() {

		}
	}

	public class LookupRule extends CommissionRule {
		// public LookupRow[] LookupTable;
		public List<LookupRow> LookupTable;

		public LookupRule() {
		}
	}

	public class TieredResult {
		private double commission;
		private double amtInTier;
		private boolean cancontinue;

		public TieredResult(double commission, double amtInTier,
				boolean cancontinue) {
			super();
			this.commission = commission;
			this.amtInTier = amtInTier;
			this.cancontinue = cancontinue;
		}

		public double getCommission() {
			return commission;
		}

		public void setCommission(double commission) {
			this.commission = commission;
		}

		public double getAmtInTier() {
			return amtInTier;
		}

		public void setAmtInTier(double amtInTier) {
			this.amtInTier = amtInTier;
		}

		public boolean isCancontinue() {
			return cancontinue;
		}

		public void setCancontinue(boolean cancontinue) {
			this.cancontinue = cancontinue;
		}

	}

	// / <summary>
	// / Compute commission given a Commission transaction and a commission
	// collection rule
	// / </summary>
	// / <returns>Returns a List<Offer> object.</returns>
	public double ComputeByCommissionRule(double Amount, CommissionRule rule) {
		if (rule == null)
			return 0.00;

		double commision = 0.00;
		double _amount = 0.00;

		if (Amount >= 0) {
			_amount = Amount;
		}
		if (Amount < 0) {
			_amount = Amount * -1;
		}
		if (rule == null) // No rule set
		{
			return 0.00;
		}

		if (rule instanceof FlatRateRule) // return a flat figure
		{
			return ComputeFlat((FlatRateRule) rule, _amount);
		} else if (rule instanceof TieredRule) // commpute commission in various
												// layers/tiers
		{
			return ComputeTiered((TieredRule) rule, _amount);
		} else if (rule instanceof LookupRule) // lookup a commission from a
												// table
		{
			return ComputeLookup((LookupRule) rule, _amount);
		}

		return commision;
	}

	public double ComputeByCommissionRule(STO _sto, CommissionRule rule) {
		if (rule == null)
			return 0.00;

		double commision = 0.00;
		double _amount = 0.00;

		if (_sto.getTotalToPay() >= 0) {
			_amount = _sto.getTotalToPay();
		}
		if (_sto.getTotalToPay() < 0) {
			_amount = _sto.getTotalToPay() * -1;
		}

		if (rule instanceof FlatRateRule) // return a flat figure
		{
			FlatRateRule flat = (FlatRateRule) rule; // cast the rule into flat
														// rate rule

			if (flat.Absolute) {
				commision = flat.Rate;
			} // return a flat rate
			else {
				commision = _amount * flat.Rate;
			} // return a flat figure based on percent %
		} else if (rule instanceof TieredRule) // lookup a commission from a
												// table
		{
			TieredRule tiered = (TieredRule) rule; // cast the rule into Tiered
													// rule

			LookupRow[] terms = (LookupRow[]) tiered.LookupTable.toArray();

			for (int i = 0; i < terms.length; i++) {
				boolean _absolute = terms[i].Absolute;
				double _max = terms[i].Max;
				double _min = terms[i].Min;
				double _rate = terms[i].Rate;

				if (_amount >= _min && _amount <= _max) {
					if (_absolute) {
						commision = _rate;
					} // return a flat rate
					else {
						_rate = _rate / 100;
						commision = _amount * _rate;
					}
				}
			}
		}

		return commision;
	}

	private double ComputeLookup(LookupRule rule, double Amount) {
		LookupRow[] terms = (LookupRow[]) rule.LookupTable.toArray();
		double commission = 0;
		for (int i = 0; i < terms.length; i++) {
			boolean _absolute = terms[i].Absolute;
			double _max = terms[i].Max;
			double _min = terms[i].Min;
			double _rate = terms[i].Rate;

			if (Amount >= _min && Amount <= _max) {
				if (_absolute) {
					commission = _rate;
				} // return a flat rate
				else {
					_rate = _rate / 100;
					commission = Amount * _rate;
				}
			}
		}
		return commission;
	}

	private double ComputeFlat(FlatRateRule flat, double Amount) {
		double commission = 0;
		if (flat.isAbsolute()) {
			commission = flat.getRate();
		} // return a flat rate
		else {
			commission = Amount * flat.getRate();
		} // return a flat figure based on percent %
		return commission;
	}

	private double ComputeTiered(TieredRule rule, double Amount) {
		return ComputeTiered(rule, Amount, 0);
	}

	private double ComputeTiered(TieredRule rule, double Amount, double min) {
		/*
         * 
         */

		// If Amount is less than minimim, dont charge
		if (Amount < min) {
			return 0;
		}

		double commission = 0.0;
		double amtInBracket = 0.00;
		int bracketId = 0;
		boolean cont = true;
		// loop through the lookup computing the neccessary taxes within the
		// bracket
		// while returning the charged amount
		for (LookupRow bracket : rule.LookupTable) {
			++bracketId;
			boolean isLastBracket = (rule.LookupTable.size() == bracketId);
			if (cont) {
				TieredResult res = AmountInBracket(bracket.Absolute, Amount,
						bracket.Min, bracket.Max, bracket.Rate, isLastBracket);
				amtInBracket = res.getAmtInTier();
				commission += res.getCommission();

			} else
				break;

			// keep reducing amt to tax by amt already taxed
			Amount -= amtInBracket;
		}

		return commission;

	}

	private TieredResult AmountInBracket(boolean absolute, double TxnAmount,
			double From, double To, double Rate, boolean isLastBracket) {
		if (isLastBracket) // last bracket
		{
			double amtInTier = TxnAmount;
			double commission = absolute ? Rate : amtInTier * Rate / 100;
			boolean cancontinue = false;
			return new TieredResult(commission, amtInTier, cancontinue);

		} else if (TxnAmount > To) // greator than the bracket
		{
			double amtInTier = (To - From);
			double commission = absolute ? Rate : amtInTier * Rate / 100;
			boolean cancontinue = true;
			return new TieredResult(commission, amtInTier, cancontinue);
		} else {
			double amtInTier = TxnAmount;
			double commission = absolute ? Rate : amtInTier * Rate / 100;
			boolean cancontinue = false;
			return new TieredResult(commission, amtInTier, cancontinue);
		}

	}

	public double ComputeCommissionByTransactionType(double subject,
			TransactionType tt) {
		CommissionRule rule = GetCommissionRuleFromTransactionType(tt);
		return ComputeByCommissionRule(subject, rule);
	}

	public CommissionRule GetCommissionRuleFromTransactionType(
			TransactionType tt) {
		CommissionRule rule = null;

		rule = GetCommissionRule(tt.getTransactionTypeID());
		return rule;
	}

	public CommissionRule GetCommissionRule(Long tt) {
		// get transactiontype from TT
		// Use this for all general ledger methods that does not post
		TransactionTypeEndpoint ttep = new TransactionTypeEndpoint();
		TransactionType TT = ttep.getTransactionType(tt);
		if (StringExtension.isNullOrEmpty(TT.getCommComputationMethod()))
			return null;

		switch (TT.getCommComputationMethod()) {
		case "L": // Lookup
			LookupRule lrule = new LookupRule();
			if (TT.getTieredTableId() == 0)
				throw new IllegalArgumentException(
						"TieredTableId field not set in TransctionType [" + tt
								+ "]");
			List<LookupRow> llr = this.GetLookTable(TT.getTieredTableId());
			lrule.LookupTable = llr;
			return lrule;
		case "T": // tiered
			TieredRule rule = new TieredRule();
			if (TT.getTieredTableId() == 0)
				throw new IllegalArgumentException(
						"TieredTableId field not set in TransctionType [" + tt
								+ "]");
			List<LookupRow> lr = this.GetLookTable(TT.getTieredTableId());
			rule.LookupTable = lr;
			return rule;
		case "F": // flat rule
			FlatRateRule flat = new FlatRateRule();
			flat.Absolute = TT.getAbsolute();
			flat.Rate = TT.getCommissionAmount();
			// fill it up
			return flat;
		}

		return null;
	}

	private List<LookupRow> GetLookTable(Long Id) {
		// get transactiontype from TT
		// Use this for all general ledger methods that does not post
		TransactionTypeEndpoint ttep = new TransactionTypeEndpoint();
		TransactionType TT = ttep.getTransactionType(Id);
		if (TT == null)
			throw new IllegalArgumentException("TransactionType[" + Id
					+ "] does not exist");
		List<LookupRow> lr = new ArrayList<LookupRow>();

		TieredDetEndpoint tep = new TieredDetEndpoint();
		Collection<TieredDet> trd = tep.getTieredtableId(TT.getTieredTableId());
		for (TieredDet t : trd) {
			LookupRow lookupTable = new LookupRow();
			lookupTable.Id = t.getId();
			lookupTable.Min = t.getMin();
			lookupTable.Max = t.getMax();
			lookupTable.Rate = t.getRate();

			lr.add(lookupTable);
		}
		return lr;
	}

	public double ComputeCommissionByTransactionType(STO _sto,
			TransactionType tt) {
		CommissionComponent cc = new CommissionComponent();
		CommissionRule rule = cc.GetCommissionRuleFromTransactionType(tt);
		return this.ComputeByCommissionRule(_sto, rule);
	}

	public double GetCommissionAmountForSTO(STO _sto) {
		// variable to hold commission
		double _commission = 0;
		// compute commission if _sto.ChargeCommFlag==true &&
		// !_sto.CommissionPaidFlag
		if (_sto.getChargeCommFlag() && !_sto.getCommissionPaidFlag()) {
			STOCommSourceFlag stoflag = STOCommSourceFlag.valueOf(_sto
					.getCommSourceFlag());

			switch (stoflag) {
			case NoCommission:
				return _commission;
			case STO:
				return _sto.getCommissionAmount();
			case TransactionType:
				TransactionTypeEndpoint ttep = new TransactionTypeEndpoint();
				TransactionType DrTxnType = ttep.getTransactionType(_sto
						.getDrTxnType());
				// get the commission, pass sto and transaction type
				_commission = this.ComputeCommissionByTransactionType(_sto,
						DrTxnType);
				break;
			}
		}
		return _commission;
	}

	public class LookupRow {
		private Long Id;
		private double Min;
		private double Max;
		private double Rate;
		private boolean Absolute;

		public Long getId() {
			return Id;
		}

		public void setId(Long id) {
			Id = id;
		}

		public double getMin() {
			return Min;
		}

		public void setMin(double min) {
			Min = min;
		}

		public double getMax() {
			return Max;
		}

		public void setMax(double max) {
			Max = max;
		}

		public double getRate() {
			return Rate;
		}

		public void setRate(double rate) {
			Rate = rate;
		}

		public boolean isAbsolute() {
			return Absolute;
		}

		public void setAbsolute(boolean absolute) {
			Absolute = absolute;
		}

	}

}