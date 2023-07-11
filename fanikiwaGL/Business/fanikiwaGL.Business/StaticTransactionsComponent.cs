//====================================================================================================
// Code generated with Motion: BC Gen (Build 2.2.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by francis.muraya at KPC7070W on 08/07/2013 17:18:46 
//====================================================================================================

using fanikiwaGL.Data;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fanikiwaGL.Framework.ExceptionHandlers;
using fanikiwaGL.Framework.ExceptionTypes;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using TX = System.Transactions;

namespace fanikiwaGL.Business
{
    /// <summary>
    /// StaticPosting business component.
    /// </summary>
    public partial class StaticTransactionsComponent
    {

        #region "Static Transactions on Customers"
        /// <summary>
        /// CreateCustomer business method. 
        /// </summary>
        /// <param name="customer">A customer value.</param>
        public Customer CreateCustomer(Customer customer)
        {
            // Data access component declarations.
            CustomerDAC customerDAC = new CustomerDAC();

            // Step 1 - Calling Create on CustomerDAC.
            Customer cus = customerDAC.Create(customer);
            return cus;
        }

        /// <summary>
        /// UpdateCustomer business method. 
        /// </summary>
        /// <param name="customer">A customer value.</param>
        public Customer UpdateCustomer(Customer customer)
        {
            // Data access component declarations.
            CustomerDAC customerDAC = new CustomerDAC();

            // Step 1 - Calling Create on CustomerDAC.
            customerDAC.UpdateById(customer);
            return customer;
        }
        #endregion "Static Transactions on Customers"

        #region "Static Transactions on Accounts"
        /// <summary>
        /// OpenAccount business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public Account OpenAccount(Account account)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();

            // Step 1 - Calling Create on AccountDAC.
            Account acc = accountDAC.Create(account);
            return acc;
        }

        /// <summary>
        /// CloseAccount business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void CloseAccount(Account account)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();

