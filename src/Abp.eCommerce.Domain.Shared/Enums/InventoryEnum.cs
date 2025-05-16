using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum StockMovementType
    {
        [Description("ManualAdjustmentPlus")]
        ManualAdjustmentPlus = 1,

        [Description("ManualAdjustmentMinus")]
        ManualAdjustmentMinus,

        [Description("Sale")]
        Sale,

        [Description("Restock")]
        Restock,

        [Description("Return")]
        Return,

        [Description("Reservation")]
        Reservation,

        [Description("ReleaseReservation")]
        ReleaseReservation
    }
}
