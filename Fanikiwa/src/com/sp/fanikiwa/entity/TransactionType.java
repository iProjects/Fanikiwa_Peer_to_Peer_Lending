package com.sp.fanikiwa.entity;

import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.googlecode.objectify.annotation.Index;
import com.sp.utils.Config;

@Entity
public class TransactionType {

	@Id
	Long transactionTypeID; 

	/*
	 * When true, the commission amount in the {commissionAmount} field is absolute and not a percentage
	 * 
	 * */
	private boolean absolute; 

	/*
	 * For future use: An expression that evaluates to an amount value.
	 * */
	private String amountExpression;

	/*
	 * When set, if an account used in this transaction type is does not exist, the value is posted in the
	 * suspenseCrAccount or suspenseDrAccount defined here.  The suspenseDrAccount & suspenseCrAccount accounts 
	 * are not tested for existence
	 * When Not Set, an exception is thrown
	 * */
	private boolean canSuspend;

	/*
	 * When set, commission is computed and posted otherwise it is not
	 * */
	private boolean chargeCommission;

	/*
	 * if true, the computed commission is removed from the transaction itself. 3 transactions are created instead of 4.
	 * */
	private boolean chargeCommissionToTransaction; 

	/*
	 * D|C. If D, the account being debited is charged commission otherwise the credit account is charged
	 * */
	private String chargeWho;

	/*
	 * [L|T\F] 
	 * L = Lookup commission from a table.
	 * T = Compute tiered value from a Tieredtable
	 * F = Flate rate
	 * */
	private String commComputationMethod;

	/*
	 * Commission amount used in Flatrate method. Can be an absolute or percentage. Interpretation is determined by {absolute} field
	 * */
	private double commissionAmount; 

	/*
	 * For future use: An expression that evaluates to an amount value.
	 * */
	private String commissionAmountExpression;

	/*
	 * A formatter string with placeholders for formatting the contra commission narrative. 
	 * The allowable place holders are in the format {abstractMoneyTransaction.*} where *= abstractMoneyTransaction properties
	 *  Defaults to  "{ShortCode} Comm{DebitAccount}" --> 
	 * */
	private String commissionContraNarrative;

	/*
	 * The account where commission is credited. It defaults to Config["COMMISSIONACCOUNT"] if not set.
	 * */
	private Long commissionCrAccount;

	/*
	 * For future use.
	 * */
	private Long commissionDrAccount;

	/*
	 * For future use.
	 * */
	private boolean commissionDrAnotherAccount;

	/*
	 * A formatter string with placeholders for formatting the main commission narrative. 
	 * The allowable place holders are in the format {abstractMoneyTransaction.*} where *= abstractMoneyTransaction properties
	 *Defaults to "{ShortCode} Comm"
	 * */
	private String commissionMainNarrative;

	/*
	 * Determines which narrative formatting method to use.
	 * */
	private short commissionNarrativeFlag;
	

	/*
	 *Used to post commission transaction if this transaction canCharge commission. If not set, defaults to
	 * Config.GetTransactionType("COMMISSIONTRANSACTIONTYPE")
	 * */
	private Long commissionTransactionType;

	/*
	 * For future use.
	 * */
	private String crCommCalcMethod;

	/*
	 * [D|C] Determines how to treate the amount for Main and Contra transactions. When set to
	 * 1) D - the amount in Main transaction  is -ve and the amount in Contra transaction is +ve
	 * 2) C - opposite of 1 above
	 * */
	private String debitCredit;

	/*
	 *  For future use. Default amount for the transaction
	 * */
	private double defaultAmount;

	/*
	 *  For future use. Default contra account for the transaction
	 * */
	private Long defaultContraAccount;

	/*
	 * A formatter string with placeholders for formatting the contra narrative. 
	 * The allowable place holders are in the format {abstractMoneyTransaction.*} where *= abstractMoneyTransaction properties
	 * Defaults to  "{ShortCode}" --> that evaluates to the shortcode of the transaction
	 * */
	private String defaultContraNarrative;

	/*
	 *  Default main account for the transaction. 
	 *  If set or withdrawal transaction, it overrides the Config[CASHACCOUNT]
	 * */
	private Long defaultMainAccount;

	/*
	 * A formatter string with placeholders for formatting the main narrative. 
	 * The allowable place holders are in the format {abstractMoneyTransaction.*} where *= abstractMoneyTransaction properties
	 * Defaults to  "{ShortCode}" --> that evaluates to the shortcode of the transaction
	 * */
	private String defaultMainNarrative;

