using Abp.eCommerce.Enums;
using AutoMapper;
using Management.Dtos.Content;
using Management.Models;
using Volo.Abp.AutoMapper;

namespace Management;

public class ManagementApplicationAutoMapperProfile : Profile
{
    public ManagementApplicationAutoMapperProfile()
    {
        //Content Management
        CreateMap<Content, ContentDto>()
            .ForMember(dest => dest.ContentType, opt => opt.MapFrom(x => x.ContentType.GetDescription()))
            .ReverseMap();

        CreateMap<Content, CreateUpdateContentDto>();
        CreateMap<CreateUpdateContentDto, Content>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);
    }
}
