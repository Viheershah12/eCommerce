using Abp.eCommerce.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Inventory.Web.Models.StockBalance
{
    public class StockBalanceViewModel : BaseIdModel
    {
        [Required]
        [Display(Name = "Product")]
        [Placeholder("Product")]
        public Guid ProductId { get; set; }

        public string? ProductName { get; set; }

        [Display(Name = "StockQuantity")]
        [Placeholder("StockQuantity")]
        public decimal? StockQuantity { get; set; }

        [Display(Name = "ReservedQuantity")]
        [Placeholder("ReservedQuantity")]
        public decimal? ReservedQuantity { get; set; }

        [Display(Name = "ReorderThreshold")]
        [Placeholder("ReorderThreshold")]
        public int ReorderThreshold { get; set; }

        [Display(Name = "AllowBackorder")]
        [Placeholder("AllowBackorder")]
        public bool AllowBackorder { get; set; } = false;
    }
}
