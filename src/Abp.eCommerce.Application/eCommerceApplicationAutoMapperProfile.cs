using AutoMapper;
using PaymentTransactions.Dtos.MpesaTransaction;
using Volo.Abp.AutoMapper;

namespace Abp.eCommerce;

public class eCommerceApplicationAutoMapperProfile : Profile
{
    public eCommerceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Mpesa
        CreateMap<MpesaTransactionDto, CreateUpdateMpesaTransactionDto>()
            .Ignore(x => x.Metadata);
    }
}
