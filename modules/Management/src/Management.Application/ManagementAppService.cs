using Management.Localization;
using Volo.Abp.Application.Services;

namespace Management;

public abstract class ManagementAppService : ApplicationService
{
    protected ManagementAppService()
    {
        LocalizationResource = typeof(ManagementResource);
        ObjectMapperContext = typeof(ManagementApplicationModule);
    }
}
