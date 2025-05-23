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
using Abp.eCommerce.Enums;

namespace Order.Order
{
    public class OrderRepository : MongoDbRepository<OrderMongoDbContext, Models.Order, Guid>, IOrderRepository
    {
        public OrderRepository(IMongoDbContextProvider<OrderMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, OrderStatus? status = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.Order, IMongoQueryable<Models.Order>>(
                    !string.IsNullOrEmpty(filter),
                    order => !string.IsNullOrEmpty(order.Customer.CustomerName) && order.Customer.CustomerName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .WhereIf<Models.Order, IMongoQueryable<Models.Order>>(
                    status.HasValue,
                    order => order.Status == status
                )
                .WhereIf<Models.Order, IMongoQueryable<Models.Order>>(
                    startDate.HasValue,
                    order => order.CreationTime >= startDate.Value
                )
                .WhereIf<Models.Order, IMongoQueryable<Models.Order>>(
                    endDate.HasValue,
                    order => order.CreationTime <= endDate.Value
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.Order>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
