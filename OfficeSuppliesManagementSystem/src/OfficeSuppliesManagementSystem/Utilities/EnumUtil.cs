using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using System.Reflection;

namespace OfficeSuppliesManagementSystem.Utilities
{
    public static class EnumUtil
    {
        public static string GetDisplayName(this Enum value, bool nameInstead = true)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            var field = type.GetField(name);
            var attribute = field.GetCustomAttribute<DisplayAttribute>();

            if (attribute == null && nameInstead)
            {
                return name;
            }

            return attribute?.Name;
        }

        public static List<SelectListItem> GenerateSelectItems(this Type type, Enum selected = null)
        {
            if (!type.GetTypeInfo().IsEnum)
            {
                throw new ArgumentException("Type must be enum.", nameof(type));
            }

            return (from Enum value in Enum.GetValues(type)
                    select
                        new SelectListItem
                        {
                            Text = value.GetDisplayName(),
                            Value = Convert.ToInt32(value).ToString(),
                            Selected = Equals(value, selected)
                        })
                .ToList();
        }
    }
}
