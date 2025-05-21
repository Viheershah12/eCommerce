using Abp.eCommerce.Dtos.MpesaCallback;
using Abp.eCommerce.Enums;
using Abp.eCommerce.Etos.MpesaTransaction;
using Abp.eCommerce.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Order.Interfaces;
using Order.Models;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Interfaces;
using PaymentTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EventBus.Distributed;

namespace Abp.eCommerce.Services
{
    public class MpesaCallbackAppService : eCommerceAppService, IMpesaCallbackAppService
    {
        #region Fields
        private readonly IMpesaTransactionAppService _mpesaTransactionAppService;
        private readonly IPaymentTransactionAppService _paymentTransactionAppService;
        private readonly IOrderTransactionAppService _orderTransactionAppService;
        private readonly ILogger<MpesaCallbackAppService> _logger;
        private readonly IDistributedEventBus _distributedEventBus;
        #endregion

        #region CTOR
        public MpesaCallbackAppService(
            IMpesaTransactionAppService mpesaTransactionAppService,
            IPaymentTransactionAppService paymentTransactionAppService,
            IOrderTransactionAppService orderTransactionAppService,
            ILogger<MpesaCallbackAppService> logger,
            IDistributedEventBus distributedEventBus
        )
        {
            _mpesaTransactionAppService = mpesaTransactionAppService;
            _paymentTransactionAppService = paymentTransactionAppService;
            _orderTransactionAppService = orderTransactionAppService;   
            _logger = logger;
            _distributedEventBus = distributedEventBus;
        }
        #endregion 

        public async Task HandleStkCallbackAsync(MpesaStkCallbackDto input)
        {
            try
            {
                var stk = input.Body?.StkCallback;
                if (stk == null)
                {
                    _logger.LogWarning("Empty STK callback received.");
                    return;
                }

                _logger.LogInformation("Received STK Callback: {@Callback}", stk);

                // Try to locate the MpesaTransaction using CheckoutRequestId
                var transaction = await _mpesaTransactionAppService.GetByCheckoutRequestIdAsync(stk.CheckoutRequestID);
                var paymentTransaction = await _paymentTransactionAppService.GetAsync(transaction.PaymentTransactionId);
                var order = await _orderTransactionAppService.GetAsync(paymentTransaction.OrderId);

                // Update transaction details
                transaction.ResultCode = stk.ResultCode;
                transaction.ResultDesc = stk.ResultDesc;
                transaction.ConfirmedOn = DateTime.Now;

                switch (stk.ResultCode)
                {
                    case 0:
                        transaction.Status = MpesaTransactionStatusEnum.Confirmed;
                        break;
                    case 1:
                        transaction.Status = MpesaTransactionStatusEnum.Failed;
                        break;
                    case 1032:
                        transaction.Status = MpesaTransactionStatusEnum.Cancelled;
                        break;
                    case 1037:
                        transaction.Status = MpesaTransactionStatusEnum.Timeout;
                        break;
                    case 2001:
                        transaction.Status = MpesaTransactionStatusEnum.Error;
                        break;
                    default:
                        transaction.Status = MpesaTransactionStatusEnum.Failed;
                        break;
                }

                if (stk.ResultCode == 0 && stk.CallbackMetadata?.Item != null)
                {
                    var metadata = stk.CallbackMetadata.Item;

                    transaction.MpesaReceiptNumber = metadata.FirstOrDefault(i => i.Name == "MpesaReceiptNumber")?.Value?.ToString();
                    transaction.PhoneNumber = metadata.FirstOrDefault(i => i.Name == "PhoneNumber")?.Value?.ToString();
                    transaction.Metadata = new CreateUpdateMpesaTransactionDto.CallbackMetadataDto
                    {
                        Item = metadata.Select(m => new CreateUpdateMpesaTransactionDto.CallbackMetadataDto.CallbackItemDto
                        {
                            Name = m.Name,
                            Value = m.Value
                        }).ToList()
                    };

                    // Update linked PaymentTransaction status
                    paymentTransaction.Status = PaymentTransactionStatus.Completed;
                    paymentTransaction.PaymentDate = DateTime.Now;

                    await _paymentTransactionAppService.UpdateAsync(paymentTransaction);

                    // Update Order
                    order.PaidDate = DateTime.Now;
                    order.PaymentStatus = PaymentStatus.Paid;
                    order.Status = OrderStatus.Processing;
                    order.PaymentMethod = PaymentMethodEnum.MpesaStk;
                    order.PaymentMethodSystemName = PaymentMethodEnum.MpesaStk.ToString();

                    await _orderTransactionAppService.UpdateAsync(order);
                }

                await _mpesaTransactionAppService.UpdateAsync(transaction);

                var message = "";

                switch (paymentTransaction.Status)
                {
                    case PaymentTransactionStatus.Pending:
                        message = L["PaymentPending"];
                        break;

                    case PaymentTransactionStatus.PendingConfirmed:
                        message = L["PaymentPendingConfirmed"];
                        break;

                    case PaymentTransactionStatus.Completed:
                        message = L["PaymentCompleted"];
                        break;

                    case PaymentTransactionStatus.Failed:
                        message = L["PaymentFailed"];
                        break;

                    case PaymentTransactionStatus.Cancelled:
                        message = L["PaymentCancelled"];
                        break;
                }

                await _distributedEventBus.PublishAsync(new MpesaTransactionStatusEto
                {
                    Status = paymentTransaction.Status,
                    CustomerId = order.CustomerId,
                    OrderId = order.Id,
                    Message = message
                });

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }            
        }
    }
}
