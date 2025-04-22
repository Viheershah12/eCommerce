using Abp.eCommerce.MongoDB;
using Abp.eCommerce.Samples;
using Xunit;

namespace Abp.eCommerce.MongoDb.Applications;

[Collection(eCommerceTestConsts.CollectionDefinitionName)]
public class MongoDBSampleAppServiceTests : SampleAppServiceTests<eCommerceMongoDbTestModule>
{

}
