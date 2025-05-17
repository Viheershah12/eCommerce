using MongoDB.Driver.Linq;
using PaymentTransactions.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace PaymentTransactions.PaymentTransaction
{
    public class PaymentTransactionRepository : MongoDbRepository<PaymentTransactionsMongoDbContext, Models.PaymentTransaction, Guid>, IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(IMongoDbContextProvider<PaymentTransactionsMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.PaymentTransaction>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting, string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.PaymentTransaction, IMongoQueryable<Models.PaymentTransaction>>(
                    !string.IsNullOrEmpty(filter),
                    paymentTransaction => !string.IsNullOrEmpty(paymentTransaction.PaymentMethodSystemName) && paymentTransaction.PaymentMethodSystemName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.PaymentTransaction>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
