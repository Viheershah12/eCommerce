using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Customer;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class CustomerInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CustomerInstallerModule>();
        });
    }
}
