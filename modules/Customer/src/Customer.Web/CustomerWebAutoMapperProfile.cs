using AutoMapper;
using Customer.Dtos.CustomerGroup;

namespace Customer.Web;

public class CustomerWebAutoMapperProfile : Profile
{
    public CustomerWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Customer Group
        CreateMap<Pages.CustomerGroup.CreateModel.CreateViewModel, CreateUpdateCustomerGroupDto>();

        CreateMap<CreateUpdateCustomerGroupDto, Pages.CustomerGroup.EditModel.EditViewModel>()
            .ReverseMap();
    }
}
