using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class LoginModel : BaseEntityModel
    {
        

        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }
        [DisplayName("UserName")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
       
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("RememberMe")]
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }
    }
}
