using Inventory.Dtos.Inventory;
using Inventory.Interfaces;
using Inventory.Inventory;
using Inventory.StockMovement;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Inventory.Services
{
    public class StockBalanceAppService : InventoryAppService, IStockBalanceAppService
    {
        #region Fields
        private readonly InventoryManager _inventoryManager;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IStockMovementRepository _stockMovementRepository;
        #endregion

        #region CTOR
        public StockBalanceAppService(
            InventoryManager inventoryManager,
            IInventoryRepository inventoryRepository,
            IStockMovementRepository stockMovementRepository
        ) 
        {
            _inventoryManager = inventoryManager;
            _inventoryRepository = inventoryRepository;
            _stockMovementRepository = stockMovementRepository;
        }
        #endregion 

        public async Task<PagedResultDto<StockBalanceDto>> GetListAsync(GetStockBalanceListDto dto)
        {
            try
            {
                var (list, count) = await _inventoryManager.GetInventoryListing(dto);
                var items = ObjectMapper.Map<List<Models.Inventory>, List<StockBalanceDto>>(list);

                return new PagedResultDto<StockBalanceDto>(count, items);
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateStockBalanceDto dto)
        {
            try
            {
                var inventoryDto = ObjectMapper.Map<CreateUpdateStockBalanceDto, Models.Inventory>(dto);
                var inventory = await _inventoryRepository.InsertAsync(inventoryDto);

                return inventory.Id;
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
            {
                throw new UserFriendlyException(
                    code: InventoryErrorCodes.DuplicateProductId,
                    message: L[InventoryErrorCodes.DuplicateProductId, dto.ProductName],
                    details: L[InventoryErrorCodes.DuplicateProductId, dto.ProductName],
                    logLevel: LogLevel.Error);
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task<CreateUpdateStockBalanceDto> GetAsync(Guid id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(id);
                var dto = ObjectMapper.Map<Models.Inventory, CreateUpdateStockBalanceDto>(inventory);

                return dto;
            }
            catch (Exception ex) 
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateStockBalanceDto dto)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(dto.Id);

                var updatedInventoryDto = ObjectMapper.Map<CreateUpdateStockBalanceDto, Models.Inventory>(dto);
                updatedInventoryDto.ConcurrencyStamp = inventory.ConcurrencyStamp;

                await _inventoryRepository.UpdateAsync(updatedInventoryDto);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(id);
                await _inventoryRepository.DeleteAsync(inventory);

                var stockMovementQuerable = await _stockMovementRepository.GetQueryableAsync();
                var stockMovementList = stockMovementQuerable.Where(x => x.InventoryId == inventory.Id);

                await _stockMovementRepository.DeleteManyAsync(stockMovementList.Select(x => x.Id));
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }
    }
}
