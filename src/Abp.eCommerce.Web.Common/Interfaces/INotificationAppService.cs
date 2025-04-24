using Abp.eCommerce.MvcNotifications;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.eCommerce.Web.Common.Interfaces
{
    public interface INotificationAppService : IApplicationService
    {
        void ShowNotification(ITempDataDictionary tempData, RenderTypeEnum renderType,
            eCommerceNotificationTypeEnum notificationType, string message, string? title = null, bool keep = false);

        void ShowInfoToastNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowInfoPopupNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowWarningToastNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowWarningPopupNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowSuccessToastNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowSuccessPopupNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowErrorToastNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ShowErrorPopupNotification(ITempDataDictionary tempData, string message, string? title = null);

        void ProcessCatchException(ITempDataDictionary tempData, Exception ex, bool keep = true);

        void ProcessValidationErrors(ITempDataDictionary tempData, IList<ValidationResult> validationErrors, bool keep = true);
    }
}
