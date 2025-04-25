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

        public ProductCategory? Category { get; set; }

        public partial class ProductCategory : BaseIdModel
        {
            public string Name { get; set; }
        }

        public string? Description { get; set; }

        public string? SKU { get; set; }

        public string[] ProductTags { get; set; } = [];

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; } 

        public bool IsFeatured { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public decimal Price { get; set; }

        public decimal CostPrice { get; set; } //For Internal Use

        public List<Media> Media { get; set; } = [];
    }
}
