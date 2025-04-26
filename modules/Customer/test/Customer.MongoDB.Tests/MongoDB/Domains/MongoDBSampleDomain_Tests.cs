using Customer.Samples;
using Xunit;

namespace Customer.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<CustomerMongoDbTestModule>
{

}
