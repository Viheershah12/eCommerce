using MongoDB.Driver.Linq;
using Order.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace Order.ShoppingCart
{
    public class ShoppingCartRepository : MongoDbRepository<OrderMongoDbContext, Models.ShoppingCart, Guid>, IShoppingCartRepository        
    {
        public ShoppingCartRepository(IMongoDbContextProvider<OrderMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.ShoppingCart>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, Guid? userId = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.ShoppingCart, IMongoQueryable<Models.ShoppingCart>>(
                    !string.IsNullOrEmpty(filter),
                    shoppingCart => !string.IsNullOrEmpty(shoppingCart.Username) && shoppingCart.Username.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .WhereIf<Models.ShoppingCart, IMongoQueryable<Models.ShoppingCart>>(
                    userId.HasValue,
                    shoppingCart => shoppingCart.UserId == userId
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.ShoppingCart>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
