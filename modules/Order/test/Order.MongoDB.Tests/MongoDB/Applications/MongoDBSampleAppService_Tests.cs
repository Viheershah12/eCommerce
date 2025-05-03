using Order.MongoDB;
using Order.Samples;
using Xunit;

namespace Order.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<OrderMongoDbTestModule>
{

}
