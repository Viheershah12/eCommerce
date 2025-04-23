using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Management;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ManagementHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class ManagementConsoleApiClientModule : AbpModule
{

}
