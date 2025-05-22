using Abp.eCommerce.Helpers;
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

        // Stock Balance
        CreateMap<Models.Inventory, StockBalanceDto>();

        CreateMap<CreateUpdateStockBalanceDto, Models.Inventory>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.ExtraProperties)
            .ReverseMap();

        // Stock Movement
        CreateMap<CreateUpdateStockMovementDto, Models.StockMovement>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.ExtraProperties)
            .ReverseMap();

        CreateMap<Models.StockMovement, StockMovementDto>()
            .ForMember(dest => dest.MovementType, opt => opt.MapFrom(x => x.MovementType.GetDescription()));
    }
}
