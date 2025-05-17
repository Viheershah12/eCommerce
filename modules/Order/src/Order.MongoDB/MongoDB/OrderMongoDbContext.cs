using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Order.MongoDB;

[ConnectionStringName(OrderDbProperties.ConnectionStringName)]
public class OrderMongoDbContext : AbpMongoDbContext, IOrderMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Models.ShoppingCart> ShoppingCart => Collection<Models.ShoppingCart>();

    public IMongoCollection<Models.WishList> WishList => Collection<Models.WishList>();

    public IMongoCollection<Models.Order> Order => Collection<Models.Order>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureOrder();
    }
}
