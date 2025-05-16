using Volo.Abp;
using Volo.Abp.MongoDB;

namespace PaymentTransactions.MongoDB;

public static class PaymentTransactionsMongoDbContextExtensions
{
    public static void ConfigurePaymentTransactions(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
