using Product.Dtos.ProductTag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Product.Interfaces
{
    public interface IProductTagAppService : IApplicationService
    {
        Task<PagedResultDto<ProductTagDto>> GetListAsync(GetProductTagListDto dto);

        Task<Guid> CreateAsync(CreateUpdateProductTagDto dto);

        Task<CreateUpdateProductTagDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateProductTagDto dto);

        Task DeleteAsync(Guid id);
    }
}
