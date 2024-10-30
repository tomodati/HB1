using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingApp.Services.Helpers
{
    public static class GeneralHelper
    {

        public static DateTime ConvertToDateTime(this string input)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(input, "yyyyMMdd", provider);
        }

        public static string[] SplitValue(this string val)
        {
            // here we should check if indexes are below 0 and raise exceptions
            int startIndex = val.IndexOf("(");
            int endIndex = val.IndexOf(")");
            string commandText = val.Substring(startIndex + 1, endIndex - startIndex - 1);
            return commandText.Split(new char[] { ',' });
        }


    }
}
