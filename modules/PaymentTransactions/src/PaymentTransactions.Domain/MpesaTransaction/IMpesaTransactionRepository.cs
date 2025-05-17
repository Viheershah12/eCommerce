using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PaymentTransactions.MpesaTransaction
{
    public interface IMpesaTransactionRepository : IRepository<Models.MpesaTransaction, Guid>
    {
        Task<List<Models.MpesaTransaction>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting);
    }
}
