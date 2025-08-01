﻿using Volo.Abp.AspNetCore.Mvc.UI.Theming;
using Volo.Abp.DependencyInjection;

namespace Abp.eCommerce.Web.Public.Themes.Basic;

[ThemeName(Name)]
public class BasicTheme : ITheme, ITransientDependency
{
    public const string Name = "Basic";

    public virtual string GetLayout(string name, bool fallbackToDefault = true)
    {
        switch (name)
        {
            case StandardLayouts.Application:
                return "~/Themes/Basic/Layouts/Application.cshtml";
            case StandardLayouts.Account:
                return "~/Themes/Basic/Layouts/Account.cshtml";
            case StandardLayouts.Empty:
                return "~/Themes/Basic/Layouts/Empty.cshtml";
            case "Store":
                return "~/Themes/Basic/Layouts/Store.cshtml";
            default:
                return fallbackToDefault ? "~/Themes/Basic/Layouts/Application.cshtml" : string.Empty;
        }
    }
}
