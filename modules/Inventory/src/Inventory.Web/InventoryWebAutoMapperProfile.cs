using AutoMapper;
using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;

namespace Inventory.Web;

public class InventoryWebAutoMapperProfile : Profile
{
    public InventoryWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Stock Balance
        CreateMap<Pages.StockBalance.CreateModel.CreateViewModel, CreateUpdateStockBalanceDto>().ReverseMap();
        CreateMap<Pages.StockBalance.EditModel.EditViewModel, CreateUpdateStockBalanceDto>().ReverseMap();

        // Stock Movement
        CreateMap<Pages.StockMovement.CreateModel.CreateViewModel, CreateUpdateStockMovementDto>().ReverseMap();
        CreateMap<Pages.StockMovement.EditModel.EditViewModel, CreateUpdateStockMovementDto>().ReverseMap();
    }
}
