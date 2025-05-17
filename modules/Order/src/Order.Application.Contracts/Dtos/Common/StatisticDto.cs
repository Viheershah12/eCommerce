using Order.Dtos.ShoppingCart;
using Order.Dtos.WishList;
using Product.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Dtos.Common
{
    public class StatisticDto
    {
        public List<ShoppingItemDto> ShoppingCartItems { get; set; } = [];

        public List<StoreProductDto> WishListItems { get; set; } = [];

        public int ShoppingCartCount { get; set; }

        public int WishListCount { get; set; }
    }

    public class ShoppingItemDto : StoreProductDto
    {
        public int Quantity { get; set; }

        public Guid CartItemId { get; set; }
    }
}
