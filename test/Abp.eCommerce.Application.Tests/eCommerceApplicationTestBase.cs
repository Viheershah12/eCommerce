using Volo.Abp.Modularity;

namespace Abp.eCommerce;

public abstract class eCommerceApplicationTestBase<TStartupModule> : eCommerceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
