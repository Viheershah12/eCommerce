using Microsoft.AspNetCore.Mvc;
using Product.Dtos.Product;
using Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Product.Controllers
{
    [Area(ProductRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = ProductRemoteServiceConsts.RemoteServiceName)]
    [Route("api/product/product")]
    public class ProductController : ProductsController, IProductAppService
    {
        #region Fields
        private readonly IProductAppService _productAppService;
        #endregion

        #region CTOR
        public ProductController(
            IProductAppService productAppService
        )
        {
            _productAppService = productAppService;
        }
        #endregion

        #region Product
        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto dto)
        {
            return await _productAppService.GetListAsync(dto);
        }

        [HttpGet]
        [Route("getListByCategory")]
        public async Task<PagedResultDto<StoreProductDto>> GetListByCategoryAsync(GetProductCategoryListDto dto)
        {
            return await _productAppService.GetListByCategoryAsync(dto);    
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateProductDto dto)
        {
            return await _productAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateProductDto> GetAsync(Guid id)
        {
            return await _productAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateProductDto dto)
        {
            await _productAppService.UpdateAsync(dto);
        }

        [HttpPut]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _productAppService.DeleteAsync(id);
        }
        #endregion

        #region Media
        [HttpDelete]
        [Route("deleteProductMedia")]
        public async Task DeleteProductMediaAsync(DeleteProductMedia dto)
        {
            await _productAppService.DeleteProductMediaAsync(dto);  
        }
        #endregion

        #region Tier Price
        [HttpPost]
        [Route("createTierPrice")]
        public async Task CreateTierPriceAsync(CreateUpdateProductTierPriceDto dto)
        {
            await _productAppService.CreateTierPriceAsync(dto);
        }

        [HttpGet]
        public async Task<TierPriceDto> GetTierPriceAsync(GetTierPriceDto dto)
        {
            return await _productAppService.GetTierPriceAsync(dto);
        }

        [HttpPut]
        [Route("updateTierPrice")]
        public async Task UpdateTierPriceAsync(CreateUpdateProductTierPriceDto dto)
        {
            await _productAppService.UpdateTierPriceAsync(dto);
        }

        [HttpPut]
        [Route("deleteTierPrice")]
        public async Task DeleteTierPriceAsync(DeleteTierPriceDto dto)
        {
            await _productAppService.DeleteTierPriceAsync(dto);
        }
        #endregion
    }
}
