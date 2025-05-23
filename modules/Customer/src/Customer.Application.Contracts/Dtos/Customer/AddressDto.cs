using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Dtos.Customer
{
    public class AddressDto : BaseUserModel
    {
        public string Address { get; set; }

        public string? Country { get; set; }

        public string? Locality { get; set; }

        public string? PostalCode { get; set; }

        public string? Sublocality { get; set; }

        public string? AdministrativeAreaLevel1 { get; set; }

        public string? AdministrativeAreaLevel2 { get; set; }

        public string? StreetNumber { get; set; }

        public string? UnitNumber { get; set; }

        public string? BuildingName { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
