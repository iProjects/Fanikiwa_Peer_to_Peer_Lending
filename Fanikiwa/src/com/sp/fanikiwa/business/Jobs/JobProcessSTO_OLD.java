package com.sp.fanikiwa.business.Jobs;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;

import com.google.api.server.spi.response.NotFoundException;
import com.sp.fanikiwa.Enums.PostingCheckFlag;
import com.sp.fanikiwa.Enums.STOCommFreqFlag;
import com.sp.fanikiwa.Enums.STOCommissionChargeWho;
import com.sp.fanikiwa.Enums.STOType;
import com.sp.fanikiwa.api.AccountEndpoint;
import com.sp.fanikiwa.api.STOEndpoint;
import com.sp.fanikiwa.api.TransactionTypeEndpoint;
import com.sp.fanikiwa.business.CommissionComponent;
import com.sp.fanikiwa.business.financialtransactions.GenericTransaction;
import com.sp.fanikiwa.business.financialtransactions.NarrativeFormat;
import com.sp.fanikiwa.business.financialtransactions.TransactionFactory;
import com.sp.fanikiwa.business.financialtransactions.TransactionPost;
import com.sp.fanikiwa.entity.Account;
import com.sp.fanikiwa.entity.DoubleEntry;
import com.sp.fanikiwa.entity.STO;
import com.sp.fanikiwa.entity.Transaction;
import com.sp.fanikiwa.entity.TransactionType;
import com.sp.utils.DateExtension;
import com.sp.utils.GLUtil;
import com.sp.utils.Utils;

/*
 * This processes all STOs that are due. It uses the  STO.startDate > rundate to determine
 * the STOs to process. 
 * It computes interest using the interest computation method defined in loan.InterestComputationMethod
 * and accrues interest on the either field loan.AccruedInterest or loan.AccruedIntInSusp depending on the
 * setting loan.AccrueInSusp value. It adds interest to the already existing figure in those fields
 * 
 * Finally it adjusts the loan.LastIntAccrualDate and loan.NextIntAccrualDate depending on the setting
 * in field loan.InterestAccrualInterval
 * */
public class JobProcessSTO_OLD implements IJobItem {

	boolean EnableLog = true;

	private static final Logger log = Logger.getLogger(JobProcessSTO_OLD.class.getName());

	@Override
	public void Run(Date d) {
		// TODO Auto-generated method stub
		if (EnableLog) log.info("Processing STO for [" + d + "] started");
		RunSTOs(d);
		if (EnableLog) log.info("Processing STO for [" + d + "] ended");
	}
	


    private void RunSTOs(Date date)
    {

        //1. Run STOs
        STOEndpoint st = new STOEndpoint();
        Collection<STO> _ScheduleTransactions = st.SelectSTOByDateFrom(date).getItems();

        if (EnableLog) log.info("Processing [" + _ScheduleTransactions.size() + "] transactions");
        for (STO _sto : _ScheduleTransactions)
        {
            //Process now
            try
            {
                String msg = String.format("Processing STO Id[{0}], NextPayDate[{7}] "+ 
                        "DrAccount[{1}],  "+ 
                        "CrAccount[{2}],  "+ 
                        "PayAmount[{3}],  "+ 
                        "TotalToPay[{4}],  "+ 
                        "CrTxnType[{5}],  "+ 
                        "DrTxnType[{6}] ", 
                 _sto.getId(),
                 _sto.getDrAccount(),
                 _sto.getCrAccount(),
                 _sto.getPayAmount(),
                 _sto.getTotalToPay(),
                 _sto.getCrTxnType(),
                 _sto.getDrTxnType(),
                 _sto.getNextPayDate());
                if (EnableLog) log.info(msg);

                ProcessSTOTxn(_sto, date);
                if (EnableLog) log.info("Processing STO[" + _sto.getId()+ "] completed");
            }
            catch (Exception ex)
            {
                log.log(Level.SEVERE, "An error occurred while processing STO[" + _sto.getId() + "] completed\n", ex);
            }

        }

    }


    private void ProcessSTOTxn(STO _sto, Date date) throws Exception
    {
        /*
         * STO transfers money from one account to another
         */
    	STOType stoflag = STOType.valueOf( _sto.getSTOType());
 		
        switch (stoflag)
        {
            case Normal: //Normal STO; pays the sto
                ProcessNormalSTO(_sto, date);
                break;
            case Sweep: //Sweep STO; sweeps the DrAcc
                ProcessSweepSTO(_sto, date);
                break;
            default:
                ProcessNormalSTO(_sto, date);
                break;
        }


    }

