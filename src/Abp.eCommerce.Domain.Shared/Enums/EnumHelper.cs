using Abp.eCommerce.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.eCommerce.Enums
{
    public class EnumDropDownListModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public static class EnumHelper
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = Enum.GetValues(type);

                foreach (int val in values)
                {
                    if (val == e.ToInt32(CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttribute = memInfo[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() as DescriptionAttribute;

                        if (descriptionAttribute != null)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }

            return null; // could also return string.Empty
        }

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }

        public static List<EnumDropDownListModel> ToSelectionList<T>(IStringLocalizer<eCommerceResource> localizer, bool isLocalized = true) where T : struct, Enum
        {
            var array = Enum.GetValues(typeof(T)).Cast<T>();
            return array.Select(a => new EnumDropDownListModel
            {
                Name = isLocalized ? a.GetDescription().LocalizedEnum(localizer) : a.GetDescription(),
                Id = Convert.ToInt32(a)
            })
            .OrderBy(x => x.Id)
            .ToList();
        }

        public static string LocalizedEnum(this string desc, IStringLocalizer<eCommerceResource> localizer)
        {
            return localizer[desc];
        }

        public static List<SelectListItem> AddDefaultValue(this List<SelectListItem> selectList, IStringLocalizer<eCommerceResource> localizer, string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                selectList.Insert(0, new SelectListItem
                {
                    Text = localizer.GetString("PleaseSelect"),
                    Value = ""
                });
            }
            else
            {
                selectList.Insert(0, new SelectListItem
                {
                    Text = localizer.GetString("PleaseSelectItem", name),
                    Value = ""
                });
            }


            return selectList;
        }
    }
}
