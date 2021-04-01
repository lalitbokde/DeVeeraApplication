using CRM.Core;
using CRM.Core.Domain.Common;
using CRM.Core.Domain.Directory;

using DeVeeraApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.Model.Common
{
    public class AddressModel : BaseEntityModel
    {

        public AddressModel()
        {
            AvailableUsers = new List<SelectListItem>();

            AvailableCountries = new List<SelectListItem>();
            AvailableStates = new List<SelectListItem>();
            
        }

        //[Required(AllowEmptyStrings = false)]
        [DisplayName("FirstName")]
        public string FirstName { get; set; }

  
        [DisplayName("MiddleName")]
        public string MiddleName { get; set; }
        //[Required(AllowEmptyStrings = false)]
        [DisplayName("LastName")]
        public string LastName { get; set; }
        //[Required(AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("CompanyName")]
        public string Companyname { get; set; }

        [DisplayName("DOB")]
        public DateTime DOB { get; set; }

        [DisplayName("Country")]
        public int? CountryId { get; set; }

        [DisplayName("Country")]
        public string CountryName { get; set; }

        [DisplayName("StateProvince")]
        public int? StateProvinceId { get; set; }

        [DisplayName("StateProvince")]
        public string StateProvinceName { get; set; }
     
        public StateProvince StateProvince { get; set; }


        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Address1")]
        public string Address1 { get; set; }

        [DisplayName("Address2")]
        public string Address2 { get; set; }

        [DisplayName("ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [DisplayName("FaxNumber")]
        public string FaxNumber { get; set; }

        //address in HTML format (usually used in grids)
        [DisplayName("Address")]
        public string AddressHtml { get; set; }

        //formatted custom address attributes
        public string FormattedCustomAddressAttributes { get; set; }


       public string AddressType { get; set; }


        public IList<SelectListItem> AvailableUsers { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }
      
    
    }

   

}
