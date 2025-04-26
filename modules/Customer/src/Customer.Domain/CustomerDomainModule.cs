using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Customer;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(CustomerDomainSharedModule)
)]
public class CustomerDomainModule : AbpModule
{

}
