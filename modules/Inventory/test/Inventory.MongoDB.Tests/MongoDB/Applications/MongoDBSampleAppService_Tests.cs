using Inventory.MongoDB;
using Inventory.Samples;
using Xunit;

namespace Inventory.MongoDb.Applications;

[Collection(MongoTestCollection.Name)]
public class MongoDBSampleAppService_Tests : SampleAppService_Tests<InventoryMongoDbTestModule>
{

}
