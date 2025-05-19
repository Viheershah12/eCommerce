using Abp.eCommerce.Models;
using Order.Dtos.OrderTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Order.Interfaces
{
    public interface IOrderTransactionAppService : IApplicationService
    {
        Task<BasePagedModel<OrderDto>> GetListAsync(GetOrderListDto dto);

        Task<Guid> CreateAsync(CreateUpdateOrderDto dto);

        Task<CreateUpdateOrderDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateOrderDto dto);

        Task DeleteAsync(Guid id);

        Task CancelAsync(Guid orderId);
    }
}
