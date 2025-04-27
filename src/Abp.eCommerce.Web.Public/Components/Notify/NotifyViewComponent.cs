using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

public class NotifyViewComponent : AbpViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View("/Components/Notify/Default.cshtml");
    }
}
