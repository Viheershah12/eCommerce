using Inventory.Dtos.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Inventory.Inventory
{
    public class InventoryManager : DomainService
    {
        #region Fields
        private readonly IInventoryRepository _inventoryRepository;
        #endregion

        #region CTOR
        public InventoryManager(
            IInventoryRepository inventoryRepository                
        )
        {
            _inventoryRepository = inventoryRepository;
        }
        #endregion

        public async Task<(List<Models.Inventory> items, int totalCount)> GetInventoryListing(GetStockBalanceListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.Inventory.ProductName);

            var result = await _inventoryRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _inventoryRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.ProductName.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
