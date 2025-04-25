using Product.Localization;
using Volo.Abp.Application.Services;

namespace Product;

public abstract class ProductsAppService : ApplicationService
{
    protected ProductsAppService()
    {
        LocalizationResource = typeof(ProductResource);
        ObjectMapperContext = typeof(ProductApplicationModule);
    }
}
