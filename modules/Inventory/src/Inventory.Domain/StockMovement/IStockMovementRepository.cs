using Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Inventory.StockMovement
{
    public interface IStockMovementRepository : IRepository<Models.StockMovement, Guid>
    {
        Task<List<Models.StockMovement>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null);
    }
}
