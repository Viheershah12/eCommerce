using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using PaymentTransactions.MpesaTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace PaymentTransactions.PaymentTransaction
{
    public class PaymentTransactionManager : DomainService
    {
        #region Fields
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        #endregion

        #region CTOR
        public PaymentTransactionManager(
            IPaymentTransactionRepository paymentTransactionRepository  
        ) 
        {
            _paymentTransactionRepository = paymentTransactionRepository;
        }
        #endregion 

        public async Task<(List<Models.PaymentTransaction> items, int totalCount)> GetPaymentTransactionListing(GetPaymentTransactionListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.PaymentTransaction.PaymentDate);

            var result = await _paymentTransactionRepository.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting, dto.Filter);

            var totalCount = await _paymentTransactionRepository.CountAsync(x =>
                string.IsNullOrEmpty(dto.Filter) || x.PaymentMethodSystemName.Contains(dto.Filter)
            );

            return (result, totalCount);
        }
    }
}
