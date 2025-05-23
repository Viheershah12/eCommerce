using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Helpers
{
    public static class DataTypeHelper
    {
        public static string ToDateFormatDisplay(this DateTime? dt)
        {
            if (dt == null)
            {
                return "-";
            }
            return ((DateTime)dt).ToString("dd/MM/yyyy");
        }

        public static string ToDateFormatDisplay(this DateTime dt)
        {
            if (dt == null)
            {
                return "-";
            }
            return ((DateTime)dt).ToString("dd/MM/yyyy");
        }

        public static string ToDateTimeFormatDisplay(this DateTime? dt)
        {
            if (dt == null)
            {
                return "-";
            }
            DateTime localTime = ((DateTime)dt).ToLocalTime();
            return localTime.ToString("dd/MM/yyyy hh:mm tt");
        }

        public static string ToDateTimeFormatDisplay(this DateTime dt)
        {
            DateTime localTime = dt.ToLocalTime();
            return localTime.ToString("dd/MM/yyyy hh:mm tt");
        }

        public static string ToDateFormatDisplay(this DateOnly? dt)
        {
            if (dt == null)
            {
                return "-";
            }
            return ((DateOnly)dt).ToString("dd/MM/yyyy");
        }

        public static string ToServerDateFormatString(this DateTime? dt)
        {
            return dt != null ? ((DateTime)dt).ToString("yyyy-MM-dd'T'HH:mm:ssZ") : null;
        }

        public static List<SelectListItem> AddNoYesValue(this List<SelectListItem> selectList)
        {
            selectList.Add(new SelectListItem
            {
                Text = "No",
                Value = false.ToString()
            });

            selectList.Add(new SelectListItem
            {
                Text = "Yes",
                Value = true.ToString()
            });

            return selectList;
        }

        public static string ToDisplay<T>(this T data)
        {
            if (data is null)
            {
                return "-";
            }

            if (data is string)
            {
                var _data = data as string;
                return String.IsNullOrEmpty(_data) ? "-" : _data;
            }
            else if (data is DateTime)
            {
                DateTime? _data = data as DateTime?;
                return _data.ToDateFormatDisplay();
            }
            else if (data is DateOnly)
            {
                DateOnly? _data = data as DateOnly?;
                return _data.ToDateFormatDisplay();
            }
            else if (data is bool)
            {
                bool _data = (bool)(data as bool?);
                return _data == true ? "Yes" : "No";
            }
            else if (data is string[])
            {
                var arrayString = data as string[];

                var joinedString = string.Empty;

                if (arrayString.Count() > 1)
                {
                    joinedString = string.Join(", ", arrayString);
                }
                else if (arrayString.Count() == 1)
                {
                    joinedString = arrayString[0];
                }

                return joinedString;
            }
            else if (data is List<string>)
            {
                var arrayString = data as List<string>;

                var joinedString = string.Empty;

                if (arrayString.Count() > 1)
                {
                    joinedString = string.Join(", ", arrayString);
                }
                else if (arrayString.Count() == 1)
                {
                    joinedString = arrayString[0];
                }

                return joinedString;
            }
            return data.ToString();
        }

        public static string ToDisplayDecimalToHour(this decimal data)
        {
            var decimalHour = Math.Round((data * 24) / 0.5m) * 0.5m;

            return CommonToNumberWithSeparatorsConvertor(decimalHour);
        }

        public static decimal ToDecimalToHour(this decimal data)
        {
            var decimalHour = 0.0m;

            if (data > 0)
            {
                decimalHour = Math.Round((data * 24) / 0.5m) * 0.5m;
            }

            return decimalHour;
        }

        public static decimal ToDecimalToHour(this decimal? data)
        {
            var decimalHour = 0.0m;

            if (data.HasValue && data > 0)
            {
                decimalHour = Math.Round(((decimal)data * 24) / 0.5m) * 0.5m;
            }

            return decimalHour;
        }

        public static string ToDisplayDecimalToHour(this decimal? data)
        {
            if (!data.HasValue)
            {
                return (0.0m).ToString().ToDisplay();
            }

            try
            {
                var decimalHour = Math.Round(((decimal)data * 24) / 0.5m) * 0.5m;
                return CommonToNumberWithSeparatorsConvertor(decimalHour);
            }
            catch (Exception ex)
            {
                return (0.0m).ToString().ToDisplay();
            }
        }

        public static bool IsValidEmail(this string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static List<SelectListItem> ToDropdownList(this List<EnumDropDownListModel> enumList)
        {
            var dropdownList = new List<SelectListItem>();
            foreach (var data in enumList)
            {
                dropdownList.Add(new SelectListItem
                {
                    Text = data.Name,
                    Value = data.Id.ToString()
                });
            }

            return dropdownList;
        }

        public static Dictionary<string, string> ObjectToDictionary(this object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var dictionary = new Dictionary<string, string>();
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                string stringValue = value?.ToString() ?? string.Empty;
                dictionary.Add(property.Name, stringValue);
            }

            return dictionary;
        }

        public static string ToNumberWithSeparators(this decimal data)
        {
            return CommonToNumberWithSeparatorsConvertor(data);
        }

        public static string ToNumberWithSeparators(this decimal? data)
        {
            if (!data.HasValue)
            {
                return string.Empty.ToDisplay();
            }

            return CommonToNumberWithSeparatorsConvertor((decimal)data);
        }

        public static string ToNumberWithSeparators(this float data)
        {
            var decimalRepresentation = (decimal)data;
            return CommonToNumberWithSeparatorsConvertor(decimalRepresentation);
        }

        public static string ToNumberWithSeparators(this float? data)
        {
            if (!data.HasValue)
            {
                return string.Empty.ToDisplay();
            }

            try
            {
                var decimalRepresentation = (decimal)data.Value;
                return CommonToNumberWithSeparatorsConvertor(decimalRepresentation);
            }
            catch (Exception ex)
            {
                return data.ToString();
            }
        }

        public static string ToNumberWithSeparators(this double data)
        {
            var decimalRepresentation = (decimal)data;
            return CommonToNumberWithSeparatorsConvertor(decimalRepresentation);
        }

        public static string ToNumberWithSeparators(this double? data)
        {
            if (!data.HasValue)
            {
                return string.Empty.ToDisplay();
            }

            try
            {
                var decimalRepresentation = (decimal)data.Value;
                return CommonToNumberWithSeparatorsConvertor(decimalRepresentation);
            }
            catch (Exception ex)
            {
                return data.ToString();
            }
        }

        private static string CommonToNumberWithSeparatorsConvertor(this decimal data)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            string formattedNumberCurrent = data.ToString("N2", currentCulture);

            return formattedNumberCurrent;
        }
    }
}
