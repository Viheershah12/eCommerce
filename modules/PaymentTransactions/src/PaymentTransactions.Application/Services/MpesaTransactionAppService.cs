using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Interfaces;
using PaymentTransactions.MpesaTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace PaymentTransactions.Services
{
    public class MpesaTransactionAppService : PaymentTransactionsAppService, IMpesaTransactionAppService
    {
        #region Fields
        private readonly MpesaTransactionManager _mpesaTransactionManager;
        private readonly IMpesaTransactionRepository _mpesaTransactionRepository;
        #endregion

        #region CTOR
        public MpesaTransactionAppService(
            MpesaTransactionManager mpesaTransactionManager,
            IMpesaTransactionRepository mpesaTransactionRepository
        ) 
        {
            _mpesaTransactionManager = mpesaTransactionManager;
            _mpesaTransactionRepository = mpesaTransactionRepository;
        }
        #endregion

        public async Task<PagedResultDto<MpesaTransactionDto>> GetListAsync(GetMpesaTransactionListDto dto)
        {
            try
            {
                var (list, count) = await _mpesaTransactionManager.GetMpesaTransactionListing(dto);
                var items = ObjectMapper.Map<List<Models.MpesaTransaction>, List<MpesaTransactionDto>>(list);

                return new PagedResultDto<MpesaTransactionDto>(count, items);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<Guid> CreateAsync(CreateUpdateMpesaTransactionDto dto)
        {
            try
            {
                var mpesaTransactionDto = ObjectMapper.Map<CreateUpdateMpesaTransactionDto, Models.MpesaTransaction>(dto);
                var mpesaTransaction = await _mpesaTransactionRepository.InsertAsync(mpesaTransactionDto);

                return mpesaTransaction.Id;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateMpesaTransactionDto> GetAsync(Guid id)
        {
            try
            {
                var mpesaTransaction = await _mpesaTransactionRepository.GetAsync(id);
                var res = ObjectMapper.Map<Models.MpesaTransaction, CreateUpdateMpesaTransactionDto>(mpesaTransaction);

                return res;
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task<CreateUpdateMpesaTransactionDto> GetByCheckoutRequestIdAsync(string id)
        {
            try
            {
                var querable = await _mpesaTransactionRepository.GetQueryableAsync();
                var transaction = querable.FirstOrDefault(x => x.CheckoutRequestId == id);

                return ObjectMapper.Map<Models.MpesaTransaction?, CreateUpdateMpesaTransactionDto>(transaction);
            }
            catch (Exception ex) 
            {
                throw new BusinessException(ex.Message);
            }
        }

        public async Task UpdateAsync(CreateUpdateMpesaTransactionDto dto)
        {
            try
            {
                var mpesaTransaction = await _mpesaTransactionRepository.GetAsync(dto.Id);

                var updatedMpesaTransaction = ObjectMapper.Map<CreateUpdateMpesaTransactionDto, Models.MpesaTransaction>(dto);
                updatedMpesaTransaction.ConcurrencyStamp = mpesaTransaction.ConcurrencyStamp;

                await _mpesaTransactionRepository.UpdateAsync(updatedMpesaTransaction, true);
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
                var mpesaTransaction = await _mpesaTransactionRepository.GetAsync(id);
                await _mpesaTransactionRepository.DeleteAsync(mpesaTransaction);
            }
            catch (Exception ex)
            {
                throw new BusinessException(ex.Message);
            }
        }
    }
}
