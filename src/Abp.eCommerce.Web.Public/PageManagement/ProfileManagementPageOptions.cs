using System.Collections.Generic;

namespace Abp.eCommerce.Web.Public.PageManagement;

public class ProfileManagementPageOptions
{
    public List<IProfileManagementPageContributor> Contributors { get; }

    public ProfileManagementPageOptions()
    {
        Contributors = [];
    }
}
