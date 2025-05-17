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
        Pending = 5,

        [Description("PendingConfirmed")]
        PendingConfirmed = 10,

        [Description("Completed")]
        Completed = 15,

        [Description("Failed")]
        Failed = 20,

        [Description("Cancelled")]
        Cancelled = 25
    }

    public enum MpesaTransactionStatusEnum
    {
        [Description("Pending")]
        Pending,               // STK push request initiated, awaiting user input

        [Description("Sent")]
        Sent,                  // Request sent to Safaricom successfully

        [Description("AwaitingUserInput")]
        AwaitingUserInput,     // Awaiting user to input PIN on phone

        [Description("Success")]
        Success,               // User completed the transaction (callback confirms)

        [Description("Failed")]
        Failed,                // Transaction failed (callback error, timeout, or declined)

        [Description("Cancelled")]
        Cancelled,             // User cancelled via phone or app

        [Description("Timeout")]
        Timeout,               // User did not respond in time

        [Description("Error")]
        Error,                 // System or API error before reaching Safaricom

        [Description("Confirmed")]
        Confirmed,             // Optional: Post-success business logic confirmation

        [Description("Reversed")]
        Reversed               // If refund or reversal occurs
    }
}
