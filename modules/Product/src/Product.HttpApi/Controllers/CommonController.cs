using Microsoft.AspNetCore.Mvc;
using Product.Dtos.Common;
using Product.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Product.Controllers
{
    [Area(ProductRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = ProductRemoteServiceConsts.RemoteServiceName)]
    [Route("api/product/common")]
    public class CommonController : ProductsController, ICommonAppService
    {
        #region Fields
        private readonly ICommonAppService _commonAppService;   
        #endregion

        #region CTOR
        public CommonController(
            ICommonAppService commonAppService      
        ) 
        {
            _commonAppService = commonAppService;
        }
        #endregion

        [HttpGet]
        [Route("getProductCategoryDropdown")]
        public async Task<List<ProductCategoryDropdownDto>> GetProductCategoryDropdownAsync()
        {
            return await _commonAppService.GetProductCategoryDropdownAsync();
        }
    }
}
