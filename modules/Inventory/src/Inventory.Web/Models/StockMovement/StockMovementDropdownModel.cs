using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Inventory.Web.Models.StockMovement
{
    public class StockMovementDropdownModel
    {
        public List<SelectListItem> InventoryDropdown { get; set; } = [];

        public List<SelectListItem> StockMovementTypeDropdown { get; set; } = [];

        public List<SelectListItem> OrderDropdown { get; set; } = [];
    }
}
