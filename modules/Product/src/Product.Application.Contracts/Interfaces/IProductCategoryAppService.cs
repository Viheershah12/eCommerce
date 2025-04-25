using Product.Dtos.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Product.Interfaces
{
    public interface IProductCategoryAppService : IApplicationService
    {
        Task<PagedResultDto<ProductCategoryDto>> GetListAsync(GetProductCategoryListDto dto);

        Task<Guid> CreateAsync(CreateUpdateProductCategoryDto dto);

        Task<CreateUpdateProductCategoryDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateProductCategoryDto dto);

        Task DeleteAsync(Guid id);
    }
}
