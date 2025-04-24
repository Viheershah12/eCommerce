using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum ContentType
    {
        [Description("AboutUs")]
        AboutUs = 0,

        [Description("Terms")]
        Terms,

        [Description("Privacy")]
        Privacy
    }
}
