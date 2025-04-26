
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Customer.CustomerGroup
{
    public interface ICustomerGroupRepository : IRepository<Models.CustomerGroup, Guid>
    {
        Task<List<Models.CustomerGroup>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
