package com.sp.fanikiwa.business.financialtransactions;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.TransactionTypeEndpoint;
import com.sp.fanikiwa.business.CommissionComponent;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.DoubleEntry;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.Config;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.StringExtension;
import com.sp.utils.Utils;
import com.sp.fanikiwa.entity.Transaction;

public abstract class abstractMoneyTransaction {
	boolean sepCommission = false;

	Date valuedate;

	public Date PostDate;
	public Date ValueDate;
	public Date RecordDate;
	public boolean ForcePostFlag;
	public boolean StatementFlag;
	public String Authorizer;
	public String UserID;
	public String Reference;
	public String ContraReference;
	public String ShortCode;
	public Account DebitAccount;
	public Account CreditAccount;
	public Account CommissionDebitAccount;
	public Account CommissionCreditAccount;
	public double Amount;
	public double Commission;
	public boolean ChargeCommission;
	public boolean ChargeCommissionToTransaction;
	public String Narrative;
	public TransactionType TransactionType;
	public TransactionType CommissionTransactionType;

	public abstractMoneyTransaction(TransactionType ttype, String shortCode,
			Date postDate, Account drAccount, Account crAccount, double amount,
			boolean forcePost, boolean statFlag, String authorizer, String user,
			String reference) {
		if (ttype == null)
			throw new NullPointerException("Transaction type cannot be null");
		TransactionType = ttype;

		PostDate = postDate;
		ShortCode = shortCode;
		DebitAccount = drAccount;
		CreditAccount = crAccount;
		Amount = amount;
		ForcePostFlag = forcePost;
		StatementFlag = statFlag;
		Authorizer = authorizer;
		UserID = user;
		Reference = reference;

		ContraReference = Utils.GetRandomHexNumber(10);
		ValueDate = postDate;
		// Call initialize

		Initialize();
	}

	private void Initialize() {
		/*
		 * the following setting follow in order of priority 1. If user pushes
		 * it during transction - use the user setting otherwise 2. If it is set
		 * in the TType - use the TType setting otherwise 3. If it is set in the
		 * config file - use the config setting otherwise 4. Use the hardcoded
		 * setting hereabove
		 */
		// Who to debit commission - look at the Charge who field of transaction
		// type
		if (TransactionType.getChargeWho() != null
				&& TransactionType.getChargeWho().toUpperCase().equals("C")) {
			CommissionDebitAccount = CreditAccount;
		} else {
			CommissionDebitAccount = DebitAccount;
		}

		// Who to debit commission
		CommissionCreditAccount = Config.GetAccount("COMMISSIONACCOUNT");
		// if there is one set in the TType use it
		if (TransactionType.getCommissionCrAccount() != null
				&& TransactionType.getCommissionCrAccount() != 0)
			CommissionCreditAccount = GLUtil.GetAccount(TransactionType
					.getCommissionCrAccount());
		// Use generic comm txn type
		CommissionTransactionType = Config
				.GetTransactionType("COMMISSIONTRANSACTIONTYPE");
		if (TransactionType.getCommissionTransactionType() != null
				&& TransactionType.getCommissionTransactionType() != 0)
			CommissionTransactionType = GLUtil
					.GetTransactionType(TransactionType
							.getCommissionTransactionType());

		// value dates
		if (TransactionType.getValueDateOffset() > 0)
			valuedate = DateExtension.addDays(PostDate,
					TransactionType.getValueDateOffset());

		ChargeCommission = TransactionType.getChargeCommission();

	}

	public Transaction getDebitTransaction() {

		if(Amount == 0) return null;
		Transaction txn = new Transaction();
		txn.setAccount(DebitAccount);
		txn.setAmount(Amount * -1);
		txn.setTransactionType(TransactionType);
		txn.setPostDate(PostDate);
		txn.setValueDate(ValueDate);
		txn.setRecordDate(RecordDate);
		txn.setForcePostFlag(ForcePostFlag);
		txn.setStatementFlag(StatementFlag);
		txn.setAuthorizer(Authorizer);
		txn.setUserID(UserID);
		txn.setReference(Reference);
		txn.setContraReference(ContraReference);
		txn.setNarrative(Narrative);
		return txn;
	}

	public Transaction getCreditTransaction() {
		if(Amount == 0) return null;
		
		Transaction txn = new Transaction();
		txn.setAccount(CreditAccount);
		txn.setAmount(Amount);
		txn.setTransactionType(TransactionType);
		txn.setPostDate(PostDate);
		txn.setValueDate(ValueDate);
		txn.setRecordDate(RecordDate);
		txn.setForcePostFlag(ForcePostFlag);
		txn.setStatementFlag(StatementFlag);
		txn.setAuthorizer(Authorizer);
		txn.setUserID(UserID);
		txn.setReference(Reference);
		txn.setContraReference(ContraReference);
		txn.setNarrative(Narrative);
		return txn;

	}

