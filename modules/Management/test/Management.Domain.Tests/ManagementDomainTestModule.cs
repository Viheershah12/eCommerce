using Volo.Abp.Modularity;

namespace Management;

[DependsOn(
    typeof(ManagementDomainModule),
    typeof(ManagementTestBaseModule)
)]
public class ManagementDomainTestModule : AbpModule
{

}
