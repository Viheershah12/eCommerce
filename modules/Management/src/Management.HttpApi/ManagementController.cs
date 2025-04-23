using Management.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Management;

public abstract class ManagementController : AbpControllerBase
{
    protected ManagementController()
    {
        LocalizationResource = typeof(ManagementResource);
    }
}
