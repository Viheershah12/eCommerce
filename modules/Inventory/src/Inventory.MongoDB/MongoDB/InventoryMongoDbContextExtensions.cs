using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Inventory.MongoDB;

public static class InventoryMongoDbContextExtensions
{
    public static void ConfigureInventory(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
