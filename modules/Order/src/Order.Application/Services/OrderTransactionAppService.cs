using Abp.eCommerce.Models;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using Order.Order;
using Product.Dtos.Common;
using Product.Dtos.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Order.Services
{
    public class OrderTransactionAppService : OrderAppService, IOrderTransactionAppService
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly OrderManager _orderManager; 
        #endregion

        #region CTOR
        public OrderTransactionAppService(
            IOrderRepository orderRepository,
            OrderManager orderManager
        ) 
        {
            _orderRepository = orderRepository;
            _orderManager = orderManager;   
        }
        #endregion 

        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto dto)
        {
            try
            {
                var (list, count) = await _orderManager.GetOrderListing(dto);
                var items = ObjectMapper.Map<List<Models.Order>, List<OrderDto>>(list);

                return new PagedResultDto<OrderDto>(count, items);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(message: ex.Message);            
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateOrderDto dto)
        {
            try
            {
                var orderDto = ObjectMapper.Map<CreateUpdateOrderDto, Models.Order>(dto);
                var order = await _orderRepository.InsertAsync(orderDto);

                return order.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateOrderDto> GetAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.Order, CreateUpdateOrderDto>(order);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateOrderDto dto)
        {
            try
            {
                var order = await _orderRepository.GetAsync(dto.Id);

                var updatedOrder = ObjectMapper.Map<CreateUpdateOrderDto, Models.Order>(dto);
                updatedOrder.ConcurrencyStamp = order.ConcurrencyStamp;

                await _orderRepository.UpdateAsync(updatedOrder, true);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(id);
                await _orderRepository.DeleteAsync(order);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
