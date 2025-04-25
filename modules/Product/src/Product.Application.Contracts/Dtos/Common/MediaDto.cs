
using Abp.eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Dtos.Common
{
    public class MediaDto : BaseIdModel
    {
        public string Name { get; set; }
    }
}
