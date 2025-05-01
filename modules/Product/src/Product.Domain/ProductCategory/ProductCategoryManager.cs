using Product.Dtos.Product;
using Product.Dtos.ProductCategory;
using Product.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Product.ProductCategory
{
    public class ProductCategoryManager : DomainService
    {
        #region Fields
        private readonly IProductCategoryRepository _productCategoryRepository;
        #endregion

        #region CTOR
        public ProductCategoryManager(
            IProductCategoryRepository productCategoryRepository            
        )
        {
            _productCategoryRepository = productCategoryRepository;
        }
        #endregion

        public async Task<(List<Models.ProductCategory> items, int totalCount)> GetProductListing(Dtos.ProductCategory.GetProductCategoryListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.ProductCategory.Name);

            var result = await _productCategoryRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _productCategoryRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
