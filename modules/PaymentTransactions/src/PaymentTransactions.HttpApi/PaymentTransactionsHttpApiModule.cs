using Localization.Resources.AbpUi;
using PaymentTransactions.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace PaymentTransactions;

[DependsOn(
    typeof(PaymentTransactionsApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class PaymentTransactionsHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(PaymentTransactionsHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<PaymentTransactionsResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
