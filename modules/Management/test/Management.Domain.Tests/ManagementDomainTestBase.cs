using Volo.Abp.Modularity;

namespace Management;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class ManagementDomainTestBase<TStartupModule> : ManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
