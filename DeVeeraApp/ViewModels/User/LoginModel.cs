using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.User
{
    public partial class LoginModel : BaseEntityModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}
