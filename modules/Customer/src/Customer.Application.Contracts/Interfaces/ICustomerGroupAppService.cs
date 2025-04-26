using Customer.Dtos.CustomerGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Customer.Interfaces
{
    public interface ICustomerGroupAppService : IApplicationService
    {
        Task<PagedResultDto<CustomerGroupDto>> GetListAsync(GetCustomerGroupListDto dto);

        Task<Guid> CreateAsync(CreateUpdateCustomerGroupDto dto);

        Task<CreateUpdateCustomerGroupDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateCustomerGroupDto dto);

        Task DeleteAsync(Guid id);
    }
}
