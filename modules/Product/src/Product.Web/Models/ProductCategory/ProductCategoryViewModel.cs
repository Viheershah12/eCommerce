using Abp.eCommerce.Models;
using Microsoft.AspNetCore.Http;
using Product.Dtos.Common;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product.Web.Models.ProductCategory
{
    public class ProductCategoryViewModel : BaseIdModel
    {
        [Placeholder("Name")]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Placeholder("Description")]
        [Display(Name = "Description")]
        [TextArea(Rows = 2)]
        public string? Description { get; set; }

        [Placeholder("Icon")]
        [Display(Name = "Icon")]
        public string? Icon { get; set; }

        public IFormFile? CategoryMedia { get; set; }

        public UserFile? UploadedMedia { get; set; }

        [Placeholder("DisplayOrder")]
        [Display(Name = "DisplayOrder")]
        public int DisplayOrder { get; set; }

        [Placeholder("IsActive")]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
    }

    public class UserFile : BaseIdModel
    {
        public string? Filename { get; set; }
    }
}
