using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace PaymentTransactions.MongoDB;

[ConnectionStringName(PaymentTransactionsDbProperties.ConnectionStringName)]
public interface IPaymentTransactionsMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */

    IMongoCollection<Models.PaymentTransaction> PaymentTransactions { get; }

    IMongoCollection<Models.MpesaTransaction> MpesaTransactions { get; }
}
