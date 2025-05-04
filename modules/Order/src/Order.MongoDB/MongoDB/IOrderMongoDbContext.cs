using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Order.MongoDB;

[ConnectionStringName(OrderDbProperties.ConnectionStringName)]
public interface IOrderMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */

    IMongoCollection<Models.ShoppingCart> ShoppingCart { get; }    

    IMongoCollection<Models.WishList> WishList { get; }    
}