    private void ProcessNormalSTO(STO _sto, Date date) throws Exception
    {
        //1. if all payments are don, return
        if (_sto.getAmountPaid() >= _sto.getTotalToPay())
        {
            // log
            if (EnableLog) log.info("STO "+_sto.getId()+"already cleared...");
            return;
        }

        //2. compute AmountToPay
        double commission=0, AmountNeeded = 0;
        double AmountToPay = _sto.getTotalToPay() - _sto.getAmountPaid();
        if (AmountToPay > _sto.getPayAmount())
        {
            AmountToPay = _sto.getPayAmount();
        }


        //2. compute AmountNeeded
        //get commission
        CommissionComponent comm = new CommissionComponent();
        /*Compute commission taking care of flags 
         * 1 _sto.ChargeCommFlag
         * 2 _sto.CommissionPaidFlag
         * 3 _stoCommSourceFlag
         */
        commission = comm.GetCommissionAmountForSTO(_sto);

        AmountNeeded = commission + AmountToPay; //commission will be 0 if not charged
        AccountEndpoint aep = new AccountEndpoint();
        Account acc = aep.getAccount(_sto.getDrAccount());
        double AmountAvailable = acc.getClearedBalance()-acc.getLimit();


        /*
         * Scenario A - STOpartial pay = false
         * Scenario A1 - Account has enough money for both repayment and commission
         * Scenario A2 - Account has enough money for commission only
         * Scenario A3 - Account does not have enough money even for commission 
         * 
         * Scenario B - STOpartial pay = true
         * Scenario B1 - Account has enough money for both repayment and commission
         * Scenario B2 - Account has enough money for commission only
         * Scenario B3 - Account does not have enough money even for commission 

         */

        //First and foremost charge commission
        /* 1.
        ChargeCommission takes care of
        commission == 0
        sto.CommissionPaidFlag
        sto.ChargeCommFlag
        sto.STOCommFreqFlag.NoCommission
    */
        ChargeCommission(_sto.getDrAccount(), _sto.getCrAccount(), _sto, commission, date);

        //Scenario A1 && Scenario B1
        if (AmountAvailable >= AmountNeeded)
        {
            //if account has money, post full amount
            AmountToPay = _sto.getPayAmount();
            ProcessInstallment(_sto.getDrAccount(), _sto.getCrAccount(), _sto, AmountToPay, date, AmountAvailable);
            return;
        }
        else
        {

            //Scenario A2  - taken care of Batchposting with limit checking

            //Scenario B2
            if (_sto.getPartialPay())
            {
                //account doesnt have enough money, pay whatever is there 
                AmountToPay = acc.getClearedBalance()-acc.getLimit();
                ProcessInstallment(_sto.getDrAccount(), _sto.getCrAccount(), _sto, AmountToPay, date, AmountToPay);
                return;
            }

            //Scenario A3 && Scenario B3
            if (AmountAvailable < commission)
            {
                ProcessNonPay(_sto.getDrAccount(), _sto.getCrAccount(), _sto, commission, date, AmountAvailable);
                return;
            }
        }

    }

    private void ProcessSweepSTO(STO _sto, Date date) throws Exception
    {
        /*
         * Sweep everything in the account.
         * Does not consider the value sto.Payamount field
         */
    	 AccountEndpoint aep = new AccountEndpoint();
         Account acc = aep.getAccount(_sto.getDrAccount());
         double AmountAvailable = acc.getClearedBalance()-acc.getLimit();
         
        CommissionComponent comm = new CommissionComponent();
        double commission = comm.GetCommissionAmountForSTO(_sto); //get the commission for running the sto from the transaction type
        double PayAmount = AmountAvailable - commission; //minus comm coz it is going to b charged herebelow separately
        ChargeCommission(_sto.getDrAccount(), _sto.getCrAccount(), _sto, commission, date);
        ProcessInstallment(_sto.getDrAccount(), _sto.getCrAccount(), _sto, PayAmount, date, PayAmount);
    }


    private void ProcessInstallment(Long DrAcc, Long CrAcc, STO _sto, double PayAmount, Date date, double AmountAvailable) throws Exception
    {

        //1. Move Money from DrAcc to CrAcc
        PostSTOTransactions(DrAcc, CrAcc, _sto, PayAmount, date);

        //2. set flags
        _sto.setAmountPaid(_sto.getAmountPaid()+ PayAmount);
        _sto.setNextPayDate (DateExtension.addInterval(date, _sto.getInterval()));
        _sto.setNoOfPaymentsMade(_sto.getNoOfPaymentsMade()+1);
        _sto.setAmountDefaulted (_sto.getAmountDefaulted()+ (_sto.getPayAmount() - _sto.getPayAmount()));
        if (_sto.getAmountDefaulted() > 0)
        {
            _sto.setNoOfDefaults(_sto.getNoOfDefaults()+1);
            _sto.setNextPayDate(DateExtension.addDays(date, 1)) ; //keep checking every day
        }

        STOEndpoint stPost = new STOEndpoint();
        stPost.updateSTO(_sto);

        //3. log
        if (EnableLog) log.info("Payment NOT made.: AmountNeeded[" + _sto.getPayAmount() +
           "], AmountAvailable[" + AmountAvailable + "]");
    }

