using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.MvcNotifications
{
    public enum eCommerceNotificationTypeEnum
    {
        info = 1,
        warning = 2,
        success = 3,
        error = 4
    }

    public enum RenderTypeEnum
    {
        toastr = 1,
        popup = 2
    }
}
