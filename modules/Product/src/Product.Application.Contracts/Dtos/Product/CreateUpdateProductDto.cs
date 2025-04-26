using Abp.eCommerce.Models;
using Product.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace Product.Dtos.Product
{
    public class CreateUpdateProductDto : BaseIdModel
    {
        public string Name { get; set; }

        public Guid? Category { get; set; }

        public string? Description { get; set; }

        public string? SKU { get; set; }

        #region Product Tags 
        public ProductTagDto[] ProductTags { get; set; } = [];

        public partial class ProductTagDto : BaseIdModel
        {
            public string Name { get; set; }
        }
        #endregion

        public string[] LimitedToCustomerGroups { get; set; } = [];

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        #region Prices
        public decimal Price { get; set; }

        public decimal OldPrice { get; set; } //For Internal Use

        public List<TeirPriceDto> TeirPrices { get; set; } = [];

        public partial class TeirPriceDto : BaseIdModel
        {
            public Guid CustomerGroupId { get; set; } // e.g., Retail, Wholesale, VIP, etc.

            public decimal Price { get; set; }

            public int? MinimumQuantity { get; set; }
        }
        #endregion

        public List<MediaDto>? Media { get; set; }

        public List<MediaDto>? UploadedMedia { get; set; }
    }
}
