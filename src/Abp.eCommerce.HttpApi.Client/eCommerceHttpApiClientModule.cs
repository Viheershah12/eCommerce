using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.TenantManagement;
using Product;
using Management;
using Customer;
using Volo.CmsKit;
using Order;
using Inventory;
using PaymentTransactions;

namespace Abp.eCommerce;

[DependsOn(
    typeof(eCommerceApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiClientModule),
    typeof(AbpFeatureManagementHttpApiClientModule),
    typeof(AbpAccountHttpApiClientModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpTenantManagementHttpApiClientModule),
    typeof(AbpSettingManagementHttpApiClientModule)
)]
    [DependsOn(typeof(CustomerHttpApiClientModule))]
    [DependsOn(typeof(CmsKitHttpApiClientModule))]
    [DependsOn(typeof(OrderHttpApiClientModule))]
    [DependsOn(typeof(InventoryHttpApiClientModule))]
    [DependsOn(typeof(PaymentTransactionsHttpApiClientModule))]
    public class eCommerceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(eCommerceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<eCommerceHttpApiClientModule>();
        });
    }
}
