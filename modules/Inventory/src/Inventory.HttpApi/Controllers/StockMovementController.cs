using Inventory.Dtos.Inventory;
using Inventory.Dtos.StockMovement;
using Inventory.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Inventory.Controllers
{
    [Area(InventoryRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = InventoryRemoteServiceConsts.RemoteServiceName)]
    [Route("api/inventory/stockmovement")]
    public class StockMovementController : InventoryController, IStockMovementAppService
    {
        #region Fields
        private readonly IStockMovementAppService _stockMovementAppService;
        #endregion

        #region CTOR
        public StockMovementController(
            IStockMovementAppService stockMovementAppService
        ) 
        {
            _stockMovementAppService = stockMovementAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<StockMovementDto>> GetListAsync(GetStockMovementListDto dto)
        {
            return await _stockMovementAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateStockMovementDto dto)
        {
            return await _stockMovementAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateStockMovementDto> GetAsync(Guid id)
        {
            return await _stockMovementAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateStockMovementDto dto)
        {
            await _stockMovementAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _stockMovementAppService.DeleteAsync(id);
        }
    }
}
