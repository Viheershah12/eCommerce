using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product.Models
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public Guid? Category { get; set; }

        public string? Description { get; set; }

        public string? SKU { get; set; }

        #region Product Tags
        public ProductTag[] ProductTags { get; set; } = [];

        public partial class ProductTag : BaseIdModel
        {
            public string Name { get; set; }
        }
        #endregion

        #region Limited To Customer Groups
        public CustomerGroup[] LimitedToCustomerGroups { get; set; } = [];

        public partial class CustomerGroup : BaseIdModel
        {
            public string Name { get; set; }
        }
        #endregion 

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; } 

        public bool IsFeatured { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        #region Prices
        public decimal Price { get; set; }

        public decimal OldPrice { get; set; } //For Internal Use

        public List<TeirPrice> TierPrices { get; set; } = [];

        public partial class TeirPrice : BaseIdModel
        {
            public Guid CustomerGroupId { get; set; } // e.g., Retail, Wholesale, VIP, etc.

            public string? CustomerGroup { get; set; }

            public decimal Price { get; set; }

            public int? MinimumQuantity { get; set; }
        }
        #endregion

        public List<Media>? Media { get; set; }
    }
}
