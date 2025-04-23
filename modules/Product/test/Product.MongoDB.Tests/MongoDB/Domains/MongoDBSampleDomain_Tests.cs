using Product.Samples;
using Xunit;

namespace Product.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<ProductMongoDbTestModule>
{

}
