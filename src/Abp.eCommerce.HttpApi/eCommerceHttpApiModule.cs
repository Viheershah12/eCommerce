using Localization.Resources.AbpUi;
using Abp.eCommerce.Localization;
using Volo.Abp.Account;
using Volo.Abp.SettingManagement;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.Localization;
using Volo.Abp.TenantManagement;
using Product;
using Management;
using Customer;
using Volo.CmsKit;
using Order;
using Inventory;

namespace Abp.eCommerce;

 [DependsOn(
    typeof(eCommerceApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpAccountHttpApiModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpTenantManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule)
    )]
[DependsOn(typeof(ProductHttpApiModule))]
[DependsOn(typeof(ManagementHttpApiModule))]
    [DependsOn(typeof(CustomerHttpApiModule))]
    [DependsOn(typeof(CmsKitHttpApiModule))]
    [DependsOn(typeof(OrderHttpApiModule))]
    [DependsOn(typeof(InventoryHttpApiModule))]
    public class eCommerceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<eCommerceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
