using AutoMapper;
using Order.Dtos.Common;
using Order.Dtos.OrderTransaction;
using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Product.Dtos.Product;
using Volo.Abp.AutoMapper;

namespace Order;

public class OrderApplicationAutoMapperProfile : Profile
{
    public OrderApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // Order
        CreateMap<Models.Order, OrderDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(x => x.Customer.Id))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Customer.CustomerName));

        CreateMap<CreateUpdateOrderDto, Models.Order>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);

        CreateMap<Models.Order, CreateUpdateOrderDto>()
            .Ignore(x => x.SelectedAddress)
            .ReverseMap();

        CreateMap<Models.Order.OrderItem, CreateUpdateOrderDto.OrderItemDto>().ReverseMap();
        CreateMap<Models.Order.CustomerDetail, CreateUpdateOrderDto.CustomerDetailDto>().ReverseMap();

        // Shopping Cart
        CreateMap<Models.ShoppingCart, ShoppingCartDto>()
            .ForMember(dest => dest.CartCount, opt => opt.MapFrom(x => x.Items.Count));

        CreateMap<CreateUpdateShoppingCartDto, Models.ShoppingCart>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp)
            .ReverseMap();

        CreateMap<CreateUpdateShoppingCartDto.ShoppingCartItemDto, Models.ShoppingCart.CartItem>()
            .ReverseMap();

        CreateMap<CreateUpdateShoppingCartItemDto, Models.ShoppingCart.CartItem>()
            .ReverseMap();

        // Wishlist
        CreateMap<Models.WishList, WishListDto>()
            .ForMember(dest => dest.WishListCount, opt => opt.MapFrom(x => x.Items.Count));

        CreateMap<CreateUpdateWishListDto, Models.WishList>()
            .IgnoreFullAuditedObjectProperties()
            .Ignore(x => x.ExtraProperties)
            .Ignore(x => x.ConcurrencyStamp);

        CreateMap<CreateUpdateWishListDto.WishListItemDto, Models.WishList.WishlistItem>()
            .ReverseMap();

        CreateMap<CreateUpdateWishlistItemDto, Models.WishList.WishlistItem>()
            .ReverseMap();

        // Product
        CreateMap<StoreProductDto, ShoppingItemDto>()
            .Ignore(x => x.CartItemId)
            .Ignore(x => x.Quantity);
    }
}
