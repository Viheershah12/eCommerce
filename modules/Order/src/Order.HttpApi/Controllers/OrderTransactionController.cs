using Abp.eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Order.Controllers
{
    [Area(OrderRemoteServiceConsts.ModuleName)]
    [RemoteService(Name = OrderRemoteServiceConsts.RemoteServiceName)]
    [Route("api/order/ordertransaction")]
    public class OrderTransactionController : OrderController, IOrderTransactionAppService
    {
        #region Fields
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        #endregion

        #region CTOR
        public OrderTransactionController(IOrderTransactionAppService orderTransactionAppService) 
        {
            _orderTransactionAppService = orderTransactionAppService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("getList")]
        public async Task<BasePagedModel<OrderDto>> GetListAsync(GetOrderListDto dto)
        {
            return await _orderTransactionAppService.GetListAsync(dto);
        }

        [HttpPost]
        [Route("create")]
        public async Task<Guid> CreateAsync(CreateUpdateOrderDto dto)
        {
            return await _orderTransactionAppService.CreateAsync(dto);
        }

        [HttpGet]
        [Route("get")]
        public async Task<CreateUpdateOrderDto> GetAsync(Guid id)
        {
            return await _orderTransactionAppService.GetAsync(id);
        }

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(CreateUpdateOrderDto dto)
        {
            await _orderTransactionAppService.UpdateAsync(dto);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task DeleteAsync(Guid id)
        {
            await _orderTransactionAppService.DeleteAsync(id);
        }
        #endregion

        #region Order Note
        [HttpPost]
        [Route("createOrderNote")]
        public async Task CreateOrderNoteAsync(CreateUpdateOrderNoteDto dto)
        {
            await _orderTransactionAppService.CreateOrderNoteAsync(dto);
        }

        [HttpGet]
        [Route("getOrderNote")]
        public async Task<CreateUpdateOrderNoteDto> GetOrderNoteAsync(IdOrderIdModel dto)
        {
            return await _orderTransactionAppService.GetOrderNoteAsync(dto);
        }

        [HttpPut]
        [Route("updateOrderNote")]
        public async Task UpdateOrderNoteAsync(CreateUpdateOrderNoteDto dto)
        {
            await _orderTransactionAppService.UpdateOrderNoteAsync(dto);
        }

        [HttpDelete]
        [Route("deleteOrderNote")]
        public async Task DeleteOrderNoteAsync(IdOrderIdModel dto)
        {
            await _orderTransactionAppService.DeleteOrderNoteAsync(dto);
        }
        #endregion

        #region Other Methods
        [HttpGet]
        [Route("getOrderDetail")]
        public async Task<OrderDetailDto> GetOrderDetailAsync(Guid id)
        {
            return await _orderTransactionAppService.GetOrderDetailAsync(id);
        }

        [HttpPut]
        [Route("cancel")]
        public async Task CancelAsync(Guid orderId)
        {
            await _orderTransactionAppService.CancelAsync(orderId);
        }
        #endregion
    }
}
