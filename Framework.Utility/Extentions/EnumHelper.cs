using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Extentions
{
    public static class EnumHelper
    {

        public static string? GetDisplayName(this Enum? enumValue)
        {
            if (enumValue == null)
                return null;

            var displayAttribute = enumValue.GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault(m => m.GetCustomAttributes(typeof(DisplayAttribute), false).Any())
                ?.GetCustomAttribute<DisplayAttribute>();

            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
            else
            {
                return enumValue.ToString();
            }
        }
    }

    public static class Enum<T> where T : Enum
    {
        public static IEnumerable<T> GetAllValuesAsIEnumerable()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