    private void ProcessNonPay(Long Borrower, Long Investor, STO _sto, double commission, Date date, double AmountAvailable) throws NotFoundException
    {
        _sto.setNextPayDate(DateExtension.addDays(date, 1)); //keep checking every day
        _sto.setNoOfDefaults(_sto.getNoOfDefaults()+1);
        _sto.setAmountDefaulted (_sto.getAmountDefaulted()+  _sto.getPayAmount());

        //update db
        STOEndpoint stPost = new STOEndpoint();
        stPost.updateSTO(_sto);
        if (EnableLog) log.info("Payment NOT made.: AmountNeeded[" + _sto.getPayAmount() +
            "],  Commission[" + commission + "] AmountAvailable[" + AmountAvailable + "]");
    }

  
    private boolean PostSTOTransactions(Long DrAcc, Long CrAcc, STO sto, double Amount, Date stoCreatedDate) throws Exception
    {
        TransactionTypeEndpoint tep = new TransactionTypeEndpoint();
        List<Transaction> txns = TransactionFactory.LoanRepayment(
        		tep.getTransactionType(sto.getDrTxnType()), 
        		stoCreatedDate, 
        		GLUtil.GetAccount(DrAcc), 
        		GLUtil.GetAccount(CrAcc), Amount, "Repayment", "", "Diary", "Diary");
        //now request posting service to post
        TransactionPost.Post(txns);
        return true;
    }

 

    private void ChargeCommission(Long DrAcc, Long CrAcc, STO sto, double commission, Date date) throws NotFoundException
    {
        if (commission == 0) return;

        if (sto.getCommissionPaidFlag()) return;

        if (!sto.getChargeCommFlag()) return;

        if ( STOCommFreqFlag.values()[sto.getCommFreqFlag()]  == STOCommFreqFlag.NoCommission) return;

        ChargeCommissionImpl(DrAcc, CrAcc, sto, commission, date);
    }

    private void ChargeCommissionImpl(Long DrAcc, Long CrAcc, STO sto, double commission, Date date) throws NotFoundException
    {
        //build commission transactions

        List<Transaction> txns = new ArrayList<Transaction>();
        //Create Ref and contra
        String contraref = Utils.GetRandomHexNumber(10);

        boolean forcePost =(PostingCheckFlag.values()[sto.getLimitFlag()]  == PostingCheckFlag.ForcePost);
        		//(sto.LimitFlag == (short)PostingLimitCheckFlag.ForcePost);
        


        /*
            1. If r.ChargeWho == 'L' // Borrower to pay commission
            2. If r.ChargeWho == 'I' // Investor to pay commission
            3. If r.ChargeWho == 'B' // Both to pay commission
         */
        switch (STOCommissionChargeWho.values()[ sto.getChargeWho()])
        {
            case Borrower:
                txns = MakeCommissionTxns(forcePost, contraref, DrAcc, sto, commission, date);
                break;
            case Investor:
                txns = MakeCommissionTxns(forcePost, contraref, CrAcc, sto, commission, date);
                break;
            case BothBorrowerAndInvestor:
                txns = MakeCommissionTxns(forcePost, contraref, DrAcc, sto, commission, date);
                txns.addAll(MakeCommissionTxns(forcePost, contraref, CrAcc, sto, commission, date));
                break;
            default:
                break;
        }

        //Post
        //now request posting service to post with limit checking according to sto set up
        TransactionPost.BatchPost(txns, PostingCheckFlag.values()[ sto.getLimitFlag()]);


        //Mark STO Commission as paid
        /*
            Mark comm as paid if CommFreqFlag== STOCommFreqFlag.PayCommissionPerOnce
         */
        if (STOCommFreqFlag.values()[ sto.getCommFreqFlag()] == STOCommFreqFlag.PayCommissionPerOnce)
        {
            sto.setCommissionPaidFlag ( true);
            STOEndpoint sPost = new STOEndpoint();
            sPost.updateSTO(sto);
        }
    }
    private List<Transaction> MakeCommissionTxns(boolean forcePost, String contraref, Long DrAcc, STO sto, double Amount, Date date)
    {
        List<Transaction> txns = new ArrayList<Transaction>();
        TransactionType tt = GLUtil.GetTransactionType(sto.getDrTxnType());

        if (Amount != 0)
        {
        	 GenericTransaction ltxn = new GenericTransaction(tt,
        	            "LES",
        	            new Date(),
        	            GLUtil.GetAccount(DrAcc),
        	            GLUtil.GetAccount(sto.getCommissionAccount()),
        	            Amount,
        	            forcePost,
        	            true,
        	            "SYS",
        	            "SYS",
        	            "STO Comm");
        	 NarrativeFormat fmt = new NarrativeFormat(tt);
        	DoubleEntry de = ltxn.GetDoubleEntry(fmt);
           txns.add(de.getDr());txns.add(de.getCr());

        }
        return txns;
    }


