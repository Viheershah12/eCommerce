
using Abp.eCommerce.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Abp.eCommerce.Web.Public.Models.Store
{
    public class ProductViewModel : BaseIdModel
    {
        public string Name { get; set; }

        public Guid? Category { get; set; }

        public string? Description { get; set; }

        public string? SKU { get; set; }

        #region Product Tags

        public ProductTagViewModel[]? ProductTags { get; set; } = [];

        public partial class ProductTagViewModel : BaseIdModel
        {
            public string Name { get; set; }
        }
        #endregion

        #region Limited To Customer Group
        public string[]? LimitedToCustomerGroupIds { get; set; } = [];

        public CustomerGroupViewModel[]? LimitedToCustomerGroups { get; set; } = [];

        public partial class CustomerGroupViewModel : BaseIdModel
        {
            public string Name { get; set; }
        }
        #endregion 

        public bool IsPublished { get; set; }

        public bool IsNew { get; set; }

        public bool IsFeatured { get; set; }

        public DateTime? AvailableFrom { get; set; }

        public DateTime? AvailableTo { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; } 

        public List<UserFileDto>? UploadedMedia { get; set; }

        public decimal Stock { get; set; }
    }

    public class AddToCartDto
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
