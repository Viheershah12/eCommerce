using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Order.MongoDB;

public static class OrderMongoDbContextExtensions
{
    public static void ConfigureOrder(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
