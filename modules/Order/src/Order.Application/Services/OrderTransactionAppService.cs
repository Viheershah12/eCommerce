using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using AutoMapper;
using Newtonsoft.Json;
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
using Volo.Abp.Data;
using Volo.Abp.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Order.Services
{
    public class OrderTransactionAppService : OrderAppService, IOrderTransactionAppService
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly OrderManager _orderManager;
        private readonly IdentityUserManager _identityUserManager;
        #endregion

        #region CTOR
        public OrderTransactionAppService(
            IOrderRepository orderRepository,
            OrderManager orderManager,
            IdentityUserManager identityUserManager
        ) 
        {
            _orderRepository = orderRepository;
            _orderManager = orderManager;
            _identityUserManager = identityUserManager;
        }
        #endregion 

        public async Task<BasePagedModel<OrderDto>> GetListAsync(GetOrderListDto dto)
        {
            try
            {
                var (list, count) = await _orderManager.GetOrderListing(dto);
                var items = ObjectMapper.Map<List<Models.Order>, List<OrderDto>>(list);

                return new BasePagedModel<OrderDto>(count, items, dto.MaxResultCount, dto.SkipCount);
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
                var customer = await _identityUserManager.GetByIdAsync(dto.Customer.Id);
                var address = new UserAddress();

                if (dto.SelectedAddress == AddressTypeEnum.Shipping)
                {
                    var shippingAddressDictionary = customer.GetProperty("ShippingAddress");
                    var shippingAddressJson = JsonConvert.SerializeObject(shippingAddressDictionary);
                    address = JsonConvert.DeserializeObject<UserAddress?>(shippingAddressJson) ?? new UserAddress();
                }
                else if (dto.SelectedAddress == AddressTypeEnum.Billing)
                {
                    var billingAddressDictionary = customer.GetProperty("BillingAddress");
                    var billingAddressJson = JsonConvert.SerializeObject(billingAddressDictionary);
                    address = JsonConvert.DeserializeObject<UserAddress?>(billingAddressJson) ?? new UserAddress();
                }

                orderDto.Customer = new Models.Order.CustomerDetail
                {
                    Id = customer.Id,
                    CustomerName = customer.Name + " " + customer.Surname,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    DateOfBirth = customer.GetProperty<DateTime?>("DateOfBirth"),
                    Gender = customer.GetProperty<Gender?>("Gender"),
                    HomePhoneNumber = customer.GetProperty<string?>("HomePhoneNumber"),
                    IdentificationType = customer.GetProperty<IdentificationType?>("IdentificationType"),
                    IdentificationNo = customer.GetProperty<string?>("IdentificationNo"),
                    DeliveryAddress = address
                };

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

        public async Task CancelAsync(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(orderId);
                order.Status = OrderStatus.Cancelled;

                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
