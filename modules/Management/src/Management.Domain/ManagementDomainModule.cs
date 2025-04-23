using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Management;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ManagementDomainSharedModule)
)]
public class ManagementDomainModule : AbpModule
{

}
