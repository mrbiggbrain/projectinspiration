using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectInspiration.SDK.Shared.TransactionService
{
    public interface ITransactionService
    {
        T Post<T, O>(O obj, String controller = null, String action = null, IEnumerable<ITransactionParameter> parama = null);
        T Get<T>(String controller = null, String action = null, IEnumerable<ITransactionParameter> parama = null);
    }
}
