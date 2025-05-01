using Abp.eCommerce.Models;
using AutoMapper;
using Management.Dtos.File;
using Product.Dtos.Common;
using Product.Dtos.Product;
using Product.Dtos.ProductCategory;
using Product.Dtos.ProductTag;
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
        CreateMap<Models.Product, ProductDto>();
        CreateMap<Models.Product, StoreProductDto>()
            .Ignore(x => x.Media);
        CreateMap<Models.Product, CreateUpdateProductDto>()
            .Ignore(x => x.Media)
            .Ignore(x => x.UploadedMedia);

        CreateMap<CreateUpdateProductDto, Models.Product>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp)
            .Ignore(x => x.Media);

        CreateMap<Models.Product.TeirPrice, CreateUpdateProductDto.TeirPriceDto>()
            .ReverseMap();

        CreateMap<Models.Product.ProductTag, CreateUpdateProductDto.ProductTagDto>()
            .ReverseMap();

        CreateMap<Models.Product.CustomerGroup, CreateUpdateProductDto.CustomerGroupDto>()
            .ReverseMap();

        // Product Category
        CreateMap<Models.ProductCategory, ProductCategoryDto>();

        CreateMap<Models.ProductCategory, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.UploadedMedia)
            .Ignore(x => x.CategoryMedia);

        CreateMap<CreateUpdateProductCategoryDto, Models.ProductCategory>()
            .Ignore(x => x.CategoryMedia)
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);

        // Product Tag
        CreateMap<Models.ProductTag, ProductTagDto>();
        CreateMap<Models.ProductTag, CreateUpdateProductTagDto>();

        CreateMap<CreateUpdateProductTagDto, Models.ProductTag>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);

        // File
        CreateMap<FileDto, UserFileDto>().ReverseMap();
        CreateMap<UserFileDto, CreateFileDto>();

        CreateMap<FileDto, MediaDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Filename));

        CreateMap<Media, MediaDto>()
            .ReverseMap();
    }
}
