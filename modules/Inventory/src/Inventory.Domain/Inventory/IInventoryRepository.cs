using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inventory.Inventory
{
    public interface IInventoryRepository : IRepository<Models.Inventory, Guid>
    {
        Task<List<Models.Inventory>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
