using AutoMapper;
using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using Volo.Abp.AutoMapper;

namespace Inventory;

public class InventoryApplicationAutoMapperProfile : Profile
{
    public InventoryApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CreateUpdateInventoryDto, Models.Inventory>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.ExtraProperties)
            .ReverseMap();

        CreateMap<CreateUpdateStockMovementDto, Models.StockMovement>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.ExtraProperties)
            .ReverseMap();
    }
}
