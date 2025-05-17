using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum OrderStatus
    {
        [Description("Pending")]
        Pending = 10,

        [Description("Processing")]
        Processing = 20,

        [Description("Completed")]
        Completed = 30,

        [Description("Cancelled")]
        Cancelled = 40
    }

    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 1,

        [Description("UnPaid")]
        UnPaid = 10,

        [Description("PartiallyPaid")]
        PartiallyPaid = 20,

        [Description("Paid")]
        Paid = 30
    }

    public enum ShippingStatus
    {
        [Description("UnPaid")]
        ShippingNotRequired = 10,

        [Description("UnPaid")]
        Pending = 20,

        [Description("UnPaid")]
        PreparedToShipped = 30,

        [Description("UnPaid")]
        PartiallyShipped = 40,

        [Description("UnPaid")]
        Shipped = 50,

        [Description("UnPaid")]
        Delivered = 60,
    }

    public enum PaymentTransactionStatus
    {
        [Description("Pending")]
        Pending,

        [Description("Paid")]
        Paid,

        [Description("Failed")]
        Failed,

        [Description("Cancelled")]
        Cancelled
    }
}