    //private void ClearInvestment(Member Borrower, Member Investor, int TTYpe, double Amount, Date stoCreatedDate)
    //{
    //    try
    //    {
    //        //build commission transactions

    //        List<Transaction> txns = new List<Transaction>();
    //        //Create Ref and contra
    //        string contraref = Utils.GetRandomHexNumber(10);

    //        MakeClearInvestmentTxns(contraref, Borrower, Investor, TTYpe, Amount, txns, stoCreatedDate);
    //        //Post
    //        FinancialPostingComponent fPost = new FinancialPostingComponent();
    //        //now request posting service to post
    //        fPost.BatchPost(txns);

    //        //ask log4net to log posted transactions details
    //        foreach (var txn in txns)
    //        {
    //            infoLog.Info(string.Format("Posted ClearInvestment Transaction. AccountID: {0}, Amount: {1}, PostDate: {2}, RecordDate: {3}, Narrative: {4} ", txn.AccountID, txn.Amount, txn.PostDate, txn.RecordDate, txn.Narrative));
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        //TODO: Handle your exceptions here. Remove any try-catch blocks if you
    //        //are not handling any exceptions.  
    //        bool rethrow = false;
    //        rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
    //        if (rethrow)
    //        {
    //            CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
    //            throw new FaultException<CustomExpMsg>(customMsg,
    //                new FaultReason(customMsg.ErrorMsg),
    //                new FaultCode("ClearInvestment"));
    //        }
    //    }
    //}

    //public void MakeClearInvestmentTxns(string contraref, Member Borrower, Member Investor, int TxnType, double Amount, List<Transaction> txns, Date stoCreateDate)
    //{
    //    try
    //    {
    //        //Build the debit transaction 
    //        Transaction drtransaction = new Transaction();
    //        //investor investmentaccount was credited when loan was created, now debit to show repayment
    //        drtransaction.AccountID = Investor.InvestmentAccountId;
    //        drtransaction.Amount = Amount * -1;
    //        drtransaction.TransactionTypeId = TxnType;

    //        //postdate is sto createddate
    //        drtransaction.PostDate = stoCreateDate;
    //        drtransaction.RecordDate = Date.Today;

    //        drtransaction.ForcePostFlag = false;
    //        drtransaction.StatementFlag = "Y";
    //        drtransaction.Authorizer = "SYSTEM";
    //        drtransaction.UserID = "SYSTEM";
    //        drtransaction.Reference = Config.GetInt["FANIKIWAAGENT"];
    //        drtransaction.ContraReference = contraref;


    //        //Build the credit transaction
    //        Transaction crtransaction = new Transaction();
    //        //borrower loanaccount was debited when loan was created, now credit to show the repayment
    //        crtransaction.AccountID = Borrower.LoanAccountId;
    //        crtransaction.Amount = Amount;
    //        crtransaction.TransactionTypeId = TxnType;

    //        //postdate is sto createddate
    //        crtransaction.PostDate = stoCreateDate;
    //        crtransaction.RecordDate = Date.Today;

    //        crtransaction.ForcePostFlag = false;
    //        crtransaction.StatementFlag = "Y";
    //        crtransaction.Authorizer = "SYSTEM";
    //        crtransaction.UserID = "SYSTEM";
    //        crtransaction.Reference = Config.GetInt["FANIKIWAAGENT"];
    //        crtransaction.ContraReference = contraref;

    //        //Crossreference narratives
    //        drtransaction.Narrative = "ClearInvestment " + crtransaction.AccountID;
    //        crtransaction.Narrative = "ClearInvestment " + drtransaction.AccountID;

    //        txns.Add(drtransaction);
    //        txns.Add(crtransaction);
    //    }
    //    catch (Exception ex)
    //    {
    //        //TODO: Handle your exceptions here. Remove any try-catch blocks if you
    //        //are not handling any exceptions.  
    //        bool rethrow = false;
    //        rethrow = BusinessLogicExceptionHandler.HandleException(ref ex);
    //        if (rethrow)
    //        {
    //            CustomExpMsg customMsg = new CustomExpMsg(ex.Message);
    //            throw new FaultException<CustomExpMsg>(customMsg,
    //                new FaultReason(customMsg.ErrorMsg),
    //                new FaultCode("MakeClearInvestmentTxns"));
    //        }
    //    }
    //}


}
