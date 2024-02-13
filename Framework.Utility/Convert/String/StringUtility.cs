using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Utility.Convert.String
{
    public static class StringUtility
    {
        public static string ToEnglishNumber(this string strNum)
        {
            char[] pn = { '۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹', '٫' };
            char[] en = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            var digits = pn.Zip(en, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);

            strNum = digits.Aggregate(strNum, (result, s) => result.Replace(s.Key, s.Value));
            return strNum;
        }
        public static string GetProperty(this object @object, string fieldName)
        {
            dynamic o = @object;
            var s = o[fieldName];
            return s ?? s.value;
        }

        public static bool IsNullOrEmptyOrWhiteSpace(string fieldName)
        {
            if (fieldName is null || fieldName == "")
                return true;
            return false;
        }
    }
}
