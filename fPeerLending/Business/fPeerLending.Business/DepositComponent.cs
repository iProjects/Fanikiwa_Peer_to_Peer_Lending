//====================================================================================================
// Code generated with Motion: BC Gen (Build 2.2.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 08/03/2013 18:56:09 
//====================================================================================================


using fPeerLending.Data;
using fPeerLending.Entities;
using fPeerLending.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Runtime.Serialization;

using fanikiwaGL.Business;
using fanikiwaGL.Entities;
using fanikiwaGL.Framework;
using fCommon.Utility;
using fCommissions.Commission.Business;
using fMessagingSystem.Entities;


namespace fPeerLending.Business
{
    /// <summary>
    /// Deposit business component.
    /// </summary>
    public partial class DepositComponent
    {
        public List<Transaction> Deposit(int TransactionType,
            int DraccountID,
            int CrAccountID,
            decimal Amount,
            string Narr,
            string reference,
            string UserID,
            string Authorizer)
        {

            FinancialTransactionComponent fc = new FinancialTransactionComponent();
            return fc.CreateTransactionsFromTransactionType(
                  TransactionType,
              DraccountID,
              CrAccountID,
              Amount,
              Narr,
              reference,
              UserID,
              Authorizer);
        }

        public List<Transaction> MpesaDeposit(string DraccountID,
            decimal Amount,
            string Narr,
            string reference)
        {
            StaticTransactionsComponent sPostingClient = new StaticTransactionsComponent();
            int ContraAccId = Config.GetInt("MPESACASHACCOUNT");
            int ttID = Config.GetInt("MPESADEPOSITTRANSACTIONTYPE");
            TransactionType tt = sPostingClient.GetTransactionType(ttID);
            if (tt.DefaultMainAccount != 0) ContraAccId = tt.DefaultMainAccount;

            int DrAccID = Config.GetInt("MPESASUSPENSEACCOUNT");
            int DrCusAccId;
            int.TryParse(DraccountID, out DrCusAccId);
            if(sPostingClient.AccountExists(DrCusAccId))  DrAccID = DrCusAccId;

            return this.Deposit(ttID,
             DrAccID,
            ContraAccId,
             Amount,
             Narr,
             reference,
             "SYS",
             "SYS");
        }
    }



}