using PaymentTransactions.Samples;
using Xunit;

namespace PaymentTransactions.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<PaymentTransactionsMongoDbTestModule>
{

}
