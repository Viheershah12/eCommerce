using AutoMapper;
using Customer.Dtos.CustomerGroup;
using Volo.Abp.AutoMapper;

namespace Customer;

public class CustomerApplicationAutoMapperProfile : Profile
{
    public CustomerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Customer Group
        CreateMap<Models.CustomerGroup, CustomerGroupDto>();
        CreateMap<Models.CustomerGroup, CreateUpdateCustomerGroupDto>();

        CreateMap<CreateUpdateCustomerGroupDto, Models.CustomerGroup>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);
    }
}
