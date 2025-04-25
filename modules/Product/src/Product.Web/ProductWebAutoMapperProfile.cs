using AutoMapper;
using Product.Dtos.Common;
using Product.Dtos.ProductCategory;
using Product.Web.Models.ProductCategory;
using Volo.Abp.AutoMapper;

namespace Product.Web;

public class ProductWebAutoMapperProfile : Profile
{
    public ProductWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Pages.ProductCategory.CreateModel.CreateViewModel, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.CategoryMedia);

        CreateMap<CreateUpdateProductCategoryDto, Pages.ProductCategory.EditModel.EditViewModel>()
            .Ignore(x => x.CategoryMedia);

        CreateMap<Pages.ProductCategory.EditModel.EditViewModel, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.CategoryMedia);

        CreateMap<MediaDto, UserFile>()
            .ForMember(dest => dest.Filename, opt => opt.MapFrom(x => x.Name))
            .ReverseMap();
    }
}
