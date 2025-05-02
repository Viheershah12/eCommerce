using Product.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Product.Interfaces
{
    public interface IProductAppService : IApplicationService
    {
        #region Product
        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto dto);

        Task<PagedResultDto<StoreProductDto>> GetListByCategoryAsync(Dtos.Product.GetProductCategoryListDto dto);

        Task<Guid> CreateAsync(CreateUpdateProductDto dto);

        Task<CreateUpdateProductDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateProductDto dto);

        Task DeleteAsync(Guid id);
        #endregion

        #region Media
        Task DeleteProductMediaAsync(DeleteProductMedia dto);
        #endregion

        #region Tier Price
        Task CreateTierPriceAsync(CreateUpdateProductTierPriceDto dto);

        Task<TierPriceDto> GetTierPriceAsync(GetTierPriceDto dto);

        Task UpdateTierPriceAsync(CreateUpdateProductTierPriceDto dto);

        Task DeleteTierPriceAsync(DeleteTierPriceDto dto);
        #endregion

        #region Similar Products
        Task<List<StoreProductDto>> GetSimilarProductAsync(List<Guid> tagIds, int limit = 10);
        #endregion
    }
}
