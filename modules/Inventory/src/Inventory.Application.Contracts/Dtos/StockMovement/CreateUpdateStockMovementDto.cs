using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Dtos.StockMovement
{
    public class CreateUpdateStockMovementDto : BaseIdModel
    {
        public Guid InventoryId { get; set; }

        public string ProductName { get; set; }

        public StockMovementType MovementType { get; set; }

        public int QuantityChanged { get; set; }

        public string? Reason { get; set; }

        public Guid? OrderId { get; set; } // Sale Order / Have Restock Management 

        public decimal QuantityBeforeChange { get; set; }

        public decimal QuantityAfterChange { get; set; }
    }
}
