using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum PaymentMethodEnum
    {
        [Description("CashOnDelivery")]
        CashOnDelivery = 1,

        [Description("MpesaStk")]
        MpesaStk 
    }
}
