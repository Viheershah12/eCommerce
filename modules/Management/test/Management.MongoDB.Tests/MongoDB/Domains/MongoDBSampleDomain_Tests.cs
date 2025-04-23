using Management.Samples;
using Xunit;

namespace Management.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<ManagementMongoDbTestModule>
{

}
