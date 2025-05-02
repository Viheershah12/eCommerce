using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Store;
using AngleSharp.Css.Dom;
using AutoMapper;
using Product.Dtos.Product;
using Volo.Abp.Application.Dtos;

namespace Abp.eCommerce.Web.Public;

public class eCommerceWebPublicAutoMapperProfile : Profile
{
    public eCommerceWebPublicAutoMapperProfile()
    {
        CreateMap<PagedResultDto<StoreProductDto>, BasePagedModel<ProductItemViewModel>>();
        CreateMap<StoreProductDto, ProductItemViewModel>();

        CreateMap<CreateUpdateProductDto, ProductViewModel>();
        CreateMap<CreateUpdateProductDto.ProductTagDto, ProductViewModel.ProductTagViewModel>();
    }
}
