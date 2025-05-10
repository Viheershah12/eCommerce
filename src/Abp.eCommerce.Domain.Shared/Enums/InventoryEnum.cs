using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum StockMovementType
    {
        ManualAdjustment,
        Sale,
        Restock,
        Return,
        Reservation,
        ReleaseReservation
    }
}
