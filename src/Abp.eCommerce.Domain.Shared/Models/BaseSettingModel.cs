using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Models
{
    public class BaseImageSettingModel 
    {
        public int MaxNumberOfPictures { get; set; }

        public int MaxSize { get; set; }

        public string? Role { get; set; }
    }

    public class BaseVideoSettingModel
    {
        public int MaxNumberOfVideos { get; set; }

        public int MaxSize { get; set; }

        public string? Role { get; set; }
    }

    public class BaseGoogleApiKeySettingModel
    {
        public string? GoogleApiKey { get; set;}
    }

    public class BaseWorkRadiusSettingModel
    {
        public double WorkRadius { get; set; }
    }
}
