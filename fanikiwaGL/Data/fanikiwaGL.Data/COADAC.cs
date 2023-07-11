//====================================================================================================
// Code generated with Momentum: DAC Gen (Build 2.5.4750.27570)
// Layered Architecture Solution Guidance (http://layerguidance.codeplex.com)
//
// Generated by fmuraya at SOFTBOOKSSERVER on 08/24/2013 16:52:15 
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
    /// COA data access component. Manages CRUD operations for the COA table.
    /// </summary>
    public partial class COADAC : DataAccessComponent
    {

        /// <summary>
        /// Inserts a new row in the COA table.
        /// </summary>
        /// <param name="cOA">A COA object.</param>
        /// <returns>An updated COA object.</returns>
        public COA Create(COA cOA)
        {
            const string SQL_STATEMENT =
                "INSERT INTO dbo.COA ([ShortCode], [Description], [COALevel], [Parent], [Rorder]) " +
                "VALUES(@ShortCode, @Description, @COALevel, @Parent, @Rorder); SELECT SCOPE_IDENTITY();";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@ShortCode", DbType.StringFixedLength, cOA.ShortCode);
                db.AddInParameter(cmd, "@Description", DbType.String, cOA.Description);
                db.AddInParameter(cmd, "@COALevel", DbType.Int32, cOA.COALevel);
                db.AddInParameter(cmd, "@Parent", DbType.Int32, cOA.Parent);
                db.AddInParameter(cmd, "@Rorder", DbType.Int32, cOA.Rorder);

                // Get the primary key value.
                cOA.Id = Convert.ToInt32(db.ExecuteScalar(cmd));
            }

            return cOA;
        }

        /// <summary>
        /// Updates an existing row in the COA table.
        /// </summary>
        /// <param name="cOA">A COA entity object.</param>
        public void UpdateById(COA cOA)
        {
            const string SQL_STATEMENT =
                "UPDATE dbo.COA " +
                "SET " +
                    "[ShortCode]=@ShortCode, " +
                    "[Description]=@Description, " +
                    "[COALevel]=@COALevel, " +
                    "[Parent]=@Parent, " +
                    "[Rorder]=@Rorder " +
                "WHERE [Id]=@Id ";

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                // Set parameter values.
                db.AddInParameter(cmd, "@ShortCode", DbType.StringFixedLength, cOA.ShortCode);
                db.AddInParameter(cmd, "@Description", DbType.String, cOA.Description);
                db.AddInParameter(cmd, "@COALevel", DbType.Int32, cOA.COALevel);
                db.AddInParameter(cmd, "@Parent", DbType.Int32, cOA.Parent);
                db.AddInParameter(cmd, "@Rorder", DbType.Int32, cOA.Rorder);
                db.AddInParameter(cmd, "@Id", DbType.Int32, cOA.Id);

                db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// Conditionally deletes one or more rows in the COA table.
        /// </summary>
        /// <param name="id">A id value.</param>
        public void DeleteById(int id)
        {
            const string SQL_STATEMENT = "DELETE dbo.COA " +
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
        /// Returns a row from the COA table.
        /// </summary>
        /// <param name="id">A Id value.</param>
        /// <returns>A COA object with data populated from the database.</returns>
        public COA SelectById(int id)
        {
            const string SQL_STATEMENT =
                "SELECT [Id], [ShortCode], [Description], [COALevel], [Parent], [Rorder] " +
                "FROM dbo.COA  " +
                "WHERE [Id]=@Id ";

            COA cOA = null;

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                    {
                        // Create a new COA
                        cOA = new COA();

                        // Read values.
                        cOA.Id = base.GetDataValue<int>(dr, "Id");
                        cOA.ShortCode = base.GetDataValue<string>(dr, "ShortCode");
                        cOA.Description = base.GetDataValue<string>(dr, "Description");
                        cOA.COALevel = base.GetDataValue<int>(dr, "COALevel");
                        cOA.Parent = base.GetDataValue<int>(dr, "Parent");
                        cOA.Rorder = base.GetDataValue<int>(dr, "Rorder");
                    }
                }
            }

            return cOA;
        }

        /// <summary>
        /// Conditionally retrieves one or more rows from the COA table.
        /// </summary>
        /// <returns>A collection of COA objects.</returns>		
        public List<COA> Select()
        {
            // WARNING! The following SQL query does not contain a WHERE condition.
            // You are advised to include a WHERE condition to prevent any performance
            // issues when querying large resultsets.
            const string SQL_STATEMENT =
                "SELECT [Id], [ShortCode], [Description], [COALevel], [Parent], [Rorder] " +
                "FROM dbo.COA ";

            List<COA> result = new List<COA>();

            // Connect to database.
            Database db = DatabaseFactory.CreateDatabase(CONNECTION_NAME);
            using (DbCommand cmd = db.GetSqlStringCommand(SQL_STATEMENT))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    while (dr.Read())
                    {
                        // Create a new COA
                        COA cOA = new COA();

                        // Read values.
                        cOA.Id = base.GetDataValue<int>(dr, "Id");
                        cOA.ShortCode = base.GetDataValue<string>(dr, "ShortCode");
                        cOA.Description = base.GetDataValue<string>(dr, "Description");
                        cOA.COALevel = base.GetDataValue<int>(dr, "COALevel");
                        cOA.Parent = base.GetDataValue<int>(dr, "Parent");
                        cOA.Rorder = base.GetDataValue<int>(dr, "Rorder");

                        // Add to List.
                        result.Add(cOA);
                    }
                }
            }

            return result;
        }
    }
}