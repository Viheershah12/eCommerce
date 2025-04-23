using Management.MongoDB;
using Management.Samples;
using Xunit;

namespace Management.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<ManagementMongoDbTestModule>
{

}
