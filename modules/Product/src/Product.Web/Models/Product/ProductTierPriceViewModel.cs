using Abp.eCommerce.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product.Web.Models.Product
{
    public class ProductTierPriceViewModel : BaseIdModel
    {
        public Guid ProductId { get; set; }

        [Placeholder("CustomerGroup")]
        [Display(Name = "CustomerGroup")]
        [Required]
        public Guid CustomerGroupId { get; set; }

        public string? CustomerGroup { get; set; }

        [Placeholder("Price")]
        [Display(Name = "Price")]
        [Required]
        public decimal Price { get; set; }

        [Placeholder("MinimumQuantity")]
        [Display(Name = "MinimumQuantity")]
        public int? MinimumQuantity { get; set; }        
    }
}
