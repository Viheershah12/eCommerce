using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Order.ShoppingCart
{
    public interface IShoppingCartRepository : IRepository<Models.ShoppingCart, Guid>
    {
        Task<List<Models.ShoppingCart>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string? filter = null, Guid? userId = null);
    }
}
