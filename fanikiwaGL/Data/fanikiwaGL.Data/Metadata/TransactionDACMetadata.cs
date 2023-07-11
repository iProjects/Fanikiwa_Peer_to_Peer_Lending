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
    public partial class TransactionDAC
    {
        public List<Transaction> SelectByContraReferenceRange(string ContraReference)
        {
            return null;
        }
        public decimal SumTransactionsByContraReference(string ContraReference)
        {
            return 0;
        }

        /// <summary>
        /// Returns a row from the Transactions table.
        /// </summary>
        /// <param name="transactionID">A TransactionID value.</param>
        /// <returns>A Transaction object with data populated from the database.</returns>
        public List<Transaction> SelectLastTranscations(int accountID, int Take)
        {
            string SQL_STATEMENT =
                "SELECT TOP " + Take + " * FROM dbo.Transactions  " +
                "WHERE [AccountID]=@accountID " +
                "ORDER BY [PostDate] DESC";

            List<Transaction> result = new List<Transaction>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@accountID", DbType.Int32, accountID);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Transaction
                        Transaction transaction = new Transaction();

                        // Read values.
                        transaction.TransactionID = base.GetDataValue<int>(dr, "TransactionID");
                        transaction.TransactionTypeId = base.GetDataValue<int>(dr, "TransactionTypeId");
                        transaction.AccountID = base.GetDataValue<int>(dr, "AccountID");
                        transaction.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        transaction.PostDate = base.GetDataValue<DateTime>(dr, "PostDate");
                        transaction.RecordDate = base.GetDataValue<DateTime>(dr, "RecordDate");
                        transaction.ValueDate = base.GetDataValue<DateTime>(dr, "ValueDate");
                        transaction.Narrative = base.GetDataValue<string>(dr, "Narrative");
                        transaction.ForcePostFlag = base.GetDataValue<bool>(dr, "ForcePostFlag");
                        transaction.StatementFlag = base.GetDataValue<string>(dr, "StatementFlag");
                        transaction.Authorizer = base.GetDataValue<string>(dr, "Authorizer");
                        transaction.UserID = base.GetDataValue<string>(dr, "UserID");
                        transaction.Reference = base.GetDataValue<string>(dr, "Reference");
                        transaction.ContraReference = base.GetDataValue<string>(dr, "ContraReference");

                        // Add to List.
                        result.Add(transaction);
                    }
                }
            }

            return result;
        }

        public List<Transaction> SelectByAccountDateRange(int accountID, DateTime startDate, DateTime endDate)
        {
            const string SQL_STATEMENT =
                "SELECT * FROM dbo.Transactions  " +
                "WHERE [AccountID]=@accountID " +
                "AND [PostDate]>=@startDate " +
                "AND [PostDate]<=@endDate " +
                "ORDER BY [TransactionID],[PostDate]";

            List<Transaction> result = new List<Transaction>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@accountID", DbType.Int32, accountID);
                db.AddInParameter(cmd, "@startDate", DbType.Date, startDate);
                db.AddInParameter(cmd, "@endDate", DbType.Date, endDate);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Transaction
                        Transaction transaction = new Transaction();

                        // Read values.
                        transaction.TransactionID = base.GetDataValue<int>(dr, "TransactionID");
                        transaction.TransactionTypeId = base.GetDataValue<int>(dr, "TransactionTypeId");
                        transaction.AccountID = base.GetDataValue<int>(dr, "AccountID");
                        transaction.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        transaction.PostDate = base.GetDataValue<DateTime>(dr, "PostDate");
                        transaction.RecordDate = base.GetDataValue<DateTime>(dr, "RecordDate");
                        transaction.ValueDate = base.GetDataValue<DateTime>(dr, "ValueDate");
                        transaction.Narrative = base.GetDataValue<string>(dr, "Narrative");
                        transaction.ForcePostFlag = base.GetDataValue<bool>(dr, "ForcePostFlag");
                        transaction.StatementFlag = base.GetDataValue<string>(dr, "StatementFlag");
                        transaction.Authorizer = base.GetDataValue<string>(dr, "Authorizer");
                        transaction.UserID = base.GetDataValue<string>(dr, "UserID");
                        transaction.Reference = base.GetDataValue<string>(dr, "Reference");
                        transaction.ContraReference = base.GetDataValue<string>(dr, "ContraReference");

                        // Add to List.
                        result.Add(transaction);
                    }
                }
            }

            return result;
        }

        public List<Transaction> SelectAllTxn()
        {
            const string SQL_STATEMENT =
                "SELECT * FROM dbo.Transactions  " + 
                "ORDER BY [TransactionID],[PostDate]";

            List<Transaction> result = new List<Transaction>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            { 

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new Transaction
                        Transaction transaction = new Transaction();

                        // Read values.
                        transaction.TransactionID = base.GetDataValue<int>(dr, "TransactionID");
                        transaction.TransactionTypeId = base.GetDataValue<int>(dr, "TransactionTypeId");
                        transaction.AccountID = base.GetDataValue<int>(dr, "AccountID");
                        transaction.Amount = base.GetDataValue<decimal>(dr, "Amount");
                        transaction.PostDate = base.GetDataValue<DateTime>(dr, "PostDate");
                        transaction.RecordDate = base.GetDataValue<DateTime>(dr, "RecordDate");
                        transaction.ValueDate = base.GetDataValue<DateTime>(dr, "ValueDate");
                        transaction.Narrative = base.GetDataValue<string>(dr, "Narrative");
                        transaction.ForcePostFlag = base.GetDataValue<bool>(dr, "ForcePostFlag");
                        transaction.StatementFlag = base.GetDataValue<string>(dr, "StatementFlag");
                        transaction.Authorizer = base.GetDataValue<string>(dr, "Authorizer");
                        transaction.UserID = base.GetDataValue<string>(dr, "UserID");
                        transaction.Reference = base.GetDataValue<string>(dr, "Reference");
                        transaction.ContraReference = base.GetDataValue<string>(dr, "ContraReference");

                        // Add to List.
                        result.Add(transaction);
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Returns a row from the Transactions table.
        /// </summary>
        /// <param name="transactionID">A TransactionID value.</param>
        /// <returns>A Transaction object with data populated from the database.</returns>
        public decimal  SumTransactionsBeforeDate(int accountID, DateTime startDate)
        {
            const string SQL_STATEMENT =
                " SELECT  SUM([Amount]) AS [TOTAL], [AccountID], [PostDate] " +
                " FROM dbo.Transactions  " +
                " WHERE [AccountID] = @accountID " +
                " AND [PostDate] > @startDate " +
                " GROUP BY [AccountID], [PostDate], [TransactionID] " +
                " ORDER BY [TransactionID], [PostDate] " ;

            decimal total = 0.00M;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@accountID", DbType.Int32, accountID);
                db.AddInParameter(cmd, "@startDate", DbType.Date, startDate);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {

                        int AccountID = base.GetDataValue<int>(dr, "AccountID");
                         total = base.GetDataValue<decimal>(dr, "TOTAL");
                    }
                }
            }

            return total;
        }

    }
}
