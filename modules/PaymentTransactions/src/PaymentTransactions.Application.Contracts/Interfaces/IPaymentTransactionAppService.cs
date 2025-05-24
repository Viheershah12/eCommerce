using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace PaymentTransactions.Interfaces
{
    public interface IPaymentTransactionAppService : IApplicationService
    {
        #region CRUD
        Task<PagedResultDto<PaymentTransactionDto>> GetListAsync(GetPaymentTransactionListDto dto);

        Task<Guid> CreateAsync(CreateUpdatePaymentTransactionDto dto);

        Task<CreateUpdatePaymentTransactionDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdatePaymentTransactionDto dto);

        Task DeleteAsync(Guid id);
        #endregion

        #region Other Methods
        Task<int> GetStatusAsync(Guid transactionId);

        Task UpdatePaymentTransactionNoteAsync(UpdatePaymentTransactionNoteDto dto);

        Task<OrderPaymentTransactionDto> GetOrderPaymentTransactionAsync(Guid orderId);
        #endregion 
    }
}
