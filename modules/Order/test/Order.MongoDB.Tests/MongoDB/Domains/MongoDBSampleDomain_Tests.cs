using Order.Samples;
using Xunit;

namespace Order.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<OrderMongoDbTestModule>
{

}
