using System.ComponentModel.DataAnnotations;

namespace Abp.eCommerce.Web.Public.Models.Profile
{
    public class UserAddressViewModel
    {
        [Display(Name = "DisplayName:Address")]
        public string Address { get; set; }

        [Display(Name = "DisplayName:Country")]
        public string Country { get; set; }

        [Display(Name = "DisplayName:Locality")]
        public string? Locality { get; set; }

        [Display(Name = "DisplayName:PostalCode")]
        public string? PostalCode { get; set; }

        public string? Sublocality { get; set; }

        public string? AdministrativeAreaLevel1 { get; set; }

        public string? AdministrativeAreaLevel2 { get; set; }

        [Display(Name = "DisplayName:StreetNumber")]
        public string? StreetNumber { get; set; }

        [Display(Name = "DisplayName:UnitNumber")]
        public string? UnitNumber { get; set; }

        [Display(Name = "DisplayName:BuildingName")]
        public string? BuildingName { get; set; }

        public long Latitude { get; set; }

        public long Longitude { get; set; }
    }
}
