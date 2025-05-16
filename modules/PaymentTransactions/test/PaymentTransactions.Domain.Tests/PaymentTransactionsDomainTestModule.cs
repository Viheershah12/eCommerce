using Volo.Abp.Modularity;

namespace PaymentTransactions;

[DependsOn(
    typeof(PaymentTransactionsDomainModule),
    typeof(PaymentTransactionsTestBaseModule)
)]
public class PaymentTransactionsDomainTestModule : AbpModule
{

}
