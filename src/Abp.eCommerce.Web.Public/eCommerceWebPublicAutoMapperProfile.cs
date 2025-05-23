using Abp.eCommerce.Models;
using Abp.eCommerce.Web.Public.Models.Order;
using Abp.eCommerce.Web.Public.Models.Store;
using Abp.eCommerce.Web.Public.Pages.Account.Components.ProfileManagementGroup.PersonalInfo;
using AngleSharp.Css.Dom;
using AutoMapper;
using Customer.Dtos.Customer;
using Order.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.MpesaTransaction;
using PaymentTransactions.Dtos.OrderTransaction;
using PaymentTransactions.Dtos.PaymentTransaction;
using Product.Dtos.Product;
using System.Collections.Generic;
using Volo.Abp.Account;
using Volo.Abp.Application.Dtos;
using static OpenIddict.Abstractions.OpenIddictConstants;
using Volo.Abp.Data;
using Volo.Abp.ObjectExtending;
using Abp.eCommerce.Web.Public.Models.Profile;

namespace Abp.eCommerce.Web.Public;

public class eCommerceWebPublicAutoMapperProfile : Profile
{
    public eCommerceWebPublicAutoMapperProfile()
    {
        // Components
        CreateMap<ProfileDto, AccountProfilePersonalInfoManagementGroupViewComponent.PersonalInfoModel>()
            .MapExtraProperties(MappingPropertyDefinitionChecks.None);

        CreateMap<UserAddress, UserAddressViewModel>();

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
