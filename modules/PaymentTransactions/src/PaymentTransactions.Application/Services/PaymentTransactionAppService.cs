using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Interfaces;
using PaymentTransactions.MpesaTransaction;
using PaymentTransactions.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp;
using PaymentTransactions.Dtos.PaymentTransaction;
using Microsoft.Extensions.Logging;
using Abp.eCommerce.Enums;

namespace PaymentTransactions.Services
{
    public class PaymentTransactionAppService : PaymentTransactionsAppService, IPaymentTransactionAppService
    {
        #region Fields
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly PaymentTransactionManager _paymentTransactionManager;
        private readonly ILogger<PaymentTransactionAppService> _logger;
        #endregion

        #region CTOR
        public PaymentTransactionAppService(
           IPaymentTransactionRepository paymentTransactionRepository,
           PaymentTransactionManager paymentTransactionManager,
           ILogger<PaymentTransactionAppService> logger
        ) 
        {
            _paymentTransactionManager = paymentTransactionManager;
            _paymentTransactionRepository = paymentTransactionRepository;
            _logger = logger;
        }
        #endregion 

        public async Task<int> GetStatusAsync(Guid transactionId)
        {
            try
            {
                var paymentTransaction = await _paymentTransactionRepository.GetAsync(transactionId);
                return (int)paymentTransaction.Status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return (int)PaymentTransactionStatus.Failed;
            }
        }

        public async Task<PagedResultDto<PaymentTransactionDto>> GetListAsync(GetPaymentTransactionListDto dto)
        {
            try
            {
                var (list, count) = await _paymentTransactionManager.GetPaymentTransactionListing(dto);
                var items = ObjectMapper.Map<List<Models.PaymentTransaction>, List<PaymentTransactionDto>>(list);

                return new PagedResultDto<PaymentTransactionDto>(count, items);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdatePaymentTransactionDto dto)
        {
            try
            {
                var paymentTransactionDto = ObjectMapper.Map<CreateUpdatePaymentTransactionDto, Models.PaymentTransaction>(dto);
                var paymentTransaction = await _paymentTransactionRepository.InsertAsync(paymentTransactionDto);

                return paymentTransaction.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdatePaymentTransactionDto> GetAsync(Guid id)
        {
            try
            {
                var paymentTransaction = await _paymentTransactionRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.PaymentTransaction, CreateUpdatePaymentTransactionDto>(paymentTransaction);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdatePaymentTransactionDto dto)
        {
            try
            {
                var paymentTransaction = await _paymentTransactionRepository.GetAsync(dto.Id);

                var updatedPaymentTransaction = ObjectMapper.Map<CreateUpdatePaymentTransactionDto, Models.PaymentTransaction>(dto);
                updatedPaymentTransaction.ConcurrencyStamp = paymentTransaction.ConcurrencyStamp;

                await _paymentTransactionRepository.UpdateAsync(updatedPaymentTransaction, true);
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
                var paymentTransaction = await _paymentTransactionRepository.GetAsync(id);
                await _paymentTransactionRepository.DeleteAsync(paymentTransaction);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
