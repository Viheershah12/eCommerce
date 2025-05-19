using Microsoft.Extensions.Logging;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using PaymentTransactions.Interfaces;
using PaymentTransactions.MpesaTransaction;
using PaymentTransactions.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace PaymentTransactions.Services
{
    public class OrderDetailAppService : PaymentTransactionsAppService, IOrderDetailAppService
    {
        #region Fields
        private readonly IPaymentTransactionRepository _paymentTransationRepository;
        private readonly IMpesaTransactionRepository _mpesaTransactionRepository;
        private readonly ILogger<OrderDetailAppService> _logger;
        #endregion

        #region CTOR
        public OrderDetailAppService(
            IPaymentTransactionRepository paymentTransactionRepository,
            IMpesaTransactionRepository mpesaTransactionRepository,
            ILogger<OrderDetailAppService> logger
        )
        {
            _paymentTransationRepository = paymentTransactionRepository;
            _mpesaTransactionRepository = mpesaTransactionRepository;
            _logger = logger;
        }
        #endregion 

        public async Task<GetOrderPaymentDetailDto> GetOrderPaymentDetailAsync(Guid orderId)
        {
            try
            {
                var paymentTransactionQuerable = await _paymentTransationRepository.GetQueryableAsync();
                var paymentTransaction = paymentTransactionQuerable.FirstOrDefault(x => x.OrderId == orderId);

                if (paymentTransaction == null)
                    return new GetOrderPaymentDetailDto();

                var mpesaQuerable = await _mpesaTransactionRepository.GetQueryableAsync();
                var mpesaTransaction = mpesaQuerable.FirstOrDefault(x => x.PaymentTransactionId == paymentTransaction.Id);

                var res = new GetOrderPaymentDetailDto
                {
                    PaymentTransaction = ObjectMapper.Map<Models.PaymentTransaction?, PaymentTransactionDto?>(paymentTransaction),
                    MpesaTransaction = ObjectMapper.Map<Models.MpesaTransaction?, MpesaTransactionDto?>(mpesaTransaction)
                };

                return res;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new GetOrderPaymentDetailDto();
            }
        }
    }
}
