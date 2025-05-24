using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using AutoMapper;
using Inventory.Dtos.StockMovement;
using Inventory.Interfaces;
using Newtonsoft.Json;
using Order.Dtos.OrderTransaction;
using Order.Interfaces;
using Order.Order;
using PaymentTransactions.Dtos.PaymentTransaction;
using PaymentTransactions.Interfaces;
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
using Volo.Abp.ObjectMapping;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Order.Services
{
    public class OrderTransactionAppService : OrderAppService, IOrderTransactionAppService
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly OrderManager _orderManager;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IStockMovementAppService _stockMovementAppService;
        private readonly IPaymentTransactionAppService _paymentTransactionAppService;
        #endregion

        #region CTOR
        public OrderTransactionAppService(
            IOrderRepository orderRepository,
            OrderManager orderManager,
            IdentityUserManager identityUserManager,
            IStockMovementAppService stockMovementAppService,
            IPaymentTransactionAppService paymentTransactionAppService
        ) 
        {
            _orderRepository = orderRepository;
            _orderManager = orderManager;
            _identityUserManager = identityUserManager;
            _stockMovementAppService = stockMovementAppService;
            _paymentTransactionAppService = paymentTransactionAppService;
        }
        #endregion

        #region CRUD
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

                var saleStockAdjustment = new OrderTransactionMovementDto
                {
                    OrderId = order.Id,
                    OrderItems = order.OrderItems.Select(x => new OrderTransactionMovementDto.OrderItemDto
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        Quantity = x.Quantity
                    }).ToList()
                };

                await _stockMovementAppService.CreateSaleStockMovement(saleStockAdjustment);

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
        #endregion

        #region Order Notes
        public async Task CreateOrderNoteAsync(CreateUpdateOrderNoteDto dto)
        {
            try
            {
                var order = await _orderRepository.GetAsync(dto.OrderId);

                var orderNote = ObjectMapper.Map<CreateUpdateOrderNoteDto, Models.Order.OrderNote>(dto);
                order.OrderNotes.Add(orderNote);

                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateOrderNoteDto> GetOrderNoteAsync(IdOrderIdModel dto)
        {
            try
            {
                var order = await _orderRepository.GetAsync(dto.OrderId);
                var orderNote = order.OrderNotes.First(x => x.Id == dto.Id);

                var res = ObjectMapper.Map<Models.Order.OrderNote, CreateUpdateOrderNoteDto>(orderNote);
                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateOrderNoteAsync(CreateUpdateOrderNoteDto dto)
        {
            try
            {
                var order = await _orderRepository.GetAsync(dto.OrderId);

                var orderNote = ObjectMapper.Map<CreateUpdateOrderNoteDto, Models.Order.OrderNote>(dto);
                order.OrderNotes.RemoveAll(x => x.Id == dto.Id);
                order.OrderNotes.Add(orderNote);

                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task DeleteOrderNoteAsync(IdOrderIdModel dto)
        {
            try
            {
                var order = await _orderRepository.GetAsync(dto.OrderId);
                order.OrderNotes.RemoveAll(x => x.Id == dto.Id);

                await _orderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
        #endregion

        #region Other Method
        public async Task<OrderDetailDto> GetOrderDetailAsync(Guid id)
        {
            try
            {
                var order = await _orderRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.Order, OrderDetailDto>(order);

                // Payment Transaction
                if (order.PaymentStatus == PaymentStatus.Paid)
                {
                    var paymentDetails = await _paymentTransactionAppService.GetOrderPaymentTransactionAsync(order.Id);
                    res.PaymentTransaction = ObjectMapper.Map<OrderPaymentTransactionDto, OrderDetailDto.PaymentTransactionDto>(paymentDetails);
                    res.MpesaTransaction = ObjectMapper.Map<OrderPaymentTransactionDto.MpesaTransactionDto?, OrderDetailDto.MpesaTransactionDto?>(paymentDetails.MpesaTransaction);
                }

                return res;
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
        #endregion
    }
}
