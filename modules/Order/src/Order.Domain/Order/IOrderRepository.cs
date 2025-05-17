using Abp.eCommerce.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Order.Order
{
    public interface IOrderRepository : IRepository<Models.Order, Guid>
    {
        Task<List<Models.Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, OrderStatus? status = null);
    }
}
