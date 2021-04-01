using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class UserInfoModel : BaseEntityModel
    {
        public UserInfoModel()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
          
        }

        //MVC is suppressing further validation if the IFormCollection is passed to a controller method. That's why we add to the model
        public IFormCollection Form { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Account.Fields.Email")]
        public string Email { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("Account.Fields.EmailToRevalidate")]
        public string EmailToRevalidate { get; set; }

    
        public string Username { get; set; }

    
        public string Gender { get; set; }

        [DisplayName("Account.Fields.FirstName")]
        public string FirstName { get; set; }
        [DisplayName("Account.Fields.LastName")]
        public string LastName { get; set; }


        public int? DateOfBirthDay { get; set; }
        [DisplayName("Account.Fields.DateOfBirth")]
        public int? DateOfBirthMonth { get; set; }
        [DisplayName("Account.Fields.DateOfBirth")]
        public int? DateOfBirthYear { get; set; }
      
        public DateTime? ParseDateOfBirth()
        {
            if (!DateOfBirthYear.HasValue || !DateOfBirthMonth.HasValue || !DateOfBirthDay.HasValue)
                return null;

            DateTime? dateOfBirth = null;
            try
            {
                dateOfBirth = new DateTime(DateOfBirthYear.Value, DateOfBirthMonth.Value, DateOfBirthDay.Value);
            }
            catch { }
            return dateOfBirth;
        }

        [DisplayName("Account.Fields.StreetAddress")]
        public string StreetAddress { get; set; }

        [DisplayName("Account.Fields.StreetAddress2")]
        public string StreetAddress2 { get; set; }


        [DisplayName("Account.Fields.ZipPostalCode")]
        public string ZipPostalCode { get; set; }


        [DisplayName("Account.Fields.City")]
        public string City { get; set; }


        [DisplayName("Account.Fields.Country")]
        public int CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }


        [DisplayName("Account.Fields.StateProvince")]
        public int StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Account.Fields.Phone")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]




        //time zone
        [DisplayName("Account.Fields.TimeZone")]
        public string TimeZoneId { get; set; }

        public IList<SelectListItem> AvailableTimeZones { get; set; }




    }
}
