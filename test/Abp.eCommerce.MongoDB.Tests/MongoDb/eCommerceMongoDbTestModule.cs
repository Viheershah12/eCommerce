using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Abp.eCommerce.MongoDB;

[DependsOn(
    typeof(eCommerceApplicationTestModule),
    typeof(eCommerceMongoDbModule)
)]
public class eCommerceMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = eCommerceMongoDbFixture.GetRandomConnectionString();
        });
    }
}
