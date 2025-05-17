using Order.Dtos.OrderTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Order.Order
{
    public class OrderManager : DomainService
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;     
        #endregion

        #region CTOR
        public OrderManager(
            IOrderRepository orderRepository
        ) 
        {
            _orderRepository = orderRepository;
        }
        #endregion

        public async Task<(List<Models.Order> items, int totalCount)> GetOrderListing(GetOrderListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.Order.CustomerName);

            var result = await _orderRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _orderRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.CustomerName.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
