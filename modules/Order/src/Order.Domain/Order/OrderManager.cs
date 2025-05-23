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
                dto.Sorting = "Customer.CustomerName";

            var result = await _orderRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter, dto.Status, dto.StartDate, dto.EndDate);

            var totalCount = await _orderRepository.CountAsync(x =>
                (string.IsNullOrEmpty(dto.Filter) || x.Customer.CustomerName.Contains(dto.Filter)) &&
                (!dto.Status.HasValue || x.Status == dto.Status.Value) &&
                (!dto.StartDate.HasValue || x.CreationTime >= dto.StartDate.Value) &&
                (!dto.EndDate.HasValue || x.CreationTime <= dto.EndDate.Value)
            );

            return (result, totalCount);
        }
    }
}
