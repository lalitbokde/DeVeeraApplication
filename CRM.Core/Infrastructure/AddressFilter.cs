using CRM.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CRM.Core.Infrastructure
{
    public static class AddressFilter
    {
        public static string GetAddressString(Address address)
        {
            string addressString = (address != null) ? (address.FirstName + " " + address.LastName
                 + ((!string.IsNullOrEmpty(address.Address1)) ? ",\n" + address.Address1 : "")
                 + ((!string.IsNullOrEmpty(address.Address2)) ? ", " + address.Address2 : "")
                 + ((!string.IsNullOrEmpty(address.City)) ? ",\n" + address.City : "")
                   + ((address.StateProvince != null) ? (((!string.IsNullOrEmpty(address.StateProvince.Abbreviation)) ? ",\n" + address.StateProvince.Abbreviation : "")) : "")
                  + ((!string.IsNullOrEmpty(address.ZipPostalCode)) ? ", " + address.ZipPostalCode : "")
                    + ((address.Country != null) ? (((!string.IsNullOrEmpty(address.Country.Name)) ? ",\n" + address.Country.Name : "")) : "")
                     + ((!string.IsNullOrEmpty(address.PhoneNumber)) ? "\n" + address.PhoneNumber : "")
                       + ((!string.IsNullOrEmpty(address.Email)) ? "\n" + address.Email : "")) : "";
            return addressString;
        }

        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr =
                           Attribute.GetCustomAttribute(field,
                             typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
