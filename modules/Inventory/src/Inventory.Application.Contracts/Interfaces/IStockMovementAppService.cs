using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inventory.Interfaces
{
    public interface IStockMovementAppService : IApplicationService
    {
        Task<PagedResultDto<StockMovementDto>> GetListAsync(GetStockMovementListDto dto);

        Task<Guid> CreateAsync(CreateUpdateStockMovementDto dto);

        Task<CreateUpdateStockMovementDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateStockMovementDto dto);

        Task DeleteAsync(Guid id);

        #region Order Transaction 
        Task CreateSaleStockMovement(OrderTransactionMovementDto dto);
        #endregion
    }
}
