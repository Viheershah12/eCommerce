using Volo.Abp.Modularity;

namespace Abp.eCommerce;

[DependsOn(
    typeof(eCommerceApplicationModule),
    typeof(eCommerceDomainTestModule)
)]
public class eCommerceApplicationTestModule : AbpModule
{

}
