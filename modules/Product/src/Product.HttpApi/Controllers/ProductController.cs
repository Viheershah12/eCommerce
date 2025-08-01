﻿using Abp.eCommerce.Models;
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
        public async Task<BasePagedModel<StoreProductDto>> GetListByCategoryAsync(GetProductCategoryListDto dto)
        {
            return await _productAppService.GetListByCategoryAsync(dto);    
        }

        [HttpGet]
        [Route("getProductByMultipleId")]
        public async Task<List<StoreProductDto>> GetProductByMultipleIdAsync(List<Guid> ids)
        {
            return await _productAppService.GetProductByMultipleIdAsync(ids);
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

        [HttpDelete]
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

        [HttpDelete]
        [Route("deleteTierPrice")]
        public async Task DeleteTierPriceAsync(DeleteTierPriceDto dto)
        {
            await _productAppService.DeleteTierPriceAsync(dto);
        }
        #endregion

        #region Similar Products
        [HttpGet]
        [Route("getSimilarProduct")]
        public async Task<List<StoreProductDto>> GetSimilarProductAsync(List<Guid> tagIds, int limit = 10)
        {
            return await _productAppService.GetSimilarProductAsync(tagIds, limit);
        }
        #endregion

        #region Product Suggestion
        [HttpGet]
        [Route("getProductSuggestions")]
        public async Task<List<StoreProductDto>> GetProductSuggestionsAsync(Guid currentProductId)
        {
            return await _productAppService.GetProductSuggestionsAsync(currentProductId);
        }
        #endregion

        #region Product Search 
        [HttpGet]
        [Route("productSearch")]
        public async Task<List<ProductResultDto>> SearchProductAsync(string productName)
        {
            return await _productAppService.SearchProductAsync(productName);
        }
        #endregion
    }
}
