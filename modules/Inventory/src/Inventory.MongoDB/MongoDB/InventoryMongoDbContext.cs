using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Inventory.MongoDB;

[ConnectionStringName(InventoryDbProperties.ConnectionStringName)]
public class InventoryMongoDbContext : AbpMongoDbContext, IInventoryMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Models.Inventory> Inventory => Collection<Models.Inventory>();

    public IMongoCollection<Models.StockMovement> StockMovement => Collection<Models.StockMovement>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureInventory();
    }
}
