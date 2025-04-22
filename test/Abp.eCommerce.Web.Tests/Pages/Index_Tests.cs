using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Abp.eCommerce.Pages;

[Collection(eCommerceTestConsts.CollectionDefinitionName)]
public class Index_Tests : eCommerceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
