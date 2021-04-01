using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Common
{
    public class CurrencyModel : BaseEntityModel
    {
        public string CountryName { get; set; }

        public string CurrencyName { get; set; }
        public string CurrencyAbbrevation { get; set; }
    }
}
