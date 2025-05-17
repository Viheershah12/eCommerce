using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Store;
using AngleSharp.Css.Dom;
using AutoMapper;
using Order.Dtos.OrderTransaction;
using Product.Dtos.Product;
using Volo.Abp.Application.Dtos;

namespace Abp.eCommerce.Web.Public;

public class eCommerceWebPublicAutoMapperProfile : Profile
{
    public eCommerceWebPublicAutoMapperProfile()
    {
        // Store
        CreateMap<BasePagedModel<StoreProductDto>, BasePagedModel<ProductItemViewModel>>();
        CreateMap<StoreProductDto, ProductItemViewModel>();

        CreateMap<CreateUpdateProductDto, ProductViewModel>();
        CreateMap<CreateUpdateProductDto.ProductTagDto, ProductViewModel.ProductTagViewModel>();

        // Order
        CreateMap<BasePagedModel<OrderDto>, BasePagedModel<OrderItemViewModel>>();
        CreateMap<OrderDto, OrderItemViewModel>();
    }
}
