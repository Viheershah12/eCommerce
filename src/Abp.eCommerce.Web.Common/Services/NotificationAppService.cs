using Abp.eCommerce.MvcNotifications;
using Abp.eCommerce.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Validation;

namespace Abp.eCommerce.Web.Common.Services
{
    public class NotificationAppService : WebCommonServiceAppService, INotificationAppService
    {
        public void ShowNotification(ITempDataDictionary tempData, RenderTypeEnum renderType, eCommerceNotificationTypeEnum notificationType, string message, string? title = null, bool keep = false)
        {
            tempData[eCommerceNotificationKeys.RenderType] = (int)renderType;
            tempData[eCommerceNotificationKeys.Type] = (int)notificationType;
            tempData[eCommerceNotificationKeys.Title] = title;
            tempData[eCommerceNotificationKeys.Message] = message;
            tempData[eCommerceNotificationKeys.Keep] = keep;
        }

        #region Info Notification
        public void ShowInfoToastNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.toastr, eCommerceNotificationTypeEnum.info, message, title);
        }

        public void ShowInfoPopupNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.popup, eCommerceNotificationTypeEnum.info, message, title);
        }
        #endregion

        #region Warning Notification
        public void ShowWarningToastNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.toastr, eCommerceNotificationTypeEnum.warning, message, title);
        }

        public void ShowWarningPopupNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.popup, eCommerceNotificationTypeEnum.warning, message, title);
        }
        #endregion

        #region Success Notification
        public void ShowSuccessToastNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.toastr, eCommerceNotificationTypeEnum.success, message, title);
        }

        public void ShowSuccessPopupNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.popup, eCommerceNotificationTypeEnum.success, message, title);
        }
        #endregion

        #region Error Notification
        public void ShowErrorToastNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.toastr, eCommerceNotificationTypeEnum.error, message, title);
        }

        public void ShowErrorPopupNotification(ITempDataDictionary tempData, string message, string? title = null)
        {
            ShowNotification(tempData, RenderTypeEnum.popup, eCommerceNotificationTypeEnum.error, message, title);
        }
        #endregion

        #region Catch Exception
        // TODO: Not working currently, try to intercept exception
        public void ProcessCatchException(ITempDataDictionary tempData, Exception ex, bool keep = true)
        {
            switch (ex)
            {
                case AbpValidationException validationException:
                    ProcessValidationErrors(tempData, validationException.ValidationErrors);
                    break;

                default:
                    ShowErrorPopupNotification(tempData, ex.Message);
                    break;
            }
        }

        public void ProcessValidationErrors(ITempDataDictionary tempData, IList<ValidationResult> validationErrors, bool keep = true)
        {
            var title = L["Title:ValidationErrors"];
            var message = string.Join("\n", validationErrors.Select(a => a.ErrorMessage).ToList());

            ShowErrorPopupNotification(tempData, message, title);
        }
        #endregion 
    }
}
