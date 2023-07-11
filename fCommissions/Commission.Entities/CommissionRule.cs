using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fCommissions.Commission.Entities
{
    public abstract class CommissionRule
    {
    }

    public class FlatRateRule: CommissionRule
    {
        public int Id;
        public bool Absolute; //Flat rate rule can be absolute or %
        public decimal Rate;

        public FlatRateRule(bool absolute, decimal rate)
        {
            Absolute= absolute;
            Rate = rate;
        }
        public FlatRateRule()
        {

        }
    }

    public class TieredRule : CommissionRule
    {
        public List<LookupRow> LookupTable;

        public TieredRule()
        {

        }
    }
    public class LookupRule : CommissionRule
    {
        //public LookupRow[] LookupTable;
        public List<LookupRow> LookupTable;

        public LookupRule()
        {
        }
    }


    public class LookupRow
    {
        public int Id;
        public decimal Min;
        public decimal Max;
        public decimal Rate;
        public bool Absolute;
       
    }
}
