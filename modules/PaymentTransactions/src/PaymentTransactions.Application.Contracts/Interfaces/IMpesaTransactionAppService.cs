using PaymentTransactions.Dtos.MpesaTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace PaymentTransactions.Interfaces
{
    public interface IMpesaTransactionAppService : IApplicationService
    {
        Task<PagedResultDto<MpesaTransactionDto>> GetListAsync(GetMpesaTransactionListDto dto);

        Task<Guid> CreateAsync(CreateUpdateMpesaTransactionDto dto);

        Task<CreateUpdateMpesaTransactionDto> GetAsync(Guid id);

        Task UpdateAsync(CreateUpdateMpesaTransactionDto dto);

        Task DeleteAsync(Guid id);
    }
}
