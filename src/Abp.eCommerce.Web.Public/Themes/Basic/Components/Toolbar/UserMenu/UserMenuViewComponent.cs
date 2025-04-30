using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.UI.Navigation;

namespace Abp.eCommerce.Web.Public.Themes.Basic.Components.Toolbar.UserMenu
{
    public class UserMenuViewComponent : AbpViewComponent
    {
        protected IMenuManager MenuManager { get; }

        public UserMenuViewComponent(IMenuManager menuManager)
        {
            MenuManager = menuManager;
        }

        public virtual async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await MenuManager.GetAsync(StandardMenus.User);
            return View("~/Themes/Basic/Components/Toolbar/UserMenu/Default.cshtml", menu);
        }
    }
}
