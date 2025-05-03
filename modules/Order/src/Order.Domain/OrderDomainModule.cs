using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Order;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(OrderDomainSharedModule)
)]
public class OrderDomainModule : AbpModule
{

}
