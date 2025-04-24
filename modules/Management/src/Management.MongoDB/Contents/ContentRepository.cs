using Management.Models;
using Management.MongoDB;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using System.Linq.Dynamic.Core;
using MongoDB.Driver;

namespace Management.Contents
{
    public class ContentRepository : MongoDbRepository<ManagementMongoDbContext, Content, Guid>, IContentRepository
    {
        public ContentRepository(IMongoDbContextProvider<ManagementMongoDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Content>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null)
        {
            var queryable = await GetMongoQueryableAsync();

            return await queryable
                .WhereIf<Content, IMongoQueryable<Content>>(
                    !string.IsNullOrEmpty(filter),
                    content => !string.IsNullOrEmpty(content.Title) && content.Title.Contains(filter, StringComparison.CurrentCultureIgnoreCase)
                )
                .OrderBy(sorting)
                .As<IMongoQueryable<Content>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
    }
}
