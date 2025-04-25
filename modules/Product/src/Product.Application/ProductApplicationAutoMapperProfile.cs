using Abp.eCommerce.Models;
using AutoMapper;
using Management.Dtos.File;
using Product.Dtos.Common;
using Product.Dtos.ProductCategory;
using Volo.Abp.AutoMapper;

namespace Product;

public class ProductApplicationAutoMapperProfile : Profile
{
    public ProductApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Product

        // Product Category
        CreateMap<Models.ProductCategory, ProductCategoryDto>();
        CreateMap<Media, MediaDto>();

        CreateMap<Models.ProductCategory, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.UploadedMedia)
            .Ignore(x => x.CategoryMedia);

        CreateMap<CreateUpdateProductCategoryDto, Models.ProductCategory>()
            .Ignore(x => x.CategoryMedia)
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);

        // File
        CreateMap<FileDto, UserFileDto>().ReverseMap();
        CreateMap<UserFileDto, CreateFileDto>();
        CreateMap<FileDto, MediaDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Filename));
    }
}
