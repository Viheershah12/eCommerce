using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Management.MongoDB;

public static class ManagementMongoDbContextExtensions
{
    public static void ConfigureManagement(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