            // Step 1 - Calling UpdateById on AccountDAC.
            account.Closed = true;
            accountDAC.UpdateById(account);

        }

        /// <summary>
        /// UpdateAccount business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void UpdateAccount(Account account)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();

            // Step 1 - Calling UpdateById on AccountDAC.
            accountDAC.UpdateById(account);
        }

        #region "Limits"
        /// <summary>
        /// BlockFunds business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void BlockFunds(int accountId, decimal amount)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();
            Account account = accountDAC.SelectById(accountId);
            this.MarkLimit(account, amount);
        }

        /// <summary>
        /// BlockFunds business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void BlockFunds(Account account, decimal amount)
        {
            this.MarkLimit(account, amount);
        }

        /// <summary>
        /// BlockFunds business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void UnBlockFunds(Account account, decimal amount)
        {
            this.MarkLimit(account, amount * -1);
        }

        /// <summary>
        /// BlockFunds business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void UnBlockFunds(int accountId, decimal amount)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();
            Account account = accountDAC.SelectById(accountId);
            UnBlockFunds(account, amount);
        }

        /// <summary>
        /// MarkLimit business method. 
        /// </summary>
        /// <param name="account">A account value.</param>
        public void MarkLimit(Account account, decimal amount)
        {
            // Step 1 - Block funds mean making funds unavailable
            //funds are made unavailable by increasing the limit.

            //Handle as ACID
            using (System.Transactions.TransactionScope scope = new TX.TransactionScope())
            {
                if (account.Closed)
                    throw new StaticPostingException("Account closed");

                //check that funds are available after limiting  
                AccountLimitStatus limistatus = this.GetAccountLimitStatus(account);
                PassFlag lockstatus = this.GetAccountLockStatus(account);

                decimal AmountAvailable = account.ClearedBalance - account.Limit;
                decimal AvailableBalanceAfterApplyingLimit = AmountAvailable - amount;
                decimal limit = account.Limit + amount;

                //check 1 - Lock status
                if ((lockstatus == PassFlag.Locked))
                    throw new AccountLockException("Cannot mark limit to account [{0}].\nAccount lock status =[{1}]",
                        account.AccountID,
                        lockstatus);


                //check 2 - Limit status
                if ((limistatus == AccountLimitStatus.AllLimitsProhibited)
                    || (limistatus == AccountLimitStatus.LimitForBlockingProhibited && limit > 0)
                    || (limistatus == AccountLimitStatus.LimitForAdvanceProhibited && limit < 0)
                    )
                    throw new LimitException("Cannot mark limit to account [{0}].\nMarking limits prohibited, limit status =[{1}]",
                        account.AccountID,
                        limistatus);

                if (limistatus == AccountLimitStatus.LimitsAllowed && AvailableBalanceAfterApplyingLimit < 0)
                    throw new LimitException("Cannot block funds[{0}] on Acount[{1}]. There are not enough funds to block. Available balance[{2}] is below zero. ",
                        amount, account.AccountID, AvailableBalanceAfterApplyingLimit);

                // Data access component declarations.
                AccountDAC accountDAC = new AccountDAC();
                account.Limit += amount;
                accountDAC.UpdateById(account);

                scope.Complete();
            }
        }
        #endregion "Limits"



        #region ValueDated Transactions
        public List<ValueDatedTransaction> GetValueDatedTransactionByDate(DateTime date)
        {
            ValueDatedTransactionDAC vDac = new ValueDatedTransactionDAC();
            return vDac.SelectByValueDate(date.Date);
        }

        /// <summary>
        /// Clear Effects - . 
        /// </summary>
        /// <param name="account">A account value.</param>
        /// 
        public void ClearEffects(DateTime date)
        {
            foreach (var txn in GetValueDatedTransactionByDate(date))
            {
                ClearEffects(txn);
            }
        }
        public void ClearEffects(ValueDatedTransaction txn)
        {
            ClearEffectsByID(txn.AccountID, txn.Amount);
        }
        public void ClearEffects(Account account, decimal amount)
        {
            // Step 1 - Avail uncleared funds

            //Handle as ACID
            using (System.Transactions.TransactionScope scope = new TX.TransactionScope())
            {
                // Clear by increasing cleared balance.
                AccountDAC accountDAC = new AccountDAC();
                account.ClearedBalance += amount;
                accountDAC.UpdateById(account);
                scope.Complete();
            }
        }

        public void ClearEffectsByID(int accountID, decimal amount)
        {
            ClearEffects(GetAccount(accountID), amount);
        }
        #endregion

        #region "AccountStatus"
        public void SetAccountLimitStatus(Account acc, AccountLimitStatus status)
        {
            acc.LimitFlag = (short)status;
            AccountDAC acDAC = new AccountDAC();
            acDAC.UpdateById(acc);
        }

        public void SetAccountLockStatus(Account acc, PassFlag status)
        {
            acc.PassFlag = (short)status;
            AccountDAC acDAC = new AccountDAC();
            acDAC.UpdateById(acc);
        }

        #endregion "AccountStatus"
        #endregion "Static Transactions on Accounts"

        #region "Enquiry"

        #region "Accounts Status"
        /// <summary>
        /// GetAccountLockStatus business method. 
        /// </summary>
        /// <param name="accountID">A accountID value.</param>
        public PassFlag GetAccountLockStatus(int accountID)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();

            // Step 1 - Calling SelectById on AccountDAC.
            Account act = accountDAC.SelectById(accountID);
            return GetAccountLockStatus(act);
        }

        /// <summary>
        /// GetAccountLockStatus business method. 
        /// </summary>
        /// <param name="accountID">A accountID value.</param>
        public PassFlag GetAccountLockStatus(Account act)
        {
            PassFlag status = PassFlag.Ok;
            status = (PassFlag)act.PassFlag;
            return status;
        }
        /// <summary>
        /// GetAccountStatus business method. 
        /// </summary>
        /// <param name="accountID">A accountID value.</param>
        public AccountLimitStatus GetAccountLimitStatus(int accountID)
        {
            // Data access component declarations.
            AccountDAC accountDAC = new AccountDAC();

            // Step 1 - Calling SelectById on AccountDAC.
            Account act = accountDAC.SelectById(accountID);
            return GetAccountLimitStatus(act);
        }

        /// <summary>
        /// GetAccountStatus business method. 
        /// </summary>
        /// <param name="accountID">A accountID value.</param>
        public AccountLimitStatus GetAccountLimitStatus(Account act)
        {

            AccountLimitStatus status = AccountLimitStatus.Ok;
            status = (AccountLimitStatus)act.LimitFlag;
            return status;
        }
        #endregion "Accounts Status"

        #region "Account Gets"

        public decimal GetAccountLimitAmount(int accountId)
        {
            AccountDAC accountDAC = new AccountDAC();
            Account act = accountDAC.SelectById(accountId);
            return act.Limit;
        }
        public decimal GetAvailableBalance(int accountID)
        {
            return GetAvailableBalance(GetAccount(accountID));
        }
        public decimal GetAvailableBalance(Account account)
        {
            return account.ClearedBalance - account.Limit;
        }
        public decimal GetClearedBalance(Account account)
        {
            return account.ClearedBalance;
        }
        public decimal GetClearedBalance(int accountID)
        {
            return GetClearedBalance(GetAccount(accountID));
        }
        public decimal GetBookBalance(Account account)
        {
            return account.BookBalance;
        }
        public decimal GetBookBalance(int accountID)
        {
            return GetBookBalance(GetAccount(accountID));
        }
        public Account GetAccount(int accountID)
        {
            AccountDAC accDac = new AccountDAC();

            return accDac.SelectById(accountID);
        }

        public decimal GetAccountViewBalance(int accountId, DateTime startDate)
        {
            TransactionDAC tDac = new TransactionDAC();
            return tDac.SumTransactionsBeforeDate(accountId, startDate);

        }
        public List<Account> GetAllAccounts()
        {
            AccountDAC accDac = new AccountDAC();
            return accDac.Select();
        }
        public List<Account> GetAccountsByCustomer(int CustomerId)
        {
            AccountDAC accDac = new AccountDAC();
            return accDac.SelectByCustomer(CustomerId);
        }
        public List<Account> GetAccountsByMember(int MemberId)
        {
            AccountDAC accDac = new AccountDAC();
            return accDac.SelectByCustomer(GetCustomerByMember(MemberId).CustomerId);
        }

        public List<Account> GetOpenAccounts()
        {
            AccountDAC accDac = new AccountDAC();
            return accDac.SelectOpenAccounts();
        }
        public List<Account> GetClosedAccounts()
        {
            AccountDAC accDac = new AccountDAC();
            return accDac.SelectClosedAccounts();
        }
        public bool IsAccountClosed(int accountid)
        {
            AccountDAC accountDAC = new AccountDAC();
            Account act = accountDAC.SelectById(accountid);
            return act.Closed;
        }

        public bool AccountExists(int AccountID)
        {
            AccountDAC accountDAC = new AccountDAC();
            return accountDAC.CountByID(AccountID) > 0;
        }

        #endregion "Account Gets"

        #region "TransactionType"
        public TransactionType CreateTransactionType(TransactionType TransactionType)
        {
            // Data access component declarations.
            TransactionTypeDAC TransactionTypeDAC = new TransactionTypeDAC();

            // Step 1 - Calling Create on TransactionTypeDAC.
            TransactionType returnedTransactionType = TransactionTypeDAC.Create(TransactionType);
            return returnedTransactionType;
        }
        public TransactionType UpdateTransactionType(TransactionType TransactionType)
        {
            // Data access component declarations.
            TransactionTypeDAC TransactionTypeDAC = new TransactionTypeDAC();

            // Step 1 - Calling Create on TransactionTypeDAC.
            TransactionTypeDAC.UpdateById(TransactionType);
            return TransactionType;
        }
        public List<TransactionType> GetAllTransactionTypes()
        {
            TransactionTypeDAC ttDac = new TransactionTypeDAC();
            return ttDac.Select();
        }
        public TransactionType GetTransactionType(int id)
        {
            TransactionTypeDAC ttDac = new TransactionTypeDAC();
            return ttDac.SelectById(id);
        }
        public List<TieredDet> GetTieredTable(int tieredID)
        {
            TransactionTypeDAC ttDac = new TransactionTypeDAC();
            return ttDac.SelectTieredTable(tieredID);
        }
        #endregion "TransactionType"

        #region "Customers"
        public Customer GetCustomerByMember(int MemberId)
        {
            CustomerDAC csdac = new CustomerDAC();
            return csdac.SelectByMember(MemberId);
        }
        public Customer GetCustomerByEmail(string email)
        {
            CustomerDAC csdac = new CustomerDAC();
            return csdac.SelectByEmail(email);
        }
        public Customer GetCustomerByID(int Id)
        {
            CustomerDAC csdac = new CustomerDAC();
            return csdac.SelectById(Id);
        }
        public List<Customer> GetAllCustomers()
        {
            CustomerDAC csdac = new CustomerDAC();
            return csdac.Select();
        }

        #endregion "Customers"

        #region  "Transactions"
        public List<Transaction> GetAllTransactions()
        {
            TransactionDAC tdac = new TransactionDAC();
            return tdac.Select();
        }
        public List<Transaction> GetAccountTransactions(int _accountid)
        {
            TransactionDAC tdac = new TransactionDAC();
            var _accountTxnsquery = from tx in tdac.Select()
                                    where tx.AccountID == _accountid
                                    select tx;
            List<Transaction> _accountTxns = _accountTxnsquery.ToList();
            return _accountTxns;
        }

        public List<Transaction> GetAccountTransactionsByDate(int accountId, DateTime startDate, DateTime endtDate)
        {
            TransactionDAC tDac = new TransactionDAC();
            return tDac.SelectByAccountDateRange(accountId, startDate, endtDate);
        }
        public List<Transaction> SelectLastTranscations(int accountID, int Take)
        {

            TransactionDAC tDac = new TransactionDAC();
            List<Transaction> txns = tDac.SelectLastTranscations(accountID, Take);

            return txns;
        }
        public List<Transaction> SelectByAccountDateRange(int accountID, DateTime startDate, DateTime endDate)
        {

            TransactionDAC tDac = new TransactionDAC();
            List<Transaction> txns = tDac.SelectByAccountDateRange(accountID, startDate, endDate);

            return txns;
        }
        public List<TransactionModel> ConvertTransactionsToTransactionsModel(List<Transaction> txns)
        {
            List<TransactionModel> txnsView = new List<TransactionModel>();

            TransactionDAC tDac = new TransactionDAC();
             

            TransactionModel first = new TransactionModel();
            first.PostDate = DateTime.Now;
            first.TransactionID = -1;
            first.Narrative = "BALANCE B/F";

            decimal amount = 0M;
            if (amount > 0)
            {
                first.Credit = amount;
                first.Debit = 0;
            }
            else
            {
                first.Credit = 0;
                first.Debit = amount;
            }
            first.Balance = amount;

            //add to view
            txnsView.Add(first);

            //go through the transactins and compute running balance
            decimal bal = amount;
            foreach (var txn in txns)
            {
                TransactionModel txnv = new TransactionModel();
                txnv.PostDate = txn.PostDate;
                txnv.TransactionID = txn.TransactionID;
                txnv.Narrative = txn.Narrative;
                txnv.Amount = txn.Amount;
                txnv.ContraReference = txn.ContraReference;

                if (txn.Amount > 0)
                {
                    txnv.Credit = txn.Amount;
                    txnv.Debit = 0;
                }
                else
                {
                    txnv.Credit = 0;
                    txnv.Debit = txn.Amount;
                }

                bal += txn.Amount;
                txnv.Balance = bal;

                //add to view
                txnsView.Add(txnv);

            }
            return txnsView;
        }
        public List<TransactionModel> GetAccountViewTransactionsByDate(int accountId, DateTime startDate, DateTime endtDate)
        {
            List<TransactionModel> txnsView = new List<TransactionModel>();

            TransactionDAC tDac = new TransactionDAC();
            List<Transaction> txns = tDac.SelectByAccountDateRange(accountId, startDate, endtDate);

            TransactionModel first = new TransactionModel();
            first.PostDate = DateTime.Now;
            first.TransactionID = -1;
            first.Narrative = "BALANCE B/F";

            decimal amount = tDac.SumTransactionsBeforeDate(accountId, startDate);
            if (amount > 0)
            {
                first.Credit = amount;
                first.Debit = 0;
            }
            else
            {
                first.Credit = 0;
                first.Debit = amount;
            }
            first.Balance = amount;

            //add to view
            txnsView.Add(first);

            //go through the transactins and compute running balance
            decimal bal = amount;
            foreach (var txn in txns)
            {
                TransactionModel txnv = new TransactionModel();
                txnv.PostDate = txn.PostDate;
                txnv.TransactionID = txn.TransactionID;
                txnv.Narrative = txn.Narrative;
                txnv.Amount = txn.Amount;
                txnv.ContraReference = txn.ContraReference;

                if (txn.Amount > 0)
                {
                    txnv.Credit = txn.Amount;
                    txnv.Debit = 0;
                }
                else
                {
                    txnv.Credit = 0;
                    txnv.Debit = txn.Amount;
                }

                bal += txn.Amount;
                txnv.Balance = bal;

                //add to view
                txnsView.Add(txnv);

            }
            return txnsView;
        }
        public List<TransactionModel> GetViewAllTransactions()
        {
            List<TransactionModel> txnsView = new List<TransactionModel>();

            TransactionDAC tDac = new TransactionDAC();
            List<Transaction> txns = tDac.SelectAllTxn();

            TransactionModel first = new TransactionModel();
            first.PostDate = DateTime.Now;
            first.TransactionID = -1;
            first.Narrative = "BALANCE B/F";

            decimal amount = 0M;
            if (amount > 0)
            {
                first.Credit = amount;
                first.Debit = 0;
            }
            else
            {
                first.Credit = 0;
                first.Debit = amount;
            }
            first.Balance = amount;

            //add to view
            txnsView.Add(first);

            //go through the transactins and compute running balance
            decimal bal = amount;
            foreach (var txn in txns)
            {
                TransactionModel txnv = new TransactionModel();
                txnv.PostDate = txn.PostDate;
                txnv.TransactionID = txn.TransactionID;
                txnv.Narrative = txn.Narrative;
                txnv.Amount = txn.Amount;
                txnv.ContraReference = txn.ContraReference;

                if (txn.Amount > 0)
                {
                    txnv.Credit = txn.Amount;
                    txnv.Debit = 0;
                }
                else
                {
                    txnv.Credit = 0;
                    txnv.Debit = txn.Amount;
                }

                bal += txn.Amount;
                txnv.Balance = bal;

                //add to view
                txnsView.Add(txnv);

            }
            return txnsView;
        }
        public List<TransactionModel> SelectByContraReferenceRange(string ContraReference)
        {
            List<TransactionModel> txnsView = new List<TransactionModel>();

            TransactionDAC tDac = new TransactionDAC();
            List<Transaction> txns = tDac.SelectByContraReferenceRange(ContraReference);

            TransactionModel first = new TransactionModel();
            first.PostDate = DateTime.Now;
            first.TransactionID = -1;
            first.Narrative = "BALANCE B/F";

            decimal amount = tDac.SumTransactionsByContraReference(ContraReference);
            if (amount > 0)
            {
                first.Credit = amount;
                first.Debit = 0;
            }
            else
            {
                first.Credit = 0;
                first.Debit = amount;
            }
            first.Balance = amount;

            //add to view
            txnsView.Add(first);

            //go through the transactins and compute running balance
            decimal bal = amount;
            foreach (var txn in txns)
            {
                TransactionModel txnv = new TransactionModel();
                txnv.PostDate = txn.PostDate;
                txnv.TransactionID = txn.TransactionID;
                txnv.Narrative = txn.Narrative;
                txnv.Amount = txn.Amount;
                txnv.ContraReference = txn.ContraReference;

                if (txn.Amount > 0)
                {
                    txnv.Credit = txn.Amount;
                    txnv.Debit = 0;
                }
                else
                {
                    txnv.Credit = 0;
                    txnv.Debit = txn.Amount;
                }

                bal += txn.Amount;
                txnv.Balance = bal;

                //add to view
                txnsView.Add(txnv);

            }
            return txnsView;
        }

        #endregion "Transactions"

        #region "AccountTypes"
        public AccountType CreateAccountType(AccountType acctype)
        {
            AccountTypeDAC atdac = new AccountTypeDAC();
            return atdac.Create(acctype);
        }
        public void UpdateAccountTypeByID(AccountType acctype)
        {
            AccountTypeDAC atdac = new AccountTypeDAC();
            atdac.UpdateById(acctype);
        }
        public void DeleteAccountTypeById(int Id)
        {
            AccountTypeDAC atdac = new AccountTypeDAC();
            atdac.DeleteById(Id);
        }
        public List<AccountType> GetAllAccountTypes()
        {
            AccountTypeDAC atdac = new AccountTypeDAC();
            return atdac.Select();
        }
        #endregion "AccountTypes"

        #region "COAs"
        public COA CreateCOA(COA COA)
        {
            COADAC coadac = new COADAC();
            return coadac.Create(COA);
        }
        public void UpdateCOA(COA COA)
        {
            COADAC coadac = new COADAC();
            coadac.UpdateById(COA);
        }
        public void DeleteCOA(int Id)
        {
            COADAC coadac = new COADAC();
            coadac.DeleteById(Id);
        }
        public List<COA> GetAllCOAs()
        {
            COADAC coadac = new COADAC();
            return coadac.Select();
        }
        #endregion "COAs"

        #endregion "Enquiry"

        #region STO
        public void CreateSTO(STO sto)
        {
            STODAC sDac = new STODAC();
            sDac.Create(sto);
        }
        public void UpdateSTO(STO sto)
        {
            STODAC sDac = new STODAC();
            sDac.UpdateById(sto);
        }
        public List<STO> SelectSTOByDateFrom(DateTime date)
        {
            STODAC sDac = new STODAC();
            return sDac.SelectSTOByDateFrom(date);
        }
        public List<STO> SelectSTOByMember(int MemberId)
        {
            STODAC sDac = new STODAC();
            return sDac.SelectDrSTOByMemberId(MemberId);
        }
        public List<STO> SelectSTOByAdmin()
        {
            STODAC sDac = new STODAC();
            return sDac.Select();
        }
        #endregion

        #region TieredTable
        public TieredTable CreateTieredTable(TieredTable tt)
        {
            TieredTableDAC ttDac = new TieredTableDAC();
           return  ttDac.Create(tt);
        }
        public void UpdateTieredTable(TieredTable tt)
        {
            TieredTableDAC ttDac = new TieredTableDAC();
            ttDac.UpdateById(tt);
        }
        public void DeleteTieredTableById(int Id)
        {
            TieredTableDAC ttDac = new TieredTableDAC();
            ttDac.DeleteById(Id);
        }
        public List<TieredTable> SelectTieredTables()
        {
            TieredTableDAC ttDac = new TieredTableDAC();
            return ttDac.Select();
        }
        public TieredTable SelectTieredTableById(int Id)
        {
            TieredTableDAC ttDac = new TieredTableDAC();
            return ttDac.SelectById(Id);
        }
        #endregion

        #region TieredDet
        public TieredDet CreateTieredDet(TieredDet tdet)
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            return tdetDac.Create(tdet);
        }
        public void UpdateTieredDet(TieredDet tdet)
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            tdetDac.UpdateById(tdet);
        }
        public void DeleteTieredDetById(int Id)
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            tdetDac.DeleteById(Id);
        }
        public List<TieredDet> SelectTieredDets()
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            return tdetDac.Select();
        }
        public TieredDet SelectTieredDetById(int Id)
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            return tdetDac.SelectById(Id);
        }
        public List<TieredDet> SelectTableTieredDets(int Id)
        {
            TieredDetDAC tdetDac = new TieredDetDAC();
            return tdetDac.Select().Where(i=>i.TieredID == Id).ToList();
        }
        #endregion



    }
}
