using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Product.MongoDB;

[ConnectionStringName(ProductDbProperties.ConnectionStringName)]
public class ProductMongoDbContext : AbpMongoDbContext, IProductMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Models.Product> Product => Collection<Models.Product>();

    public IMongoCollection<Models.ProductCategory> ProductCategory => Collection<Models.ProductCategory>();

    public IMongoCollection<Models.ProductTag> ProductTag => Collection<Models.ProductTag>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureProduct();
    }
}
