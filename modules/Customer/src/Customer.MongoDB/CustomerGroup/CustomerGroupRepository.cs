using Customer.MongoDB;
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

namespace Customer.CustomerGroup
{
    public class CustomerGroupRepository : MongoDbRepository<CustomerMongoDbContext, Models.CustomerGroup, Guid>, ICustomerGroupRepository  
    {
        public CustomerGroupRepository(IMongoDbContextProvider<CustomerMongoDbContext> contextProvider) : base(contextProvider) 
        {
        }

        public async Task<List<Models.CustomerGroup>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .WhereIf<Models.CustomerGroup, IMongoQueryable<Models.CustomerGroup>>(
                    !string.IsNullOrEmpty(filter),
                    customerGroup => !string.IsNullOrEmpty(customerGroup.Name) && customerGroup.Name.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Models.CustomerGroup>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
