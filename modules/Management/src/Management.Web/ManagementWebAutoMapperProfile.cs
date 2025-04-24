using AutoMapper;
using Management.Dtos.Content;

namespace Management.Web;

public class ManagementWebAutoMapperProfile : Profile
{
    public ManagementWebAutoMapperProfile()
    {
        // Content Management
        CreateMap<Pages.ContentManagement.CreateModel.CreateViewModel, CreateUpdateContentDto>();
        CreateMap<Pages.ContentManagement.EditModel.EditViewModel, CreateUpdateContentDto>()
            .ReverseMap();
    }
}
