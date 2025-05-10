using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using Inventory.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Inventory.StockMovement
{
    public class StockMovementManager : DomainService
    {
        #region Fields
        private readonly IStockMovementRepository _stockMovementRepository;
        #endregion

        #region CTOR
        public StockMovementManager(
            IStockMovementRepository stockMovementRepository
        ) 
        {
            _stockMovementRepository = stockMovementRepository;
        }
        #endregion 

        public async Task<(List<Models.StockMovement> items, int totalCount)> GetStockMovementListing(GetStockMovementListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.StockMovement.ProductName);

            var result = await _stockMovementRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _stockMovementRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.ProductName.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
