using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Product;
using Management;
using Customer;
using Volo.CmsKit;
using Order;
using Inventory;
using PaymentTransactions;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System;
using Volo.Abp.BackgroundJobs;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.eCommerce;

[DependsOn(
    typeof(eCommerceDomainModule),
    typeof(eCommerceApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    //typeof(AbpHangfireModule),
    //typeof(AbpBackgroundWorkersHangfireModule),
    //typeof(AbpBackgroundJobsHangfireModule)
    )]
[DependsOn(typeof(ProductApplicationModule))]
[DependsOn(typeof(ManagementApplicationModule))]
[DependsOn(typeof(CustomerApplicationModule))]
[DependsOn(typeof(CmsKitApplicationModule))]
[DependsOn(typeof(OrderApplicationModule))]
[DependsOn(typeof(InventoryApplicationModule))]
[DependsOn(typeof(PaymentTransactionsApplicationModule))]
public class eCommerceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<eCommerceApplicationModule>();
        });

        //ConfigureHangfire(context, configuration);
    }

    //private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
    //{
    //    Configure<AbpBackgroundJobOptions>(options => {
    //        options.IsJobExecutionEnabled = false;
    //    });

    //    Configure<AbpHangfireOptions>(options =>
    //    {
    //        options.ServerOptions = new BackgroundJobServerOptions()
    //        {
    //            Queues = ["default", "mpesatransaction"]
    //        };
    //    });

    //    var migrationOptions = new MongoMigrationOptions
    //    {
    //        MigrationStrategy = new MigrateMongoMigrationStrategy(),
    //        BackupStrategy = new CollectionMongoBackupStrategy()
    //    };

    //    var storageOptions = new MongoStorageOptions
    //    {
    //        MigrationOptions = migrationOptions,
    //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(30)
    //    };

    //    var connString = configuration.GetConnectionString("Default");

    //    context.Services.AddHangfire(x => x.UseMongoStorage(
    //        connString,
    //        storageOptions
    //    ));

    //    //context.Services.AddHangfireServer();
    //    //var containerBuilder = context.Services.GetContainerBuilder();
    //    //containerBuilder.RegisterType<AbpDashboardOptionsProvider>();
    //}
}
