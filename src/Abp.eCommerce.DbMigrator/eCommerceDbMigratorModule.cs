using Abp.eCommerce.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Abp.eCommerce.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(eCommerceMongoDbModule),
    typeof(eCommerceApplicationContractsModule)
)]
public class eCommerceDbMigratorModule : AbpModule
{
}
