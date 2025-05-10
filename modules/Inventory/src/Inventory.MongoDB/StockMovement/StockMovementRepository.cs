using Inventory.Models;
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

namespace Inventory.StockMovement
{
    public class StockMovementRepository : MongoDbRepository<InventoryMongoDbContext, Models.StockMovement, Guid>, IStockMovementRepository
    {
        public StockMovementRepository(IMongoDbContextProvider<InventoryMongoDbContext> contextProvider) : base(contextProvider)
        {
        }

        public async Task<List<Models.StockMovement>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.StockMovement, IMongoQueryable<Models.StockMovement>>(
                    !string.IsNullOrEmpty(filter),
                    stockMovement => !string.IsNullOrEmpty(stockMovement.ProductName) && stockMovement.ProductName.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.StockMovement>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
