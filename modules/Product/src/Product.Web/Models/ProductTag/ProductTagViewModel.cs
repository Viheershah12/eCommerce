using Abp.eCommerce.Models;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Product.Web.Models.ProductTag
{
    public class ProductTagViewModel : BaseIdModel
    {
        [Placeholder("Name")]
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Placeholder("Description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
