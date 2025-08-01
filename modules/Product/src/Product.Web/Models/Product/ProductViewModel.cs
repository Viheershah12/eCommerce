﻿using Abp.eCommerce.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Product.Web.Models.ProductCategory;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using System.ComponentModel.DataAnnotations;

namespace Product.Web.Models.Product
{
    public class ProductViewModel : BaseIdModel
    {
        [Placeholder("Name")]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Placeholder("Category")]
        [Display(Name = "Category")]
        public Guid? Category { get; set; }

        [TextArea(Rows = 3)]
        [Placeholder("Description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Placeholder("SKU")]
        [Display(Name = "SKU")]
        public string? SKU { get; set; }

        #region Product Tags
        public string[]? ProductTagIds { get; set; } = [];

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

        [Placeholder("AvailableFrom")]
        [Display(Name = "AvailableFrom")]
        public DateTime? AvailableFrom { get; set; }

        [Placeholder("AvailableTo")]
        [Display(Name = "AvailableTo")]
        public DateTime? AvailableTo { get; set; }

        public decimal Price { get; set; }

        public decimal OldPrice { get; set; } //For Internal Use

        public List<IFormFile>? Media { get; set; } 

        public List<UserFileDto>? UploadedMedia { get; set; }
    }
}
