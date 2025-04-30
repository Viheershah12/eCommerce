using Microsoft.AspNetCore.Mvc;
using Product.Dtos.ProductCategory;
using Product.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Admin.Blogs;

namespace Abp.eCommerce.Web.Public.Themes.Basic.Components.BlogMenu
{
    public class ProductMenuViewComponent : AbpViewComponent
    {
        private readonly IProductCategoryAppService _productCategoryAppService;

        public ProductMenuViewComponent(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _productCategoryAppService.GetListAsync(new GetProductCategoryListDto
            {
                MaxResultCount = 1000
            }); 

            var items = categories.Items.Where(x => x.IsActive).ToList();

            return View("~/Themes/Basic/Components/ProductMenu/Default.cshtml", items);
        }
    }
}
