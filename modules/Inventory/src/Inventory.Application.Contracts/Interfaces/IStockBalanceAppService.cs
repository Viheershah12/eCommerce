using Inventory.Dtos.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Inventory.Interfaces
{
    public interface IStockBalanceAppService : IApplicationService
    {
        Task<PagedResultDto<StockBalanceDto>> GetListAsync(GetStockBalanceListDto dto);

        Task<Guid> CreateAsync(CreateUpdateStockBalanceDto dto);

        Task<CreateUpdateStockBalanceDto> GetAsync(Guid id);

        Task<CreateUpdateStockBalanceDto?> GetByProductIdAsync(Guid productId);

        Task UpdateAsync(CreateUpdateStockBalanceDto dto);

        Task DeleteAsync(Guid id);
    }
}
