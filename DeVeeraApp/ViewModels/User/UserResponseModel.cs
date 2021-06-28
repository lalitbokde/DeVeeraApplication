using System;

namespace DeVeeraApp.ViewModels.User
{
    public class UserResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public DateTime DOB { get; set; }
        public string ContactNo { get; set; }
        public int PasswordId { get; set; }
        public string Email { get; set; }
        public string Alias { get; set; }
        public int CustomerRoleId { get; set; }
        public bool Active { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
    }
}
