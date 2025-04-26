using Product.Dtos.Product;
using Product.Dtos.ProductTag;
using Product.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Product.ProductTag
{
    public class ProductTagManager : DomainService
    {
        #region Fields
        private readonly IProductTagRepository _productTagRepository;
        #endregion

        #region CTOR
        public ProductTagManager(IProductTagRepository productTagRepository)
        {
            _productTagRepository = productTagRepository;
        }
        #endregion

        public async Task<(List<Models.ProductTag> items, int totalCount)> GetProductListing(GetProductTagListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.ProductTag.Name);

            var result = await _productTagRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _productTagRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.Name.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
