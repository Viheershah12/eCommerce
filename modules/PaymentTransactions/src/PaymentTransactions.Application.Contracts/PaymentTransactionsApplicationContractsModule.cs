using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace PaymentTransactions;

[DependsOn(
    typeof(PaymentTransactionsDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class PaymentTransactionsApplicationContractsModule : AbpModule
{

}
