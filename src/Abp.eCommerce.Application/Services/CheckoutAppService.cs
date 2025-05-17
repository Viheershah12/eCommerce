using Abp.eCommerce.Dtos.Checkout;
using Abp.eCommerce.Interfaces;
using Order.Interfaces;
using PaymentTransactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Abp.eCommerce.Services
{
    public class CheckoutAppService : eCommerceAppService, ICheckoutAppService
    {
        #region Fields
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly IPaymentTransactionAppService _paymentTransactionAppService;
        #endregion

        #region CTOR
        public CheckoutAppService(
            IOrderTransactionAppService orderTransactionAppService,
            IPaymentTransactionAppService paymentTransactionAppService
        )
        {
            _orderTransactionAppService = orderTransactionAppService;
            _paymentTransactionAppService = paymentTransactionAppService;
        }
        #endregion

        public async Task<Guid> CheckoutAsync(CheckoutDto dto)
        {
            try
            {
                var orderId = await _orderTransactionAppService.CreateAsync(dto.Order);

                dto.PaymentTransaction.OrderId = orderId;
                var paymentTransactionId = await _paymentTransactionAppService.CreateAsync(dto.PaymentTransaction);

                return paymentTransactionId;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
