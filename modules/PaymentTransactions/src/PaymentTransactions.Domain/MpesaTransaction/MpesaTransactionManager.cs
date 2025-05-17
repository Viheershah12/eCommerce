using PaymentTransactions.Dtos.MpesaTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace PaymentTransactions.MpesaTransaction
{
    public class MpesaTransactionManager : DomainService
    {
        #region Fields
        private readonly IMpesaTransactionRepository _mpesaTransactionRepoisitory;
        #endregion

        #region CTOR
        public MpesaTransactionManager(IMpesaTransactionRepository mpesaTransactionRepository) 
        {
            _mpesaTransactionRepoisitory = mpesaTransactionRepository;
        }
        #endregion

        public async Task<(List<Models.MpesaTransaction> items, int totalCount)> GetMpesaTransactionListing(GetMpesaTransactionListDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Sorting))
                dto.Sorting = nameof(Models.MpesaTransaction.ResultCode);

            var result = await _mpesaTransactionRepoisitory.GetListAsync(dto.SkipCount, dto.MaxResultCount, dto.Sorting);
            var totalCount = await _mpesaTransactionRepoisitory.CountAsync();

            return (result, totalCount);
        }
    }
}
