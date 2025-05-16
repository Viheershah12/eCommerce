using PaymentTransactions.MongoDB;
using PaymentTransactions.Samples;
using Xunit;

namespace PaymentTransactions.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<PaymentTransactionsMongoDbTestModule>
{

}
