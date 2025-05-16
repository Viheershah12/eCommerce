using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Inventory.Web.Models.StockMovement
{
    public class StockMovementViewModel : BaseIdModel
    {
        [Required]
        [Placeholder("Inventory")]
        [Display(Name = "Inventory")]
        public Guid InventoryId { get; set; }

        [Required]
        [ReadOnlyInput(true)]
        [Placeholder("ProductName")]
        [Display(Name = "ProductName")]
        public string ProductName { get; set; }

        [Required]
        [Placeholder("MovementType")]
        [Display(Name = "MovementType")]
        public StockMovementType MovementType { get; set; }

        [Required]
        [Placeholder("QuantityChanged")]
        [Display(Name = "QuantityChanged")]
        public int QuantityChanged { get; set; }

        [Placeholder("Reason")]
        [Display(Name = "Reason")]
        public string? Reason { get; set; }

        [Placeholder("Order")]
        [Display(Name = "Order")]
        public Guid? OrderId { get; set; } 

        [Required]
        [ReadOnlyInput(true)]
        [Placeholder("QuantityBeforeChange")]
        [Display(Name = "QuantityBeforeChange")]
        public decimal QuantityBeforeChange { get; set; }

        [Required]
        [ReadOnlyInput(true)]
        [Placeholder("QuantityAfterChange")]
        [Display(Name = "QuantityAfterChange")]
        public decimal QuantityAfterChange { get; set; }
    }
}
