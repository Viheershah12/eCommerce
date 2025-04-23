using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Management;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ManagementInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ManagementInstallerModule>();
        });
    }
}
