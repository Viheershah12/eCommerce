using Microsoft.AspNetCore.Mvc;
using Product.Dtos.ProductCategory;
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
    [Route("api/product/productcategory")]
    public class ProductCategoryController : ProductsController, IProductCategoryAppService
    {
        #region Fields
        private readonly IProductCategoryAppService _productCategoryAppService;     
        #endregion

        #region CTOR
        public ProductCategoryController(
            IProductCategoryAppService productCategoryAppService
        )
        {
            _productCategoryAppService = productCategoryAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ProductCategoryDto>> GetListAsync(GetProductCategoryListDto dto)
        {
            return await _productCategoryAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateProductCategoryDto dto)
        {
            return await _productCategoryAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateProductCategoryDto> GetAsync(Guid id)
        {
            return await _productCategoryAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateProductCategoryDto dto)
        {
            await _productCategoryAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _productCategoryAppService.DeleteAsync(id);
        }
    }
}
