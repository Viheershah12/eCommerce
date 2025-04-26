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

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto dto)
        {
            return await _productAppService.GetListAsync(dto);
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
    }
}
