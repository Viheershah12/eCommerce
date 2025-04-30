using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.CmsKit.Admin.Blogs;

namespace Abp.eCommerce.Web.Public.Themes.Basic.Components.BlogMenu
{
    public class BlogMenuViewComponent : AbpViewComponent
    {
        private readonly IBlogAdminAppService _blogAdminAppService;

        public BlogMenuViewComponent(IBlogAdminAppService blogAdminAppService)
        {
            _blogAdminAppService = blogAdminAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var topics = await _blogAdminAppService.GetListAsync(new BlogGetListInput
            {
                MaxResultCount = 1000
            }); 

            return View("~/Themes/Basic/Components/BlogMenu/Default.cshtml", topics.Items.ToList());
        }
    }
}
