using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Product;

[DependsOn(
    typeof(ProductApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class ProductHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(ProductApplicationContractsModule).Assembly,
            ProductRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ProductHttpApiClientModule>();
        });

    }
}
