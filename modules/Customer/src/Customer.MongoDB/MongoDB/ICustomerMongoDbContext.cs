using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Customer.MongoDB;

[ConnectionStringName(CustomerDbProperties.ConnectionStringName)]
public interface ICustomerMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */

    IMongoCollection<Models.CustomerGroup> CustomerGroup {  get; }

    IMongoCollection<Models.Customer> AbpUsers { get; }
}
