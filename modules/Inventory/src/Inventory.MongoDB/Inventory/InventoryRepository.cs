using Inventory.MongoDB;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace Inventory.Inventory
{
    public class InventoryRepository : MongoDbRepository<InventoryMongoDbContext, Models.Inventory, Guid>, IInventoryRepository
    {
        public InventoryRepository(IMongoDbContextProvider<InventoryMongoDbContext> contextProvider) : base(contextProvider)
        {
        }

        public async Task<List<Models.Inventory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.Inventory, IMongoQueryable<Models.Inventory>>(
                    !string.IsNullOrEmpty(filter),
                    inventory => !string.IsNullOrEmpty(inventory.ProductName) && inventory.ProductName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.Inventory>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
