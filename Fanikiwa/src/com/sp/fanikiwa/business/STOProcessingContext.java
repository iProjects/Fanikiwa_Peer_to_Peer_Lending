package com.sp.fanikiwa.business;

import java.util.Date;
import java.util.List;

import com.google.api.server.spi.response.NotFoundException;
import com.googlecode.objectify.annotation.Entity;
import com.googlecode.objectify.annotation.Id;
import com.sp.fanikiwa.Enums.STOProcessResultTypes;
import com.sp.fanikiwa.Enums.STOType;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.RequestResult;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;

public class STOProcessingContext {

	public STOProcessingContext(STO _sto) {
		sto = _sto;
		STOPayAmount = sto.getPayAmount();
		amountAvailable = drAccount.getBookBalance() - drAccount.getLimit();
		isPartialSTO = sto.getPartialPay();

		// Get the accounts
		drAccount = GLUtil.GetAccount(sto.getDrAccount());
		crAccount = GLUtil.GetAccount(sto.getCrAccount());

		// Get the transaction types.
		transactionType = GLUtil.GetTransactionType(sto.getDrTxnType());
		if (transactionType == null)
			throw new NullPointerException(
					"STO Transaction type cannot be null");

		// Compute commission
		CommissionComponent comm = new CommissionComponent();
		commission = comm.GetCommissionAmountForSTO(sto);
	}

	private boolean success;

	private RequestResult requestResult;

	/*
	 * Amount on the sto.PayAmount field
	 */
	private double STOPayAmount;
	/*
	 * Commission on processing Sto as computed by commission component.
	 */
	private double commission;
	/*
	 * Amount actually paid. It may differ from STOAmount if the partialPay
	 * field is set
	 */
	private double AmountPaid;
	/*
	 * Next STO processing date. For: sweeping and partially paid -->
	 * NextPayDate += 1 day normal NextPayDate += sto.Interval
	 */
	private Date NextPayDate;

	/*
	 * STO process results
	 */
	private STOProcessResultTypes sTOProcessResultTypes;

	private boolean isPartialSTO;

	/*
	 * Payment was made. It is set even for partial payment made
	 */
	private boolean paymentMade;
	private double amountAvailable;

	private STOType stoType;
	private STO sto;
	private Date runDate = new Date();

	private Account drAccount;
	private Account crAccount;
	private TransactionType transactionType;

	public Account getDrAccount() {
		return drAccount;
	}

	public Account getCrAccount() {
		return crAccount;
	}

	public boolean isSuccess() {
		return success;
	}

	public void setSuccess(boolean success) {
		this.success = success;
	}

	public RequestResult getRequestResult() {
		return requestResult;
	}

	public void setRequestResult(RequestResult requestResult) {
		this.requestResult = requestResult;
	}

	public double getSTOAmount() {
		/*
		 * Amount the sto is supposed to pay. It is sto.PayAmount for Normal STO
		 * or Available amount (i.e.
		 * Account.getClearedBalance()-Account.getLimit()) for Sweep STO
		 */
		double STOAmount = sto.getPayAmount();// default for all
		if (this.stoType == STOType.Sweep) {
			STOAmount = amountAvailable;
		}
		return STOAmount;
	}

	public double getSTOPayAmount() {
		return STOPayAmount;
	}

	public void setSTOPayAmount(double sTOPayAmount) {
		STOPayAmount = sTOPayAmount;
	}

	public double getCommission() {
		return commission;
	}

	public void setCommission(double commission) {
		commission = commission;
	}

	public double getAmountPaid() {
		return AmountPaid;
	}

	public void setAmountPaid(double amountPaid) {
		AmountPaid = amountPaid;
	}

	public Date getNextPayDate() {

		if (this.stoType == STOType.Sweep || this.isDefaulted()) {
			NextPayDate = DateExtension.addInterval(runDate, "D"); // tomorrow
		} else {
			NextPayDate = DateExtension.addInterval(runDate, sto.getInterval());
		}
		return NextPayDate;
	}

	public STOProcessResultTypes getSTOProcessResultTypes() {
		return sTOProcessResultTypes;
	}

	public void setSTOProcessResultTypes(STOProcessResultTypes sTOProcessResultTypes) {
		this.sTOProcessResultTypes = sTOProcessResultTypes;
	}

	public boolean isPaymentMade() {
		return paymentMade;
	}

	public void setPaymentMade(boolean paymentMade) {
		this.paymentMade = paymentMade;
	}

