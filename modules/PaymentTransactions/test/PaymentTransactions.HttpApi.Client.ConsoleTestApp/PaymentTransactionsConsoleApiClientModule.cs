using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace PaymentTransactions;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PaymentTransactionsHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class PaymentTransactionsConsoleApiClientModule : AbpModule
{

}
