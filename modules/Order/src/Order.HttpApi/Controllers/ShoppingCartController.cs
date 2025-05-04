using Microsoft.AspNetCore.Mvc;
using Order.Dtos.ShoppingCart;
using Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Order.Controllers
{
    [Area(OrderRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/order/shoppingcart")]
    public class ShoppingCartController : OrderController, IShoppingCartAppService
    {
        #region Fields 
        private readonly IShoppingCartAppService _shoppingCartAppService;
        #endregion

        #region CTOR
        public ShoppingCartController(
            IShoppingCartAppService shoppingCartAppService                
        )
        {
            _shoppingCartAppService = shoppingCartAppService;
        }
        #endregion

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ShoppingCartDto>> GetListAsync(GetShoppingCartListDto dto)
        {
            return await _shoppingCartAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateShoppingCartDto dto)
        {
            return await _shoppingCartAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateShoppingCartDto> GetAsync(Guid id)
        {
            return await _shoppingCartAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("addShoppingCartItem")]
        public async Task AddShoppingCartItemAsync(CreateUpdateShoppingCartItemDto dto)
        {
            await _shoppingCartAppService.AddShoppingCartItemAsync(dto);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateShoppingCartDto dto)
        {
            await _shoppingCartAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _shoppingCartAppService.DeleteAsync(id);
        }
    }
}
