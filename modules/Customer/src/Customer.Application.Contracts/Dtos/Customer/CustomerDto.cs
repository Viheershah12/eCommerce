using Abp.eCommerce.Enums;
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Customer.Dtos.Customer
{
    public class GetCustomerListingDto : PagedResultRequestDto  
    {
        public string? Sorting { get; set; }

        public string? Filter { get; set; }
    }

    public class CustomerDto : BaseIdModel
    {
        public bool EmailConfirmed { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool IsActive { get; set; }

        public bool LockoutEnabled { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string? Surname { get; set; } = null;

        public string? Email { get; set; } = null;

        public string? PhoneNumber { get; set; } = null;

        public string? HomePhoneNumber { get; set; } = null;

        public Gender? Gender { get; set; } = null;

        public DateTime? DateOfBirth { get; set; } = null;

        public UserAddress? BillingAddress { get; set; } = null;

        public UserAddress? ShippingAddress { get; set; } = null;

        public IdentificationType? IdentificationType { get; set; } = null;

        public string? IdentificationNo { get; set; } = null;
    }
}
