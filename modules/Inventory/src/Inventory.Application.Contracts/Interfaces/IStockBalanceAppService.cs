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
        Task<PagedResultDto<InventoryDto>> GetListAsync(GetInventoryListDto dto);

        Task<Guid> CreateAsync(CreateUpdateInventoryDto dto);

        Task<CreateUpdateInventoryDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateInventoryDto dto);

        Task DeleteAsync(Guid id);
    }
}
