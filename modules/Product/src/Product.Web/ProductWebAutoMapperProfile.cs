using Abp.eCommerce.Models;
using AutoMapper;
using Product.Dtos.Common;
using Product.Dtos.Product;
using Product.Dtos.ProductCategory;
using Product.Dtos.ProductTag;
using Product.Web.Models.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.AutoMapper;

namespace Product.Web;

public class ProductWebAutoMapperProfile : Profile
{
    public ProductWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Product
        CreateMap<Pages.Product.CreateModel.CreateViewModel, CreateUpdateProductDto>()
            .Ignore(x => x.UploadedMedia)
            .Ignore(x => x.Media)
            .Ignore(x => x.LimitedToCustomerGroups)
            .Ignore(x => x.TeirPrices)
            .ForMember(dest => dest.ProductTags, opt => opt.MapFrom(x =>
                x.ProductTagIds != null
                    ? x.ProductTagIds.Select(id => new CreateUpdateProductDto.ProductTagDto { Id = id.To<Guid>() }).ToList()
                    : new List<CreateUpdateProductDto.ProductTagDto>()
            ));

        CreateMap<Pages.Product.EditModel.EditViewModel, CreateUpdateProductDto>()
            .Ignore(x => x.UploadedMedia)
            .Ignore(x => x.Media)
            .Ignore(x => x.LimitedToCustomerGroups)
            .Ignore(x => x.TeirPrices)
            .ForMember(dest => dest.ProductTags, opt => opt.MapFrom(x =>
                x.ProductTagIds != null
                    ? x.ProductTagIds.Select(id => new CreateUpdateProductDto.ProductTagDto { Id = id.To<Guid>() }).ToList()
                    : new List<CreateUpdateProductDto.ProductTagDto>()
            ));

        CreateMap<CreateUpdateProductDto, Pages.Product.EditModel.EditViewModel>()
            .Ignore(x => x.Media)
            .Ignore(x => x.ProductTagIds);

        CreateMap<CreateUpdateProductDto.ProductTagDto, Pages.Product.EditModel.EditViewModel.ProductTagViewModel>();

        // Product Category
        CreateMap<Pages.ProductCategory.CreateModel.CreateViewModel, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.CategoryMedia);

        CreateMap<CreateUpdateProductCategoryDto, Pages.ProductCategory.EditModel.EditViewModel>()
            .Ignore(x => x.CategoryMedia);

        CreateMap<Pages.ProductCategory.EditModel.EditViewModel, CreateUpdateProductCategoryDto>()
            .Ignore(x => x.CategoryMedia);

        // Product Tag
        CreateMap<Pages.ProductTag.CreateModel.CreateViewModel, CreateUpdateProductTagDto>();

        CreateMap<Pages.ProductTag.EditModel.EditViewModel, CreateUpdateProductTagDto>()
            .ReverseMap();

        // File
        CreateMap<MediaDto, UserFile>()
            .ForMember(dest => dest.Filename, opt => opt.MapFrom(x => x.Name))
            .ReverseMap();
    }
}
