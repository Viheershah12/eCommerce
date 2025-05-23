using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using AutoMapper;
using Customer.Dtos.Customer;
using Customer.Dtos.CustomerGroup;
using System;
using Volo.Abp.AutoMapper;
using Volo.Abp.Data;
using Volo.Abp.Identity;

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

        // Customer
        CreateMap<CreateUpdateCustomerDto, IdentityUserCreateDto>()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.RoleNames);

        CreateMap<CreateUpdateCustomerDto, IdentityUserUpdateDto>()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.RoleNames);

        CreateMap<Models.Customer, CustomerDto>();
        CreateMap<IdentityUser, CustomerDto>()
            .ForMember(dest => dest.HomePhoneNumber, opt => opt.MapFrom(src => src.GetProperty("HomePhoneNumber", string.Empty)))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.GetProperty("Gender", null)))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.GetProperty("DateOfBirth", null)))
            .ForMember(dest => dest.BillingAddress, opt => opt.MapFrom(src => src.GetProperty("BillingAddress", null)))
            .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.GetProperty("ShippingAddress", null)))
            .ForMember(dest => dest.IdentificationType, opt => opt.MapFrom(src => src.GetProperty("IdentificationType", null)))
            .ForMember(dest => dest.IdentificationNo, opt => opt.MapFrom(src => src.GetProperty("IdentificationNo", string.Empty)));

        CreateMap<AddressDto, UserAddress>();
    }
}
