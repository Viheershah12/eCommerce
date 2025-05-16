using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace PaymentTransactions.MongoDB;

[DependsOn(
    typeof(PaymentTransactionsDomainModule),
    typeof(AbpMongoDbModule)
    )]
public class PaymentTransactionsMongoDbModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<PaymentTransactionsMongoDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, MongoQuestionRepository>();
                 */
        });
    }
}
