using Product.MongoDB;
using Product.Samples;
using Xunit;

namespace Product.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<ProductMongoDbTestModule>
{

}
