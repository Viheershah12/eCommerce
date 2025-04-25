using System.Linq.Dynamic.Core;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Product.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Product.ProductCategory
{
    public class ProductCategoryRepository : MongoDbRepository<ProductMongoDbContext, Models.ProductCategory, Guid>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IMongoDbContextProvider<ProductMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Models.ProductCategory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.ProductCategory, IMongoQueryable<Models.ProductCategory>>(
                    !string.IsNullOrEmpty(filter),
                    product => !string.IsNullOrEmpty(product.Name) && product.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.ProductCategory>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
