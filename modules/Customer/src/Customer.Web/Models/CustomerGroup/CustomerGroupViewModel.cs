using Abp.eCommerce.Models;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Customer.Web.Models.CustomerGroup
{
    public class CustomerGroupViewModel : BaseIdModel
    {
        [Placeholder("Name")]
        [Display(Name = "Name")]    
        [Required]  
        public string Name { get; set; }

        [Placeholder("Description")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Placeholder("DisplayOrder")]
        [Display(Name = "DisplayOrder")]
        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
