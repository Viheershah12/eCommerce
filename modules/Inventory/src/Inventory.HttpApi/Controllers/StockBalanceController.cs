using Inventory.Dtos.Inventory;
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
    [Route("api/inventory/stockbalance")]
    public class StockBalanceController : InventoryController, IStockBalanceAppService
    {
        #region Fields
        private readonly IStockBalanceAppService _stockBalanceAppService;
        #endregion

        #region CTOR
        public StockBalanceController(IStockBalanceAppService stockBalanceAppService)
        {
            _stockBalanceAppService = stockBalanceAppService;
        }
        #endregion

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<InventoryDto>> GetListAsync(GetInventoryListDto dto)
        {
            return await _stockBalanceAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateInventoryDto dto)
        {
            return await _stockBalanceAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateInventoryDto> GetAsync(Guid id)
        {
            return await _stockBalanceAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateInventoryDto dto)
        {
            await _stockBalanceAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _stockBalanceAppService.DeleteAsync(id);
        }
    }
}
