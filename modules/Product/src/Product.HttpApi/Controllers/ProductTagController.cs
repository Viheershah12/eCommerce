using Microsoft.AspNetCore.Mvc;
using Product.Dtos.ProductTag;
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
    [Route("api/product/producttag")]
    public class ProductTagController : ProductsController, IProductTagAppService
    {
        #region Fields
        private readonly IProductTagAppService _productTagAppService;
        #endregion

        #region CTOR
        public ProductTagController
        (
            IProductTagAppService productTagAppService            
        )
        {
            _productTagAppService = productTagAppService;
        }
        #endregion 

        [HttpGet]
        [Route("getList")]
        public async Task<PagedResultDto<ProductTagDto>> GetListAsync(GetProductTagListDto dto)
        {
            return await _productTagAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateProductTagDto dto)
        {
            return await _productTagAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateProductTagDto> GetAsync(Guid id)
        {
            return await _productTagAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateProductTagDto dto)
        {
            await _productTagAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _productTagAppService.DeleteAsync(id);
        }
    }
}
