using Customer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Customer;

public abstract class CustomersController : AbpControllerBase
{
    protected CustomersController()
    {
        LocalizationResource = typeof(CustomerResource);
    }
}
