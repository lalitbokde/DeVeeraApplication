using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Filters
{
    public enum DateFormats
    {
        DD_MM_YYYY,
        MM_DD_YYYY,
        YYYY_MM_DD
    }

    public static class DateFilter
    {
        public static string ConvertToFormat(string Date, DateFormats FromFormat, DateFormats ToFormat = DateFormats.YYYY_MM_DD)
        {
            string Day = "";
            string Month = "";
            string Year = "";
            string NewDate = "";
            
            switch (FromFormat)
            {
                case DateFormats.DD_MM_YYYY:

                    Date = Date.Substring(0, 10);
                    string[] FullDate = Date.Split('/');
                    Day = FullDate[0].ToString();
                    Month = FullDate[1].ToString();
                    Year = FullDate[2].ToString();
                    break;

                case DateFormats.MM_DD_YYYY:

                    Date = Date.Substring(0, 10);
                    string[] FullDateCase2 = Date.Split('/');
                    Day = FullDateCase2[1].ToString();
                    Month = FullDateCase2[0].ToString();
                    Year = FullDateCase2[2].ToString();
                    break;

                case DateFormats.YYYY_MM_DD:

                    Date = Date.Substring(0, 10);
                    string[] FullDateCase3 = Date.Split('-');
                    Day = FullDateCase3[2].ToString();
                    Month = FullDateCase3[1].ToString();
                    Year = FullDateCase3[0].ToString();
                    break;

            }
        
            switch (ToFormat)
            {
                case DateFormats.YYYY_MM_DD:
                    NewDate = Year + "-" + Month + "-" + Day; 
                    break;
                case DateFormats.MM_DD_YYYY:
                    NewDate = Month + "/" + Day + "/" + Year; 
                    break;
                case DateFormats.DD_MM_YYYY:
                    NewDate = Day + "/" + Month + "/" + Year; 
                    break;
            }

            return NewDate;

        }
    }
}
