using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CRM.Core.Infrastructure
{
    public static class Extensions
    {
        public class EpplusIgnore : Attribute { }
        public static ExcelRangeBase LoadFromCollectionFiltered<T>(this ExcelRangeBase @this, List<T> collection) where T : class
        {
            MemberInfo[] membersToInclude = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !Attribute.IsDefined(p, typeof(EpplusIgnore)))
                .ToArray();

            return @this.LoadFromCollection<T>(collection, true,
                OfficeOpenXml.Table.TableStyles.Medium9,
                BindingFlags.Instance | BindingFlags.Public,
                membersToInclude);
        }

    }
}
