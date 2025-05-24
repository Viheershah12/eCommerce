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
        #region CRUD
        Task<BasePagedModel<OrderDto>> GetListAsync(GetOrderListDto dto);

        Task<Guid> CreateAsync(CreateUpdateOrderDto dto);

        Task<CreateUpdateOrderDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateOrderDto dto);

        Task DeleteAsync(Guid id);
        #endregion 

        #region Order Note
        Task CreateOrderNoteAsync(CreateUpdateOrderNoteDto dto);

        Task<CreateUpdateOrderNoteDto> GetOrderNoteAsync(IdOrderIdModel dto);

        Task UpdateOrderNoteAsync(CreateUpdateOrderNoteDto dto);

        Task DeleteOrderNoteAsync(IdOrderIdModel dto);
        #endregion

        #region Other Methods
        Task<OrderDetailDto> GetOrderDetailAsync(Guid id);

        Task CancelAsync(Guid orderId);
        #endregion 
    }
}
