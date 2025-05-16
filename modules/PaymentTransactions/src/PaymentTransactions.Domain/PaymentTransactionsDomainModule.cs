using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace PaymentTransactions;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(PaymentTransactionsDomainSharedModule)
)]
public class PaymentTransactionsDomainModule : AbpModule
{

}
