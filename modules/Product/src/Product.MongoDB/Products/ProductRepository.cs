using MongoDB.Driver.Linq;
using Product.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace Product.Products
{
    public class ProductRepository : MongoDbRepository<ProductMongoDbContext, Models.Product, Guid>, IProductRepository
    {
        public ProductRepository(IMongoDbContextProvider<ProductMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Models.Product>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.Product, IMongoQueryable<Models.Product>>(
                    !string.IsNullOrEmpty(filter),
                    product => !string.IsNullOrEmpty(product.Name) && product.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.Product>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
