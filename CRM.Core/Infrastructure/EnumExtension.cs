using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CRM.Core.Infrastructure
{
    public static class EnumDescription
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

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

        //public static  string GetSeries(Series SeriesId)
        //{

        //    var Seriestype = typeof(Series);
        //    var Seriesmember = Seriestype.GetMember(SeriesId.ToString());
        //    var Seriesattributes = Seriesmember[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        //  return ((DescriptionAttribute)Seriesattributes[0]).Description;
        //}
    }
}
