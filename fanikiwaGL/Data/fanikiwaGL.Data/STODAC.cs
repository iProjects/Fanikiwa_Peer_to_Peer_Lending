//====================================================================================================
// Base code generated with Momentum: DAC Gen (Build 2.5.5049.15162)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by Administrator at SAPSERVER on 01/09/2015 19:09:40 
//====================================================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using fanikiwaGL.Entities;

namespace fanikiwaGL.Data
{
    /// <summary>
    /// STO data access component. Manages CRUD operations for the STO table.
    /// </summary>
    public partial class STODAC : DataAccessComponent
    {
        /// <summary>
        /// Inserts a new row in the STO table.
        /// </summary>
        /// <param name="sTO">A STO object.</param>
        /// <returns>An updated STO object.</returns>
        public STO Create(STO sTO)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.STO ([AmountPaid], [PayAmount], [TotalToPay], [DrAccount], [CrAccount], [NextPayDate], [CreateDate], [StartDate], [Interval], [NoOfPayments], [NoOfPaymentsMade], [NoOfDefaults], [AmountDefaulted], [EndDate], [STOType], [ChargeCommFlag], [CommFreqFlag], [CommSourceFlag], [ChargeWho], [CommissionAccount], [CommissionPaidFlag], [CommissionAmount], [DrTxnType], [CrTxnType], [LimitFlag], [PartialPay], [LoanId], [STOAccType]) " +
                "VALUES(@AmountPaid, @PayAmount, @TotalToPay, @DrAccount, @CrAccount, @NextPayDate, @CreateDate, @StartDate, @Interval, @NoOfPayments, @NoOfPaymentsMade, @NoOfDefaults, @AmountDefaulted, @EndDate, @STOType, @ChargeCommFlag, @CommFreqFlag, @CommSourceFlag, @ChargeWho, @CommissionAccount, @CommissionPaidFlag, @CommissionAmount, @DrTxnType, @CrTxnType, @LimitFlag, @PartialPay, @LoanId, @STOAccType); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@AmountPaid", DbType.Currency, sTO.AmountPaid);
                db.AddInParameter(cmd, "@PayAmount", DbType.Currency, sTO.PayAmount);
                db.AddInParameter(cmd, "@TotalToPay", DbType.Currency, sTO.TotalToPay);
                db.AddInParameter(cmd, "@DrAccount", DbType.Int32, sTO.DrAccount);
                db.AddInParameter(cmd, "@CrAccount", DbType.Int32, sTO.CrAccount);
                db.AddInParameter(cmd, "@NextPayDate", DbType.DateTime2, sTO.NextPayDate);
                db.AddInParameter(cmd, "@CreateDate", DbType.DateTime2, sTO.CreateDate);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime2, sTO.StartDate);
                db.AddInParameter(cmd, "@Interval", DbType.AnsiString, sTO.Interval);
                db.AddInParameter(cmd, "@NoOfPayments", DbType.Int32, sTO.NoOfPayments);
                db.AddInParameter(cmd, "@NoOfPaymentsMade", DbType.Int32, sTO.NoOfPaymentsMade);
                db.AddInParameter(cmd, "@NoOfDefaults", DbType.Int32, sTO.NoOfDefaults);
                db.AddInParameter(cmd, "@AmountDefaulted", DbType.Currency, sTO.AmountDefaulted);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime2, sTO.EndDate);
                db.AddInParameter(cmd, "@STOType", DbType.Int32, sTO.STOType);
                db.AddInParameter(cmd, "@ChargeCommFlag", DbType.Boolean, sTO.ChargeCommFlag);
                db.AddInParameter(cmd, "@CommFreqFlag", DbType.Int16, sTO.CommFreqFlag);
                db.AddInParameter(cmd, "@CommSourceFlag", DbType.Int16, sTO.CommSourceFlag);
                db.AddInParameter(cmd, "@ChargeWho", DbType.Int16, sTO.ChargeWho);
                db.AddInParameter(cmd, "@CommissionAccount", DbType.Int32, sTO.CommissionAccount);
                db.AddInParameter(cmd, "@CommissionPaidFlag", DbType.Boolean, sTO.CommissionPaidFlag);
                db.AddInParameter(cmd, "@CommissionAmount", DbType.Decimal, sTO.CommissionAmount);
                db.AddInParameter(cmd, "@DrTxnType", DbType.Int32, sTO.DrTxnType);
                db.AddInParameter(cmd, "@CrTxnType", DbType.Int32, sTO.CrTxnType);
                db.AddInParameter(cmd, "@LimitFlag", DbType.Int32, sTO.LimitFlag);
                db.AddInParameter(cmd, "@PartialPay", DbType.Boolean, sTO.PartialPay);
                db.AddInParameter(cmd, "@LoanId", DbType.Int32, sTO.LoanId);
                db.AddInParameter(cmd, "@STOAccType", DbType.Int32, sTO.STOAccType);

                // Get the primary key value.
                sTO.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return sTO;
        }

        /// <summary>
        /// Updates an existing row in the STO table.
        /// </summary>
        /// <param name="sTO">A STO entity object.</param>
        public void UpdateById(STO sTO)
        {
            const string SQL_STATEMENT =
                "UPDATE dbo.STO " +
                "SET " +
                    "[AmountPaid]=@AmountPaid, " +
                    "[PayAmount]=@PayAmount, " +
                    "[TotalToPay]=@TotalToPay, " +
                    "[DrAccount]=@DrAccount, " +
                    "[CrAccount]=@CrAccount, " +
                    "[NextPayDate]=@NextPayDate, " +
                    "[CreateDate]=@CreateDate, " +
                    "[StartDate]=@StartDate, " +
                    "[Interval]=@Interval, " +
                    "[NoOfPayments]=@NoOfPayments, " +
                    "[NoOfPaymentsMade]=@NoOfPaymentsMade, " +
                    "[NoOfDefaults]=@NoOfDefaults, " +
                    "[AmountDefaulted]=@AmountDefaulted, " +
                    "[EndDate]=@EndDate, " +
                    "[STOType]=@STOType, " +
                    "[ChargeCommFlag]=@ChargeCommFlag, " +
                    "[CommFreqFlag]=@CommFreqFlag, " +
                    "[CommSourceFlag]=@CommSourceFlag, " +
                    "[ChargeWho]=@ChargeWho, " +
                    "[CommissionAccount]=@CommissionAccount, " +
                    "[CommissionPaidFlag]=@CommissionPaidFlag, " +
                    "[CommissionAmount]=@CommissionAmount, " +
                    "[DrTxnType]=@DrTxnType, " +
                    "[CrTxnType]=@CrTxnType, " +
                    "[LimitFlag]=@LimitFlag, " +
                    "[PartialPay]=@PartialPay, " +
                    "[LoanId]=@LoanId, " +
                    "[STOAccType]=@STOAccType " +
                "WHERE [Id]=@Id ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@AmountPaid", DbType.Currency, sTO.AmountPaid);
                db.AddInParameter(cmd, "@PayAmount", DbType.Currency, sTO.PayAmount);
                db.AddInParameter(cmd, "@TotalToPay", DbType.Currency, sTO.TotalToPay);
                db.AddInParameter(cmd, "@DrAccount", DbType.Int32, sTO.DrAccount);
                db.AddInParameter(cmd, "@CrAccount", DbType.Int32, sTO.CrAccount);
                db.AddInParameter(cmd, "@NextPayDate", DbType.DateTime2, sTO.NextPayDate);
                db.AddInParameter(cmd, "@CreateDate", DbType.DateTime2, sTO.CreateDate);
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime2, sTO.StartDate);
                db.AddInParameter(cmd, "@Interval", DbType.AnsiString, sTO.Interval);
                db.AddInParameter(cmd, "@NoOfPayments", DbType.Int32, sTO.NoOfPayments);
                db.AddInParameter(cmd, "@NoOfPaymentsMade", DbType.Int32, sTO.NoOfPaymentsMade);
                db.AddInParameter(cmd, "@NoOfDefaults", DbType.Int32, sTO.NoOfDefaults);
                db.AddInParameter(cmd, "@AmountDefaulted", DbType.Currency, sTO.AmountDefaulted);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime2, sTO.EndDate);
                db.AddInParameter(cmd, "@STOType", DbType.Int32, sTO.STOType);
                db.AddInParameter(cmd, "@ChargeCommFlag", DbType.Boolean, sTO.ChargeCommFlag);
                db.AddInParameter(cmd, "@CommFreqFlag", DbType.Int16, sTO.CommFreqFlag);
                db.AddInParameter(cmd, "@CommSourceFlag", DbType.Int16, sTO.CommSourceFlag);
                db.AddInParameter(cmd, "@ChargeWho", DbType.Int16, sTO.ChargeWho);
                db.AddInParameter(cmd, "@CommissionAccount", DbType.Int32, sTO.CommissionAccount);
                db.AddInParameter(cmd, "@CommissionPaidFlag", DbType.Boolean, sTO.CommissionPaidFlag);
                db.AddInParameter(cmd, "@CommissionAmount", DbType.Decimal, sTO.CommissionAmount);
                db.AddInParameter(cmd, "@DrTxnType", DbType.Int32, sTO.DrTxnType);
                db.AddInParameter(cmd, "@CrTxnType", DbType.Int32, sTO.CrTxnType);
                db.AddInParameter(cmd, "@LimitFlag", DbType.Int32, sTO.LimitFlag);
                db.AddInParameter(cmd, "@PartialPay", DbType.Boolean, sTO.PartialPay);
                db.AddInParameter(cmd, "@LoanId", DbType.Int32, sTO.LoanId);
                db.AddInParameter(cmd, "@STOAccType", DbType.Int32, sTO.STOAccType);
                db.AddInParameter(cmd, "@Id", DbType.Int32, sTO.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Conditionally deletes one or more rows in the STO table.
        /// </summary>
        /// <param name="id">A id value.</param>
        public void DeleteById(int id)
        {
            const string SQL_STATEMENT = "DELETE dbo.STO " +
                                         "WHERE [Id]=@Id ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);


                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Returns a row from the STO table.
        /// </summary>
        /// <param name="id">A Id value.</param>
        /// <returns>A STO object with data populated from the database.</returns>
        public STO SelectById(int id)
        {
            const string SQL_STATEMENT =
                "SELECT [Id], [AmountPaid], [PayAmount], [TotalToPay], [DrAccount], [CrAccount], [NextPayDate]" +
                        ", [CreateDate], [StartDate], [Interval], [NoOfPayments], [NoOfPaymentsMade], [NoOfDefaults]" +
                        ", [AmountDefaulted], [EndDate], [STOType], [ChargeCommFlag], [CommFreqFlag], [CommSourceFlag]" +
                        ", [ChargeWho], [CommissionAccount], [CommissionPaidFlag], [CommissionAmount], [DrTxnType]" +
                        ", [CrTxnType], [LimitFlag], [PartialPay], [LoanId], [STOAccType] " +
                "FROM dbo.STO  " +
                "WHERE [Id]=@Id ";

            STO sTO = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new STO
                        sTO = new STO();

                        // Read values.
                        sTO.Id = base.GetDataValue<int>(dr, "Id");
                        sTO.AmountPaid = base.GetDataValue<decimal>(dr, "AmountPaid");
                        sTO.PayAmount = base.GetDataValue<decimal>(dr, "PayAmount");
                        sTO.TotalToPay = base.GetDataValue<decimal>(dr, "TotalToPay");
                        sTO.DrAccount = base.GetDataValue<int>(dr, "DrAccount");
                        sTO.CrAccount = base.GetDataValue<int>(dr, "CrAccount");
                        sTO.NextPayDate = base.GetDataValue<DateTime>(dr, "NextPayDate");
                        sTO.CreateDate = base.GetDataValue<DateTime>(dr, "CreateDate");
                        sTO.StartDate = base.GetDataValue<DateTime>(dr, "StartDate");
                        sTO.Interval = base.GetDataValue<string>(dr, "Interval");
                        sTO.NoOfPayments = base.GetDataValue<int>(dr, "NoOfPayments");
                        sTO.NoOfPaymentsMade = base.GetDataValue<int>(dr, "NoOfPaymentsMade");
                        sTO.NoOfDefaults = base.GetDataValue<int>(dr, "NoOfDefaults");
                        sTO.AmountDefaulted = base.GetDataValue<decimal>(dr, "AmountDefaulted");
                        sTO.EndDate = base.GetDataValue<DateTime>(dr, "EndDate");
                        sTO.STOType = base.GetDataValue<int>(dr, "STOType");
                        sTO.ChargeCommFlag = base.GetDataValue<bool>(dr, "ChargeCommFlag");
                        sTO.CommFreqFlag = base.GetDataValue<short>(dr, "CommFreqFlag");
                        sTO.CommSourceFlag = base.GetDataValue<short>(dr, "CommSourceFlag");
                        sTO.ChargeWho = base.GetDataValue<short>(dr, "ChargeWho");
                        sTO.CommissionAccount = base.GetDataValue<int>(dr, "CommissionAccount");
                        sTO.CommissionPaidFlag = base.GetDataValue<bool>(dr, "CommissionPaidFlag");
                        sTO.CommissionAmount = base.GetDataValue<decimal>(dr, "CommissionAmount");
                        sTO.DrTxnType = base.GetDataValue<int>(dr, "DrTxnType");
                        sTO.CrTxnType = base.GetDataValue<int>(dr, "CrTxnType");
                        sTO.LimitFlag = base.GetDataValue<int>(dr, "LimitFlag");
                        sTO.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");
                        sTO.LoanId = base.GetDataValue<int>(dr, "LoanId");
                        sTO.STOAccType = base.GetDataValue<int>(dr, "STOAccType");
                    }
                }
            }

            return sTO;
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the STO table.
        /// </summary>
        /// <returns>A collection of STO objects.</returns>		
        public List<STO> Select()
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                "SELECT [Id], [AmountPaid], [PayAmount], [TotalToPay], [DrAccount], [CrAccount], [NextPayDate]" +
                        ", [CreateDate], [StartDate], [Interval], [NoOfPayments], [NoOfPaymentsMade], [NoOfDefaults]" +
                        ", [AmountDefaulted], [EndDate], [STOType], [ChargeCommFlag], [CommFreqFlag], [CommSourceFlag]" +
                        ", [ChargeWho], [CommissionAccount], [CommissionPaidFlag], [CommissionAmount], [DrTxnType]" +
                        ", [CrTxnType], [LimitFlag], [PartialPay], [LoanId], [STOAccType] " +
                "FROM dbo.STO ";

            List<STO> result = new List<STO>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new STO
                        STO sTO = new STO();

                        // Read values.
                        sTO.Id = base.GetDataValue<int>(dr, "Id");
                        sTO.AmountPaid = base.GetDataValue<decimal>(dr, "AmountPaid");
                        sTO.PayAmount = base.GetDataValue<decimal>(dr, "PayAmount");
                        sTO.TotalToPay = base.GetDataValue<decimal>(dr, "TotalToPay");
                        sTO.DrAccount = base.GetDataValue<int>(dr, "DrAccount");
                        sTO.CrAccount = base.GetDataValue<int>(dr, "CrAccount");
                        sTO.NextPayDate = base.GetDataValue<DateTime>(dr, "NextPayDate");
                        sTO.CreateDate = base.GetDataValue<DateTime>(dr, "CreateDate");
                        sTO.StartDate = base.GetDataValue<DateTime>(dr, "StartDate");
                        sTO.Interval = base.GetDataValue<string>(dr, "Interval");
                        sTO.NoOfPayments = base.GetDataValue<int>(dr, "NoOfPayments");
                        sTO.NoOfPaymentsMade = base.GetDataValue<int>(dr, "NoOfPaymentsMade");
                        sTO.NoOfDefaults = base.GetDataValue<int>(dr, "NoOfDefaults");
                        sTO.AmountDefaulted = base.GetDataValue<decimal>(dr, "AmountDefaulted");
                        sTO.EndDate = base.GetDataValue<DateTime>(dr, "EndDate");
                        sTO.STOType = base.GetDataValue<int>(dr, "STOType");
                        sTO.ChargeCommFlag = base.GetDataValue<bool>(dr, "ChargeCommFlag");
                        sTO.CommFreqFlag = base.GetDataValue<short>(dr, "CommFreqFlag");
                        sTO.CommSourceFlag = base.GetDataValue<short>(dr, "CommSourceFlag");
                        sTO.ChargeWho = base.GetDataValue<short>(dr, "ChargeWho");
                        sTO.CommissionAccount = base.GetDataValue<int>(dr, "CommissionAccount");
                        sTO.CommissionPaidFlag = base.GetDataValue<bool>(dr, "CommissionPaidFlag");
                        sTO.CommissionAmount = base.GetDataValue<decimal>(dr, "CommissionAmount");
                        sTO.DrTxnType = base.GetDataValue<int>(dr, "DrTxnType");
                        sTO.CrTxnType = base.GetDataValue<int>(dr, "CrTxnType");
                        sTO.LimitFlag = base.GetDataValue<int>(dr, "LimitFlag");
                        sTO.PartialPay = base.GetDataValue<bool>(dr, "PartialPay");
                        sTO.LoanId = base.GetDataValue<int>(dr, "LoanId");
                        sTO.STOAccType = base.GetDataValue<int>(dr, "STOAccType");

                        // Add to List.
                        result.Add(sTO);
                    }
                }
            }

            return result;
        }
    }
}

