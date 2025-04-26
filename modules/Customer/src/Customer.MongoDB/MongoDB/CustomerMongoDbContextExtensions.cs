using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Customer.MongoDB;

public static class CustomerMongoDbContextExtensions
{
    public static void ConfigureCustomer(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
