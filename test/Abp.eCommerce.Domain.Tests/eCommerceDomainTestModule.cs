using Volo.Abp.Modularity;

namespace Abp.eCommerce;

[DependsOn(
    typeof(eCommerceDomainModule),
    typeof(eCommerceTestBaseModule)
)]
public class eCommerceDomainTestModule : AbpModule
{

}
