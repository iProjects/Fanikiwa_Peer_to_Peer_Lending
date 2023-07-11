//====================================================================================================
// Code generated with Momentum: DAC Gen (Build 2.5.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 09/26/2013 15:47:20 
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
    /// Customers data access component. Manages CRUD operations for the Customers table.
    /// </summary>
    public partial class CustomerDAC 
    {

        
        /// <summary>
        /// Returns a row from the Customers table.
        /// </summary>
        /// <param name="customerId">A CustomerId value.</param>
        /// <returns>A Customer object with data populated from the database.</returns>
        public Customer SelectByEmail(string email)
        {
            const string SQL_STATEMENT =
                "SELECT [CustomerId], [CustomerNo], [Name], [Address], [Telephone], [Email], [Branch], [BillToName]" +
                        ", [BillToAddress], [BillToTelephone], [BillToEmail], [MemberId], [CreatedDate]  " +
                "FROM dbo.Customers  " +
                "WHERE [Email]=@Email ";

            Customer customer = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Email", DbType.String, email);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new Customer
                        customer = new Customer();

                        // Read values.
                        customer.CustomerId = base.GetDataValue<int>(dr, "CustomerId");
                        customer.CustomerNo = base.GetDataValue<string>(dr, "CustomerNo");
                        customer.Name = base.GetDataValue<string>(dr, "Name");
                        customer.Address = base.GetDataValue<string>(dr, "Address");
                        customer.Telephone = base.GetDataValue<string>(dr, "Telephone");
                        customer.Email = base.GetDataValue<string>(dr, "Email");
                        customer.Branch = base.GetDataValue<string>(dr, "Branch");
                        customer.BillToName = base.GetDataValue<string>(dr, "BillToName");
                        customer.BillToAddress = base.GetDataValue<string>(dr, "BillToAddress");
                        customer.BillToTelephone = base.GetDataValue<string>(dr, "BillToTelephone");
                        customer.BillToEmail = base.GetDataValue<string>(dr, "BillToEmail");
                        customer.CreatedDate = base.GetDataValue<DateTime>(dr, "CreatedDate");
                        customer.MemberId = base.GetDataValue<int>(dr, "MemberId");
                    }
                }
            }

            return customer;
        }

    }
}
