using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fanikiwaGL.Framework
{
    public enum STOTYPE
    {
        Normal = 0,
        Sweep = 1
    }

    public enum STOCommissionChargeWho
    {
        /*
                    0. Borrower to pay commission
                    1. Investor to pay commission
                    2.  Both to pay commission
                 */
        
        Borrower = 0,
        Investor = 1,
        BothBorrowerAndInvestor = 2
    }

    public enum STOCommFreqFlag
    {
        /*
        NoCommission = 0,
        PayCommPerSTO = 1,
        PayCommissionPerOnce = 2
        */
        NoCommission = 0,
        PayCommPerSTO = 1,
        PayCommissionPerOnce = 2
    }
    public enum STOCommSourceFlag
    {
        /*
        NoCommission = 0,
        PayCommPerSTO = 1,
        PayCommissionPerOnce = 2
        */
        NoCommission = 0,
        STO = 1,
        TransactionType = 2
    }
    public enum PostingLimitCheckFlag //used by STO for posting STO transaction as well as the commission
    { 
        CheckLimitPassFlag = 0, // Checks account limit status and PassFlag
        CheckLimitFlagOnly = 1, // Checks account limit status and not PassFlag
        NoLimitCheck = 2, // does not check limit but checks PassFlag
        ForcePost = 3, //Will not check the limit as well as PassFlag. Equivalent to Transaction.ForcePost
    }
    public enum STOAccType
    {
        Cash = 0,
        Loan = 1
    }

    public enum AccountLimitStatus
    {
        Unknown = -1,
        Ok = 0,

        //LimitCheckFlag - Limit financial transaction Post status
        //thrown as exceptions by the posting component
        PostingNoLimitChecking = 5, //0 - Do not check limit flag
        PostingOverDrawingProhibited = 6,// 1 - Check limit on cleared balance. i.e. Cleared-Limit should not be < 0
        PostingDrawingOnUnclearedEffectsAllowed = 7, // 2 - Check limit on book balance. i.e. book-Limit should not be < 0

        //Limit Flag - Marking Limit status
        LimitsAllowed = 8, //0 - Do not check limit flag
        LimitForAdvanceProhibited = 9, //3 - the account cannot have a negative limit. ie. we cannot advance. i.e limit should not be <0
        LimitForBlockingProhibited = 10, //4 - the account cannot have a +ve limit. ie. we cannot block. i.e limit should not be >0
        AllLimitsProhibited = 11 //5 - All limits prohibited i.e limit = 0
    }

    public enum PassFlag
    {
        Unknown = -1,
        Ok = 0,

        //PassFlag - Lock status
        DebitPostingProhibited = 1,
        CreditPostingProhibited = 2,
        AllPostingProhibited = 3,
        Locked = 4, //even inquiry is prohibited
    }
}
