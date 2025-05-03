using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Order;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class OrderInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<OrderInstallerModule>();
        });
    }
}
