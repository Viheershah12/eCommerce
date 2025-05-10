using Volo.Abp.Modularity;

namespace Inventory;

[DependsOn(
    typeof(InventoryDomainModule),
    typeof(InventoryTestBaseModule)
)]
public class InventoryDomainTestModule : AbpModule
{

}
