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
        Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto dto);

        Task<Guid> CreateAsync(CreateUpdateProductDto dto);

        Task<CreateUpdateProductDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateProductDto dto);

        Task DeleteAsync(Guid id);
    }
}
