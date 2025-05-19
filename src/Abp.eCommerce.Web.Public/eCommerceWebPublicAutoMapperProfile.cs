using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Store;
using AngleSharp.Css.Dom;
using AutoMapper;
using Order.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
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

        CreateMap<CreateUpdateOrderDto, OrderDetailViewModel>();
        CreateMap<CreateUpdateOrderDto.AddressDto, AddressViewModel>();
        CreateMap<CreateUpdateOrderDto.OrderItemDto, OrderDetailViewModel.OrderItemViewModel>();

        CreateMap<GetOrderPaymentDetailDto, PaymentDetailViewModel>();
        CreateMap<PaymentTransactionDto, PaymentTransactionViewModel>();
        CreateMap<MpesaTransactionDto, MpesaTransactionViewModel>();
    }
}
