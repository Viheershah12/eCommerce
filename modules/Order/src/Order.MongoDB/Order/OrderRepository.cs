using MongoDB.Driver.Linq;
using Order.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace Order.Order
{
    public class OrderRepository : MongoDbRepository<OrderMongoDbContext, Models.Order, Guid>, IOrderRepository
    {
        public OrderRepository(IMongoDbContextProvider<OrderMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.Order, IMongoQueryable<Models.Order>>(
                    !string.IsNullOrEmpty(filter),
                    order => !string.IsNullOrEmpty(order.CustomerName) && order.CustomerName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.Order>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
