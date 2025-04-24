using Management.Dtos.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Management.Interfaces
{
    public interface IContentAppService : IApplicationService
    {
        Task<PagedResultDto<ContentDto>> GetListAsync(GetContentListDto dto);

        Task<Guid> CreateAsync(CreateUpdateContentDto dto);

        Task<CreateUpdateContentDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateContentDto dto);

        Task DeleteAsync(Guid id);
    }
}
