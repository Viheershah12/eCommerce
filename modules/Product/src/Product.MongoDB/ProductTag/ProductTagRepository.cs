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

namespace Product.ProductTag
{
    public class ProductTagRepository : MongoDbRepository<ProductMongoDbContext, Models.ProductTag, Guid>, IProductTagRepository
    {
        public ProductTagRepository(IMongoDbContextProvider<ProductMongoDbContext> contextProvider) : base(contextProvider) 
        { 
        }

        public async Task<List<Models.ProductTag>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.ProductTag, IMongoQueryable<Models.ProductTag>>(
                    !string.IsNullOrEmpty(filter),
                    productTag => !string.IsNullOrEmpty(productTag.Name) && productTag.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.ProductTag>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
