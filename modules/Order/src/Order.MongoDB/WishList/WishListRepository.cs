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

namespace Order.WishList
{
    public class WishListRepository : MongoDbRepository<OrderMongoDbContext, Models.WishList, Guid>, IWishListRepository
    {
        public WishListRepository(IMongoDbContextProvider<OrderMongoDbContext> contextProvider) : base(contextProvider) { }

        public async Task<List<Models.WishList>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, Guid? userId = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.WishList, IMongoQueryable<Models.WishList>>(
                    !string.IsNullOrEmpty(filter),
                    wishList => !string.IsNullOrEmpty(wishList.Username) && wishList.Username.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .WhereIf<Models.WishList, IMongoQueryable<Models.WishList>>(
                    userId.HasValue,
                    wishList => wishList.UserId == userId
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.WishList>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
