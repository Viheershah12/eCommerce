using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace PaymentTransactions.MongoDB;

[DependsOn(
    typeof(PaymentTransactionsApplicationTestModule),
    typeof(PaymentTransactionsMongoDbModule)
)]
public class PaymentTransactionsMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
