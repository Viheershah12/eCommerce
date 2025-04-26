using Customer.MongoDB;
using Customer.Samples;
using Xunit;

namespace Customer.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<CustomerMongoDbTestModule>
{

}
