using Customer.Dtos.CustomerGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Customer.Interfaces
{
    public interface ICommonAppService : IApplicationService
    {
        Task<List<CustomerGroupDto>> GetCutomerGroupByIdAsync(List<Guid> ids);

        Task<List<CustomerGroupDto>> GetCustomerGroupDropdownAsync();
    }
}