	/*
	 * Describes the transaction
	 * */
	private String description;

	/*
	 * [0|1|2]For future use. 
	 * 0 - Transaction can be used both in background(System) or Dialog(user)
	 * 1 - Dialog only
	 * 2 - System only
	 * */
	private short dialogFlag;

	/*
	 * For future use.
	 * */
	private String drCommCalcMethod;

	/*
	 * When set, the transaction shall be posted without checking LimitFlag or PassFlag of the account. 
	 * i.e can post even to blocked/disabled/overdrawn accounts. 
	 * */
	private boolean forcePost;

	/*
	 * For future use.
	 * */
	private short narrativeFlag;

	/*
	 * Transaction type short code; used by narrative formatter
	 * */
	private String shortCode;

	/*
	 * [S|] S- the transaction will appear in the statement
	 * */
	private boolean statFlag;

	/*
	 * Account to suspend a credit transaction.
	 * */
	private Long suspenseCrAccount;

	/*
	 * Account to suspend a debit transaction.
	 * */
	private Long suspenseDrAccount;

	/*
	 * Identifies the table to be used by either Tiered or Lookup computation methods.
	 * */
	private Long tieredTableId;

	/*
	 * For future use.
	 * */
	private short txnClass;

	/*
	 * [0|1|2] For future use. Used to draw the screen for dialog use. 
	 *  0 - Draw a Single Entry View
	 *  2 - Draw a Double Entry View
	 *  3 - Draw a Multi Entry View
	 * */
	private short txnTypeView;

	/*
	 * The number of days amount remains uncleared.  The value should be positive.  
	Added to the postdate to get valuedate
	 * */
	private short valueDateOffset;

	public TransactionType() {
	}

	public Long getTransactionTypeID() {
		return this.transactionTypeID;
	}

	public void setTransactionTypeID(Long transactionTypeID) {
		this.transactionTypeID = transactionTypeID;
	}

	public boolean getAbsolute() {
		return this.absolute;
	}

	public void setAbsolute(boolean absolute) {
		this.absolute = absolute;
	}

	public String getAmountExpression() {
		return this.amountExpression;
	}

	public void setAmountExpression(String amountExpression) {
		this.amountExpression = amountExpression;
	}

	public boolean getCanSuspend() {
		return this.canSuspend;
	}

	public void setCanSuspend(boolean canSuspend) {
		this.canSuspend = canSuspend;
	}

	public boolean getChargeCommission() {
		return this.chargeCommission;
	}

	public void setChargeCommission(boolean chargeCommission) {
		this.chargeCommission = chargeCommission;
	}

	public boolean getChargeCommissionToTransaction() {
		return this.chargeCommissionToTransaction;
	}

	public void setChargeCommissionToTransaction(
			boolean chargeCommissionToTransaction) {
		this.chargeCommissionToTransaction = chargeCommissionToTransaction;
	}

	public String getChargeWho() {
		return this.chargeWho;
	}

	public void setChargeWho(String chargeWho) {
		this.chargeWho = chargeWho;
	}

	public String getCommComputationMethod() {
		return this.commComputationMethod;
	}

	public void setCommComputationMethod(String commComputationMethod) {
		this.commComputationMethod = commComputationMethod;
	}

	public double getCommissionAmount() {
		return this.commissionAmount;
	}

	public void setCommissionAmount(double commissionAmount) {
		this.commissionAmount = commissionAmount;
	}

	public String getCommissionAmountExpression() {
		return this.commissionAmountExpression;
	}

	public void setCommissionAmountExpression(String commissionAmountExpression) {
		this.commissionAmountExpression = commissionAmountExpression;
	}

	public String getCommissionContraNarrative() {
		return this.commissionContraNarrative;
	}

	public void setCommissionContraNarrative(String commissionContraNarrative) {
		this.commissionContraNarrative = commissionContraNarrative;
	}

	public Long getCommissionCrAccount() {
		return this.commissionCrAccount;
	}

	public void setCommissionCrAccount(Long commissionCrAccount) {
		this.commissionCrAccount = commissionCrAccount;
	}

	public Long getCommissionDrAccount() {
		return this.commissionDrAccount;
	}

	public void setCommissionDrAccount(Long commissionDrAccount) {
		this.commissionDrAccount = commissionDrAccount;
	}

	public boolean getCommissionDrAnotherAccount() {
		return this.commissionDrAnotherAccount;
	}

	public void setCommissionDrAnotherAccount(boolean commissionDrAnotherAccount) {
		this.commissionDrAnotherAccount = commissionDrAnotherAccount;
	}

