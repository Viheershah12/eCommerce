using Volo.Abp.Modularity;

namespace Abp.eCommerce;

/* Inherit from this class for your domain layer tests. */
public abstract class eCommerceDomainTestBase<TStartupModule> : eCommerceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
