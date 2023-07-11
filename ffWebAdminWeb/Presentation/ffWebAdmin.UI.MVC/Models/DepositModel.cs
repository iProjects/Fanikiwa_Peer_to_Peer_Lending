using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace ffWebAdmin.UI.MVC.Models
{
    public class DepositModel
    {

        [Required(ErrorMessage = "Please enter an email")]
        [RegularExpression("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?",
            ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Member(Use Email Address)")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Please enter your amount")]
        [DataType(DataType.Currency)]
        //[Range(1, 1000000, ErrorMessage = "Amount must be between 1 and 1,000,000")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Display(Name = "Available Balance")]
        public decimal BookBalance { get; set; }

        [Display(Name = "Balance After Deposit")]
        public decimal BalanceAfterDeposit { get; set; }

    }

    public class AccountsDetsModel
    {
        public int LoanAccid { get; set; }

        public int InvestmentAccid { get; set; }

        public int CurrentAccid { get; set; }

        public decimal LoanAcc { get; set; }

        public decimal InvestmentAcc { get; set; }

        public decimal CurrentAcc { get; set; }

    }

    public class MessageStatusModel
    {
        public int TotalMessages { get; set; }

        public int UprocessedMessages { get; set; }

        public int ProcessedMessages { get; set; }

    }

    public class RegisteredMembers
    {
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
    }

    public class ErrorHandlerModel
    {
        public string ExceptionMessage { get; set; }
    }

}