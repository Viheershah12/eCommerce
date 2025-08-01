﻿using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Inventory.Dtos.Inventory
{
    public class GetStockBalanceListDto : PagedResultRequestDto
    {
        public string? Filter { get; set; }

        public string? Sorting { get; set; }
    }

    public class StockBalanceDto : BaseIdModel
    {
        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal? StockQuantity { get; set; }

        public decimal? ReservedQuantity { get; set; }

        public int ReorderThreshold { get; set; }

        public bool AllowBackorder { get; set; } = false;
    }
}
