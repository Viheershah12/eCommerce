using Inventory.Samples;
using Xunit;

namespace Inventory.MongoDB.Domains;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleDomain_Tests : SampleManager_Tests<InventoryMongoDbTestModule>
{

}
