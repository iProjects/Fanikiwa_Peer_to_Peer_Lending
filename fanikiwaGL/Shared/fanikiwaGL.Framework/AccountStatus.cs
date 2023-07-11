using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Runtime.Serialization;


namespace fanikiwaGL.Framework
{
    [DataContract]
    public partial class SimulatePostStatus
    {

        public SimulatePostStatus()
        {
            Errors = new List<Exception>();
        }
        /// <summary>
        /// Gets or sets a int value for the TransactionTypeId column.
        /// </summary>
        [DataMember]
        public int TransactionTypeId { get; set; }

        /// <summary>
        /// Gets or sets a int value for the AccountID column.
        /// </summary>
        [DataMember]
        public int AccountID { get; set; }

        /// <summary>
        /// Gets or sets a decimal value for the Amount column.
        /// </summary>
        [DataMember]
        public decimal TransactionAmount { get; set; }

        /// <summary>
        /// Gets or sets a decimal value for the Amount column.
        /// </summary>
        [DataMember]
        public decimal BookBalanceBeforePosting { get; set; }

        /// <summary>
        /// Gets or sets a decimal value for the Amount column.
        /// </summary>
        [DataMember]
        public decimal ClearedBalanceBeforePosting { get; set; }

        /// <summary>
        /// Gets or sets a decimal value for the Amount column.
        /// </summary>
        [DataMember]
        public decimal Limit { get; set; }


        /// <summary>
        /// Gets or sets a limit status.
        /// </summary>
        [DataMember]
        public AccountLimitStatus  LimitStatus{ get; set; }

        /// <summary>
        /// Gets or sets a Blocked status.
        /// </summary>
        [DataMember]
        public PassFlag BlockedStatus { get; set; }

        /// <summary>
        /// Gets or sets a limit status.
        /// </summary>
        [DataMember]
        public List<Exception> Errors { get; set; }

        /// <summary>
        /// Gets or sets a limit status.
        /// </summary>
        [DataMember]
        public bool CanPost { 
            get { 
                return Errors.Count() == 0; 
            } 
        }

    }

}
