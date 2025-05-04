using Order.Dtos.ShoppingCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Order.Interfaces
{
    public interface IShoppingCartAppService : IApplicationService  
    {
        Task<PagedResultDto<ShoppingCartDto>> GetListAsync(GetShoppingCartListDto dto);

        Task<Guid> CreateAsync(CreateUpdateShoppingCartDto dto);

        Task<CreateUpdateShoppingCartDto> GetAsync(Guid id);

        Task AddShoppingCartItemAsync(CreateUpdateShoppingCartItemDto dto);

        Task UpdateAsync(CreateUpdateShoppingCartDto dto);

        Task DeleteAsync(Guid id);
    }
}
