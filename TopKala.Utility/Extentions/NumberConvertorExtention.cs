using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace TopKala.Utility.Extentions
{
    public static class NumberConvertorExtention
    {
        public static string NumberConvertor(this object obj, CultureInfo cultureInfo)
        {
            if (obj == null) return "";

            var str = obj.ToString();
            for (int i = 0; i <= 9; i++)
            {
                str = str.Replace(i.ToString(), cultureInfo.NumberFormat.NativeDigits[i]);
            }

            return str;
        }
    }
}