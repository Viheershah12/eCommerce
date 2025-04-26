using Customer.Localization;
using Volo.Abp.Application.Services;

namespace Customer;

public abstract class CustomersAppService : ApplicationService
{
    protected CustomersAppService()
    {
        LocalizationResource = typeof(CustomerResource);
        ObjectMapperContext = typeof(CustomerApplicationModule);
    }
}
