using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Management.Web.Models.ContentManagement
{
    public class ContentViewModel : BaseIdModel
    {
        [Display(Name = "Title")]
        [Placeholder("Title")]
        [Required]
        public string Title { get; set; }

        [Display(Name = "Body")]
        [Placeholder("Body")]
        [TextArea(Rows = 5)]
        [Required]
        public string Body { get; set; }

        [Display(Name = "IsPublished")]
        [Placeholder("IsPublished")]
        public bool IsPublished { get; set; }

        [Display(Name = "ContentType")]
        [Placeholder("ContentType")]
        [Required]
        public ContentType ContentType { get; set; }
    }
}
