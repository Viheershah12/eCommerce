using Microsoft.AspNetCore.Mvc;
using Order.Dtos.WishList;
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
    [Route("api/order/wishlist")]
    public class WishListController : OrderController, IWishListAppService
    {
        #region Fields
        private readonly IWishListAppService _wishListAppService;
        #endregion

        #region CTOR
        public WishListController(
            IWishListAppService wishListAppService            
        )
        {
            _wishListAppService = wishListAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<WishListDto>> GetListAsync(GetWishListListingDto dto)
        {
            return await _wishListAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateWishListDto dto)
        {
            return await _wishListAppService.CreateAsync(dto);        
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateWishListDto> GetAsync(Guid id)
        {
            return await _wishListAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("addWishlistItem")]
        public async Task AddWishlistItemAsync(CreateUpdateWishlistItemDto dto)
        {
            await _wishListAppService.AddWishlistItemAsync(dto);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateWishListDto dto)
        {
            await _wishListAppService.UpdateAsync(dto);            
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _wishListAppService.DeleteAsync(id);  
        }
    }
}