	/*
	 * AmountDefaulted += (STOAmount - AmountPaid)
	 */
	public double getAmountDefaulted() {
		//totalamount 
		return this.getSTOAmount() - this.AmountPaid;
	}

	public boolean isDefaulted() {
		return getAmountDefaulted() > 0.00;
	}

	public STOType getStoType() {
		return stoType;
	}

	public void setStoType(STOType stoType) {
		this.stoType = stoType;
	}

	public STO getSto() {
		return sto;
	}

	public void setSto(STO sto) {
		this.sto = sto;
	}

	private List<Transaction> STONormalAndSweepTransactions() throws Exception {
		// create all transactions
		List<Transaction> txns = TransactionFactory.LoanRepayment(
				transactionType, this.runDate, this.drAccount, this.crAccount,
				this.getSTOAmount(), "Repayment", "", "Diary", "Diary");

		// If commission is to be charged from the STO, add its transactions
		// here

		return txns;
	}

	private List<Transaction> STOPartialTransactions() throws Exception {

		List<Transaction> txns = TransactionFactory.LoanRepayment(
				transactionType, this.runDate, this.drAccount, this.crAccount,
				this.amountAvailable, "Repayment", "", "Diary", "Diary");

		// If commission is to be charged from the STO, add its transactions
		// here

		return txns;
	}

	private RequestResult SimulateNormal() throws Exception {
		List<Transaction> txns = STONormalAndSweepTransactions();
		return GLUtil.Simulate(txns);
	}

	private RequestResult SimulatePartial() throws Exception {
		List<Transaction> txns = STOPartialTransactions();
		return GLUtil.Simulate(txns);
	}

	public RequestResult Process() throws Exception
	{
		Post();
		SetVariables();
		return this.requestResult;
	}
	
	private void Post() throws Exception {

		RequestResult nre= SimulateNormal();
		if(nre.isSuccess())
		{
			requestResult = TransactionPost.Post(STONormalAndSweepTransactions());
			if (requestResult.isSuccess()) {

				// step 3. Set varaiables
				this.setAmountPaid(this.getSTOAmount());
				this.setPaymentMade(true);
				this.sTOProcessResultTypes = STOProcessResultTypes.FullyPaid;
				return;
			}
	
		} 
		else // Normal transactions did not simulate; lets try partial
		{
			if (isPartialSTO) // Applicable only if the STO accepts partial
								// payments
			{
				RequestResult pre= SimulatePartial();
				if(pre.isSuccess())
				{
					requestResult = TransactionPost.Post(STOPartialTransactions());
					if (requestResult.isSuccess()) {

						// step 3. Set parial varaiables
						this.setAmountPaid(this.amountAvailable);
						this.setPaymentMade(true);
						this.sTOProcessResultTypes = STOProcessResultTypes.PartialPaid;
						requestResult.setSuccess(true);
						requestResult.setResultMessage("Partial payment made");
						return;
					}
					
					this.sTOProcessResultTypes = STOProcessResultTypes.NotPaid;
					return;
				}
				requestResult = pre;
				this.sTOProcessResultTypes = STOProcessResultTypes.NotPaid;
			}
		}
		requestResult = nre;
	}

	private void SetVariables() throws NotFoundException {
		switch (this.sTOProcessResultTypes)
		{
		case FullyPaid:
			sto.setAmountPaid(sto.getAmountPaid()+ AmountPaid);
			sto.setNextPayDate (this.getNextPayDate());
			sto.setNoOfPaymentsMade(sto.getNoOfPaymentsMade()+1);
			break;
		case PartialPaid:
			sto.setAmountPaid(sto.getAmountPaid()+ AmountPaid);
			sto.setNextPayDate (this.getNextPayDate());
			sto.setNoOfPaymentsMade(sto.getNoOfPaymentsMade()+1);
			sto.setAmountDefaulted (sto.getAmountDefaulted()+ this.getAmountDefaulted());
			break;
		case NotPaid:
			sto.setNextPayDate (DateExtension.addDays(runDate, 1));
			 sto.setNoOfDefaults(sto.getNoOfDefaults()+1);
			sto.setAmountDefaulted (sto.getAmountDefaulted()+ this.getAmountDefaulted());
			break;
			
		}
		
		if (sto.getAmountDefaulted() > 0)
        {
        	sto.setNoOfDefaults(sto.getNoOfDefaults()+1);
        	sto.setNextPayDate(DateExtension.addDays(runDate, 1)) ; //keep checking every day
        }
		
		//Update STO now
		STOEndpoint stPost = new STOEndpoint();
        stPost.updateSTO(sto);
	}
}
