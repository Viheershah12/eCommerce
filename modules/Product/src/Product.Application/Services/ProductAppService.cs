using Product.Interfaces;
using Product.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Services
{
    public class ProductAppService : ProductsAppService, IProductAppService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region CTOR
        public ProductAppService(
            IProductRepository productRepository    
        )
        {
            _productRepository = productRepository;
        }
        #endregion 


    }
}
