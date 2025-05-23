using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public enum IdentificationType
    {
        [Description("PleaseSelect")]
        PleaseSelect = 0,

        [Description("Passport")]
        Passport,

        [Description("NationalId")]
        NationalId
    }

    public enum Gender
    {
        [Description("PleaseSelect")]
        PleaseSelect = 0,

        [Description("Male")]
        Male,

        [Description("Female")]
        Female,

        [Description("Other")]
        Other
    }
}
