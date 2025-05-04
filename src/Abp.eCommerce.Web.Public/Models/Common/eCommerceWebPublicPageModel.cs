using Abp.eCommerce.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Abp.eCommerce.Web.Public.Models.Common;

[Authorize]
public abstract class eCommerceWebPublicPageModel : AbpPageModel
{
    protected eCommerceWebPublicPageModel()
    {
        LocalizationResourceType = typeof(eCommerceResource);
    }

    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    private ICompositeViewEngine _viewEngine => HttpContext.RequestServices.GetRequiredService<ICompositeViewEngine>();
    private ITempDataProvider _tempDataProvider => HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

    public async Task<string> RenderPartialViewToString(string viewPath, object model)
    {
        var viewEngine = LazyServiceProvider.GetRequiredService<ICompositeViewEngine>();
        var tempDataProvider = LazyServiceProvider.GetRequiredService<ITempDataProvider>();

        var actionContext = new ActionContext(HttpContext, RouteData, new PageActionDescriptor());

        var viewResult = _viewEngine.GetView(executingFilePath: null, viewPath, isMainPage: false);
        if (!viewResult.Success)
        {
            throw new InvalidOperationException($"Partial view '{viewPath}' was not found.");
        }

        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        {
            Model = model
        };

        var tempData = new TempDataDictionary(HttpContext, tempDataProvider);

        using var writer = new StringWriter();

        var viewContext = new ViewContext(
            actionContext,
            viewResult.View,
            viewData,
            tempData,
            writer,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return writer.ToString();
    }
}
