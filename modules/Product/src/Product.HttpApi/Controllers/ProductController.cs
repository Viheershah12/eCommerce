using Microsoft.AspNetCore.Mvc;
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


    }
}
