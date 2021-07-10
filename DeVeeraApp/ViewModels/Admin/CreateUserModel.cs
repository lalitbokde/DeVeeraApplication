using CRM.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Admin
{
    public class CreateUserModel:BaseEntityModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter valid username")]
        public string Email { get; set; }
        [Required]
        public UserPassword UserPassword { get; set; }
        public bool IsAllow { get; set; }
        public int UserRoleId { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNumber { get; set; }
       public CRM.Core.Domain.Users.UserRole UserRole { get; set; }
    }
}
