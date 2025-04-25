using Product.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Product;

public abstract class ProductsController : AbpControllerBase
{
    protected ProductsController()
    {
        LocalizationResource = typeof(ProductResource);
    }
}
