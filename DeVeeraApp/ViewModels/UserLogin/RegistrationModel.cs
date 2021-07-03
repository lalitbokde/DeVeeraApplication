using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class RegistrationModel : BaseEntityModel
    {
        public RegistrationModel()
        {
            this.AvailableTimeZones = new List<SelectListItem>();
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
           
        }

        //MVC is suppressing further validation if the IFormCollection is passed to a controller method. That's why we add to the model
        public IFormCollection Form { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        public bool EnteringEmailTwice { get; set; }
        [DataType(DataType.EmailAddress)]
        [DisplayName("ConfirmEmail")]
        public string ConfirmEmail { get; set; }

        public bool UsernamesEnabled { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
       
        [DisplayName("Account.Fields.Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
       
        [DisplayName("ConfirmPassword")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("FirstName")]
        public string FirstName { get; set; }
        [DisplayName("LastName")]
        public string LastName { get; set; }

        [DisplayName("DateOfBirth")]
        public int? DateOfBirthDay { get; set; }
        [DisplayName("DateOfBirth")]
        public int? DateOfBirthMonth { get; set; }
        [DisplayName("DateOfBirth")]
        public int? DateOfBirthYear { get; set; }
        public bool DateOfBirthRequired { get; set; }
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

        [DisplayName("Company")]
        public string Company { get; set; }


        [DisplayName("StreetAddress")]
        public string StreetAddress { get; set; }


        [DisplayName("StreetAddress2")]
        public string StreetAddress2 { get; set; }

        [DisplayName("ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("Country")]
        public int CountryId { get; set; }
        public IList<SelectListItem> AvailableCountries { get; set; }


        [DisplayName("StateProvince")]
        public int StateProvinceId { get; set; }
        public IList<SelectListItem> AvailableStates { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone")]
        public string Phone { get; set; }


        //time zone
        [DisplayName("TimeZone")]
        public string TimeZoneId { get; set; }

        public IList<SelectListItem> AvailableTimeZones { get; set; }


        
    }

}
