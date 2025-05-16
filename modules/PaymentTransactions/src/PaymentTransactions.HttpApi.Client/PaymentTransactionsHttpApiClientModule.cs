using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace PaymentTransactions;

[DependsOn(
    typeof(PaymentTransactionsApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class PaymentTransactionsHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(PaymentTransactionsApplicationContractsModule).Assembly,
            PaymentTransactionsRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<PaymentTransactionsHttpApiClientModule>();
        });

    }
}
