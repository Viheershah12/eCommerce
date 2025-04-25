using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Product.MongoDB;

[ConnectionStringName(ProductDbProperties.ConnectionStringName)]
public interface IProductMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */

    IMongoCollection<Models.Product> Product { get; }

    IMongoCollection<Models.ProductCategory> ProductCategory { get; }
}
