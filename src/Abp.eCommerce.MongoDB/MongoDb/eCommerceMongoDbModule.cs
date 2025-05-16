using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.BackgroundJobs.MongoDB;
using Volo.Abp.FeatureManagement.MongoDB;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.OpenIddict.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.MongoDB;
using Volo.Abp.SettingManagement.MongoDB;
using Volo.Abp.BlobStoring.Database.MongoDB;
using Volo.Abp.Uow;
using Volo.Abp.TenantManagement.MongoDB;
using Product.MongoDB;
using Management.MongoDB;
using Customer.MongoDB;
using Volo.CmsKit.MongoDB;
using Order.MongoDB;
using Inventory.MongoDB;
using PaymentTransactions.MongoDB;

namespace Abp.eCommerce.MongoDB;

[DependsOn(
    typeof(eCommerceDomainModule),
    typeof(AbpPermissionManagementMongoDbModule),
    typeof(AbpSettingManagementMongoDbModule),
    typeof(AbpBackgroundJobsMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(AbpFeatureManagementMongoDbModule),
    typeof(AbpIdentityMongoDbModule),
    typeof(AbpOpenIddictMongoDbModule),
    typeof(AbpTenantManagementMongoDbModule),
    typeof(BlobStoringDatabaseMongoDbModule)
)]
[DependsOn(typeof(ProductMongoDbModule))]
[DependsOn(typeof(ManagementMongoDbModule))]
[DependsOn(typeof(CustomerMongoDbModule))]
    [DependsOn(typeof(CmsKitMongoDbModule))]
    [DependsOn(typeof(OrderMongoDbModule))]
    [DependsOn(typeof(InventoryMongoDbModule))]
    [DependsOn(typeof(PaymentTransactionsMongoDbModule))]
    public class eCommerceMongoDbModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMongoDbContext<eCommerceMongoDbContext>(options =>
            {
                options.AddDefaultRepositories();
            });

            context.Services.AddAlwaysDisableUnitOfWorkTransaction();
            Configure<AbpUnitOfWorkDefaultOptions>(options =>
            {
                options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
            });
        }
    }
