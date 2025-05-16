using Volo.Abp.Modularity;

namespace PaymentTransactions;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class PaymentTransactionsDomainTestBase<TStartupModule> : PaymentTransactionsTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
