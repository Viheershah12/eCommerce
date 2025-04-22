using Abp.eCommerce.Samples;
using Xunit;

namespace Abp.eCommerce.MongoDB.Domains;

[Collection(eCommerceTestConsts.CollectionDefinitionName)]
public class MongoDBSampleDomainTests : SampleDomainTests<eCommerceMongoDbTestModule>
{

}
