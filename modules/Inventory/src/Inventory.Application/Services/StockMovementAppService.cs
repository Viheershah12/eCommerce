using Abp.eCommerce.Enums;
using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using Inventory.Interfaces;
using Inventory.Inventory;
using Inventory.StockMovement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Inventory.Services
{
    public class StockMovementAppService : InventoryAppService, IStockMovementAppService
    {
        #region Fields
        private readonly IStockMovementRepository _stockMovementRepository;
        private readonly StockMovementManager _stockMovementManager;
        private readonly IInventoryRepository _stockBalanceRepository;
        #endregion

        #region CTOR
        public StockMovementAppService(
            IStockMovementRepository stockMovementRepository,
            StockMovementManager stockMovementManager,
            IInventoryRepository stockBalanceRepository
        ) 
        {
            _stockMovementRepository = stockMovementRepository;
            _stockMovementManager = stockMovementManager;
            _stockBalanceRepository = stockBalanceRepository;
        }
        #endregion

        #region CRUD
        public async Task<PagedResultDto<StockMovementDto>> GetListAsync(GetStockMovementListDto dto)
        {
            try
            {
                var (list, count) = await _stockMovementManager.GetStockMovementListing(dto);
                var items = ObjectMapper.Map<List<Models.StockMovement>, List<StockMovementDto>>(list);

                return new PagedResultDto<StockMovementDto>(count, items);      
            }
            catch (Exception ex) 
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateStockMovementDto dto)
        {
            try
            {
                var stockMovementDto = ObjectMapper.Map<CreateUpdateStockMovementDto, Models.StockMovement>(dto);
                var stockMovement = await _stockMovementRepository.InsertAsync(stockMovementDto);

                var inventory = await _stockBalanceRepository.GetAsync(stockMovement.InventoryId);
                inventory.StockQuantity = stockMovement.QuantityAfterChange;

                await _stockBalanceRepository.UpdateAsync(inventory);

                return stockMovement.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task<CreateUpdateStockMovementDto> GetAsync(Guid id)
        {
            try
            {
                var stockMovement = await _stockMovementRepository.GetAsync(id);
                var dto = ObjectMapper.Map<Models.StockMovement, CreateUpdateStockMovementDto>(stockMovement);

                return dto;
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateStockMovementDto dto)
        {
            try
            {
                var stockMovement = await _stockMovementRepository.GetAsync(dto.Id);

                var updatedstockMovementDto = ObjectMapper.Map<CreateUpdateStockMovementDto, Models.StockMovement>(dto);
                updatedstockMovementDto.ConcurrencyStamp = stockMovement.ConcurrencyStamp;

                await _stockMovementRepository.UpdateAsync(updatedstockMovementDto);
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
                var stockMovement = await _stockMovementRepository.GetAsync(id);
                await _stockMovementRepository.DeleteAsync(stockMovement);

                var inventory = await _stockBalanceRepository.GetAsync(stockMovement.InventoryId);

                if (stockMovement.MovementType == StockMovementType.ManualAdjustmentPlus || 
                    stockMovement.MovementType == StockMovementType.Restock || 
                    stockMovement.MovementType == StockMovementType.ReleaseReservation)
                {
                    inventory.StockQuantity -= stockMovement.QuantityChanged;
                }
                else
                {
                    inventory.StockQuantity += stockMovement.QuantityChanged;
                }

                await _stockBalanceRepository.UpdateAsync(inventory);
            }
            catch (Exception ex)
            {
                throw new BusinessException(message: ex.Message);
            }
        }
        #endregion

        #region Order Transactions
        public async Task CreateSaleStockMovement(OrderTransactionMovementDto dto)
        {
            try
            {
                var stockMovementList = new List<Models.StockMovement>();
                var stockBalanceList = new List<Models.Inventory>();

                foreach (var item in dto.OrderItems)
                {
                    var inventory = await _stockBalanceRepository.GetAsync(x => x.ProductId == item.ProductId);

                    var stockMovement = new Models.StockMovement
                    {
                        InventoryId = inventory.Id,
                        ProductName = item.ProductName,
                        MovementType = StockMovementType.Sale,
                        QuantityChanged = item.Quantity.To<int>(),
                        OrderId = dto.OrderId,
                        QuantityBeforeChange = inventory.StockQuantity ?? 0,
                        QuantityAfterChange = inventory.StockQuantity - item.Quantity ?? 0
                    };
                    stockMovementList.Add(stockMovement);

                    inventory.StockQuantity -= item.Quantity;
                    stockBalanceList.Add(inventory);
                }

                await _stockMovementRepository.InsertManyAsync(stockMovementList);
                await _stockBalanceRepository.UpdateManyAsync(stockBalanceList);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        #endregion 
    }
}
