using Product.Dtos.Common;
using Product.Interfaces;
using Product.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Product.Services
{
    public class CommonAppService : ProductsAppService, ICommonAppService
    {
        #region Fields
        private readonly IProductCategoryRepository _productCategoryRepository;
        #endregion

        #region CTOR
        public CommonAppService(
            IProductCategoryRepository productCategoryRepository
        ) 
        {
            _productCategoryRepository = productCategoryRepository;
        }
        #endregion 

        public async Task<List<ProductCategoryDropdownDto>> GetProductCategoryDropdownAsync()
        {
            try
            {
                var items = await _productCategoryRepository.GetListAsync(x => x.IsActive);

                var res = items.Select(x => new ProductCategoryDropdownDto
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
