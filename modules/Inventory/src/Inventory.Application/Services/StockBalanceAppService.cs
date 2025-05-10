using Inventory.Dtos.Inventory;
using Inventory.Interfaces;
using Inventory.Inventory;
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
        #endregion

        #region CTOR
        public StockBalanceAppService(
            InventoryManager inventoryManager,
            IInventoryRepository inventoryRepository
        ) 
        {
            _inventoryManager = inventoryManager;
            _inventoryRepository = inventoryRepository;
        }
        #endregion 

        public async Task<PagedResultDto<InventoryDto>> GetListAsync(GetInventoryListDto dto)
        {
            try
            {
                var (list, count) = await _inventoryManager.GetInventoryListing(dto);
                var items = ObjectMapper.Map<List<Models.Inventory>, List<InventoryDto>>(list);

                return new PagedResultDto<InventoryDto>(count, items);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateInventoryDto dto)
        {
            try
            {
                var inventoryDto = ObjectMapper.Map<CreateUpdateInventoryDto, Models.Inventory>(dto);
                var inventory = await _inventoryRepository.InsertAsync(inventoryDto);

                return inventory.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateInventoryDto> GetAsync(Guid id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(id);
                var dto = ObjectMapper.Map<Models.Inventory, CreateUpdateInventoryDto>(inventory);

                return dto;
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateInventoryDto dto)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(dto.Id);

                var updatedInventoryDto = ObjectMapper.Map<CreateUpdateInventoryDto, Models.Inventory>(dto);
                updatedInventoryDto.ConcurrencyStamp = inventory.ConcurrencyStamp;

                await _inventoryRepository.UpdateAsync(updatedInventoryDto);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var inventory = await _inventoryRepository.GetAsync(id);
                await _inventoryRepository.DeleteAsync(inventory);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
