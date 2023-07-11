using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fanikiwaGL.Entities
{
    public interface ITransactionBuilder
    {
        List<Transaction> GetTransactions();
    }


}
