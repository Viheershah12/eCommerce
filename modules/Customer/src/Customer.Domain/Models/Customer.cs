using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Customer.Models
{
    public class Customer : IdentityUser
    {
        public string? HomePhoneNumber { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public UserAddress? BillingAddress { get; set; }

        public UserAddress? ShippingAddress { get; set; }

        public IdentificationType? IdentificationType { get; set; }

        public string? IdentificationNo { get; set; }
    }
}
