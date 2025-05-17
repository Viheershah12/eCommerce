using AutoMapper;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using Volo.Abp.AutoMapper;

namespace PaymentTransactions;

public class PaymentTransactionsApplicationAutoMapperProfile : Profile
{
    public PaymentTransactionsApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Payment Transaction
        CreateMap<Models.PaymentTransaction, PaymentTransactionDto>();
        CreateMap<CreateUpdatePaymentTransactionDto, Models.PaymentTransaction>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp)
            .ReverseMap();

        // Mpesa Transaction
        CreateMap<Models.MpesaTransaction, MpesaTransactionDto>();
        CreateMap<CreateUpdateMpesaTransactionDto, Models.MpesaTransaction>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp)
            .ReverseMap();

        CreateMap<Models.MpesaTransaction.CallbackMetadata, CreateUpdateMpesaTransactionDto.CallbackMetadataDto>().ReverseMap();
        CreateMap<Models.MpesaTransaction.CallbackMetadata.CallbackItem, CreateUpdateMpesaTransactionDto.CallbackMetadataDto.CallbackItemDto>().ReverseMap();   
    }
}
