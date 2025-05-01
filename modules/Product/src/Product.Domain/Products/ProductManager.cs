using Product.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Product.Products
{
    public class ProductManager : DomainService
    {
        #region Fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region CTOR
        public ProductManager (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        public async Task<(List<Models.Product> items, int totalCount)> GetProductListing(GetProductListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.Product.Name);

            var result = await _productRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _productRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)
            );

            return (result, totalCount);
        }

        public async Task<(List<Models.Product> items, int totalCount)> GetProductByCategoryListing(GetProductCategoryListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.Product.Name);

            var result = await _productRepository.GetListByCategoryAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter, dto.Category);

            var totalCount = await _productRepository.CountAsync(x =>
                (string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)) && x.Category == dto.Category
            );

            return (result, totalCount);
        }
    }
}
