using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Dtos.ShoppingCart
{
    public class CreateUpdateShoppingCartDto : BaseUserIdModel  
    {
        public string Username { get; set; } = string.Empty;

        public List<ShoppingCartItemDto> Items { get; set; } = [];

        public partial class ShoppingCartItemDto : BaseIdModel
        {
            public Guid ProductId { get; set; }

            public int Quantity { get; set; }

            public DateTime AddedOn { get; set; }

            public DateTime UpdatedOn { get; set; }
        }
    }

    public class CreateUpdateShoppingCartItemDto : BaseIdModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime AddedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
