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

namespace PaymentTransactions.MpesaTransaction
{
    public class MpesaTransactionRepository : MongoDbRepository<PaymentTransactionsMongoDbContext, Models.MpesaTransaction, Guid>, IMpesaTransactionRepository
    {
        public MpesaTransactionRepository(IMongoDbContextProvider<PaymentTransactionsMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.MpesaTransaction>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.MpesaTransaction>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
