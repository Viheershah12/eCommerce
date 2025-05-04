using Microsoft.AspNetCore.Mvc;
using Order.Dtos.Common;
using Order.Interfaces;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;

namespace Abp.eCommerce.Web.Public.Themes.Basic.Components.MainNavbar;

public class MainNavbarViewComponent : AbpViewComponent
{
    private readonly ICommonAppService _commonAppService;
    private readonly ICurrentUser _currentUser;

    public MainNavbarViewComponent(
        ICommonAppService commonAppService,
        ICurrentUser currentUser
    )
    {
        _commonAppService = commonAppService;
        _currentUser = currentUser;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync()
    {
        var statistic = new StatisticDto();

        if (_currentUser.Id.HasValue)
        {
            statistic = await _commonAppService.GetStatisticsAsync(_currentUser.Id.Value);
        }

        return View("~/Themes/Basic/Components/MainNavbar/Default.cshtml", statistic);
    }
}
