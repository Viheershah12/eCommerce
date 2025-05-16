using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
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
    typeof(eCommerceDomainSharedModule),
    typeof(AbpFeatureManagementApplicationContractsModule),
    typeof(AbpSettingManagementApplicationContractsModule),
    typeof(AbpIdentityApplicationContractsModule),
    typeof(AbpAccountApplicationContractsModule),
    typeof(AbpTenantManagementApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationContractsModule)
)]
[DependsOn(typeof(ProductApplicationContractsModule))]
[DependsOn(typeof(ManagementApplicationContractsModule))]
    [DependsOn(typeof(CustomerApplicationContractsModule))]
    [DependsOn(typeof(CmsKitApplicationContractsModule))]
    [DependsOn(typeof(OrderApplicationContractsModule))]
    [DependsOn(typeof(InventoryApplicationContractsModule))]
    [DependsOn(typeof(PaymentTransactionsApplicationContractsModule))]
    public class eCommerceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        eCommerceDtoExtensions.Configure();
    }
}
