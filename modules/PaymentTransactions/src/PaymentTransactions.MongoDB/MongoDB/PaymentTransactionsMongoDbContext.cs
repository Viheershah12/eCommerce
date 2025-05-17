using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PaymentTransactions.MongoDB;

[ConnectionStringName(PaymentTransactionsDbProperties.ConnectionStringName)]
public class PaymentTransactionsMongoDbContext : AbpMongoDbContext, IPaymentTransactionsMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Models.PaymentTransaction> PaymentTransactions => Collection<Models.PaymentTransaction>();

    public IMongoCollection<Models.MpesaTransaction> MpesaTransactions => Collection<Models.MpesaTransaction>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigurePaymentTransactions();
    }
}