	public Transaction getDebitCommissionTransaction() {

		double commission = EvaluateCommission();
		if (commission == 0)
			return null;

		// commission Dr
		Transaction txn = new Transaction();
		txn.setAccount(CommissionDebitAccount);
		txn.setAmount(commission * -1);
		txn.setTransactionType(CommissionTransactionType);
		txn.setPostDate(PostDate);
		txn.setValueDate(ValueDate);
		txn.setRecordDate(RecordDate);
		txn.setForcePostFlag(ForcePostFlag);
		txn.setStatementFlag(StatementFlag);
		txn.setAuthorizer(Authorizer);
		txn.setUserID(UserID);
		txn.setReference(Reference);
		txn.setContraReference(ContraReference);
		txn.setNarrative("Comm");
		return txn;
	}

	public Transaction getCreditCommissionTransaction() {
		double commission = EvaluateCommission();
		if (commission == 0)
			return null;

		Transaction txn = new Transaction();
		txn.setAccount(CommissionCreditAccount);
		txn.setAmount(commission);
		txn.setTransactionType(CommissionTransactionType);
		txn.setPostDate(PostDate);
		txn.setValueDate(ValueDate);
		txn.setRecordDate(RecordDate);
		txn.setForcePostFlag(ForcePostFlag);
		txn.setStatementFlag(StatementFlag);
		txn.setAuthorizer(Authorizer);
		txn.setUserID(UserID);
		txn.setReference(Reference);
		txn.setContraReference(ContraReference);
		txn.setNarrative("Comm");
		return txn;
	}

	public DoubleEntry GetDoubleEntry(NarrativeFormat fmt) {
		return GetDoubleEntry(fmt, false);
	}

	public DoubleEntry GetDoubleEntry(NarrativeFormat fmt, boolean format) {
		Transaction dr = GetTransaction(getDebitTransaction(),
				fmt.getDrNarrativeFommatter(), format);
		Transaction cr = GetTransaction(getCreditTransaction(),
				fmt.getCrNarrativeFommatter(), format);
		DoubleEntry de = new DoubleEntry();
		de.setDr(dr);
		de.setCr(cr);
		return de;
	}

	public Transaction GetTransaction(Transaction txn, String Fommatter,
			boolean format) {
		if (format)
		{
			if(txn!=null)
			txn.setNarrative(StringExtension.format(Fommatter, this));
		}
		return txn;
	}

	public Transaction GetTransaction(Transaction txn, String Fommatter) {
		return GetTransaction(txn, Fommatter, false);
	}

	public double EvaluateCommission() {
		CommissionComponent cc = new CommissionComponent();
		return cc.ComputeCommissionByTransactionType(this.Amount,
				this.TransactionType);
	}

	public DoubleEntry GetCommissionTransaction(NarrativeFormat fmt) {
		return GetCommissionTransaction(fmt, false);
	}

	public DoubleEntry GetCommissionTransaction(NarrativeFormat fmt,
			boolean format) {
		Transaction dr = GetTransaction(getDebitCommissionTransaction(),
				fmt.getDrNarrativeCommissionFommatter(), format);
		Transaction cr = GetTransaction(getCreditCommissionTransaction(),
				fmt.getCrNarrativeCommissionFommatter(), format);
		DoubleEntry de = new DoubleEntry();
		de.setDr(dr);
		de.setCr(cr);
		return de;
	}

	public List<Transaction> GetChargeCommissionToTransaction(
			NarrativeFormat fmt) {
		return GetChargeCommissionToTransaction(fmt, false);
	}

	public List<Transaction> GetChargeCommissionToTransaction(
			NarrativeFormat fmt, boolean format) {
		List<Transaction> txns = new ArrayList<Transaction>();
		Transaction dr = getDebitTransaction();

		Transaction cr = getCreditTransaction();
		Transaction cmm = getCreditCommissionTransaction();
		dr.setAmount(dr.getAmount() - cmm.getAmount());

		txns.add(GetTransaction(dr, fmt.getDrNarrativeCommissionFommatter(),
				format));
		txns.add(GetTransaction(cr, fmt.getDrNarrativeCommissionFommatter(),
				format));
		txns.add(GetTransaction(cmm, fmt.getDrNarrativeCommissionFommatter(),
				format));
		return txns;
	}

	public List<Transaction> GetTransactionsIncludingCommission(
			NarrativeFormat MainFmt, NarrativeFormat CommFmt) {
		List<Transaction> txns = new ArrayList<Transaction>();
		DoubleEntry mainDe = GetDoubleEntry(MainFmt, true);
		if (mainDe.getDr() != null)
			txns.add(mainDe.getDr());
		if (mainDe.getCr() != null)
			txns.add(mainDe.getCr());

		if (ChargeCommission) {
			DoubleEntry cmmDe = GetCommissionTransaction(CommFmt);
			if (cmmDe.getDr() != null)
				txns.add(cmmDe.getDr());
			if (cmmDe.getCr() != null)
				txns.add(cmmDe.getCr());
		}
		return txns;
	}

}