	public String getCommissionMainNarrative() {
		return this.commissionMainNarrative;
	}

	public void setCommissionMainNarrative(String commissionMainNarrative) {
		this.commissionMainNarrative = commissionMainNarrative;
	}

	public short getCommissionNarrativeFlag() {
		return this.commissionNarrativeFlag;
	}

	public void setCommissionNarrativeFlag(short commissionNarrativeFlag) {
		this.commissionNarrativeFlag = commissionNarrativeFlag;
	}

	public Long getCommissionTransactionType() {
		return this.commissionTransactionType;
	}

	public void setCommissionTransactionType(Long commissionTransactionType) {
		this.commissionTransactionType = commissionTransactionType;
	}

	public String getCrCommCalcMethod() {
		return this.crCommCalcMethod;
	}

	public void setCrCommCalcMethod(String crCommCalcMethod) {
		this.crCommCalcMethod = crCommCalcMethod;
	}

	public String getDebitCredit() {
		return this.debitCredit;
	}

	public void setDebitCredit(String debitCredit) {
		this.debitCredit = debitCredit;
	}

	public double getDefaultAmount() {
		return this.defaultAmount;
	}

	public void setDefaultAmount(double defaultAmount) {
		this.defaultAmount = defaultAmount;
	}

	public Long getDefaultContraAccount() {
		return this.defaultContraAccount;
	}

	public void setDefaultContraAccount(Long defaultContraAccount) {
		this.defaultContraAccount = defaultContraAccount;
	}

	public String getDefaultContraNarrative() {
		return this.defaultContraNarrative;
	}

	public void setDefaultContraNarrative(String defaultContraNarrative) {
		this.defaultContraNarrative = defaultContraNarrative;
	}

	public Long getDefaultMainAccount() {
		return this.defaultMainAccount;
	}

	public void setDefaultMainAccount(Long defaultMainAccount) {
		this.defaultMainAccount = defaultMainAccount;
	}

	public String getDefaultMainNarrative() {
		return this.defaultMainNarrative;
	}

	public void setDefaultMainNarrative(String defaultMainNarrative) {
		this.defaultMainNarrative = defaultMainNarrative;
	}

	public String getDescription() {
		return this.description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public short getDialogFlag() {
		return this.dialogFlag;
	}

	public void setDialogFlag(short dialogFlag) {
		this.dialogFlag = dialogFlag;
	}

	public String getDrCommCalcMethod() {
		return this.drCommCalcMethod;
	}

	public void setDrCommCalcMethod(String drCommCalcMethod) {
		this.drCommCalcMethod = drCommCalcMethod;
	}

	public boolean getForcePost() {
		return this.forcePost;
	}

	public void setForcePost(boolean forcePost) {
		this.forcePost = forcePost;
	}

	public short getNarrativeFlag() {
		return this.narrativeFlag;
	}

	public void setNarrativeFlag(short narrativeFlag) {
		this.narrativeFlag = narrativeFlag;
	}

	public String getShortCode() {
		return this.shortCode;
	}

	public void setShortCode(String shortCode) {
		this.shortCode = shortCode;
	}

	public boolean getStatFlag() {
		return this.statFlag;
	}

	public void setStatFlag(boolean statFlag) {
		this.statFlag = statFlag;
	}

	public Long getSuspenseCrAccount() {
		return this.suspenseCrAccount;
	}

	public void setSuspenseCrAccount(Long suspenseCrAccount) {
		this.suspenseCrAccount = suspenseCrAccount;
	}

	public Long getSuspenseDrAccount() {
		return this.suspenseDrAccount;
	}

	public void setSuspenseDrAccount(Long suspenseDrAccount) {
		this.suspenseDrAccount = suspenseDrAccount;
	}

	public Long getTieredTableId() {
		return this.tieredTableId;
	}

	public void setTieredTableId(Long tieredTableId) {
		this.tieredTableId = tieredTableId;
	}

	public short getTxnClass() {
		return this.txnClass;
	}

	public void setTxnClass(short txnClass) {
		this.txnClass = txnClass;
	}

	public short getTxnTypeView() {
		return this.txnTypeView;
	}

	public void setTxnTypeView(short txnTypeView) {
		this.txnTypeView = txnTypeView;
	}

	public short getValueDateOffset() {
		return this.valueDateOffset;
	}

	public void setValueDateOffset(short valueDateOffset) {
		this.valueDateOffset = valueDateOffset;
	}

}