using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels.User
{
    public partial class LoginModel : BaseEntityModel
    {    
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Enter valid email address")]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }

        public string Username { get; set; }

       
        [Required(ErrorMessage = "Please enter Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,25}$",
        ErrorMessage = "The password length must be minimum 8 characters.The password must contain one or more special characters,uppercase characters,lowercase characters,numeric values..!!")]
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }

        public string BannerImageUrl { get; set; }
        [NotMapped]
        public string ErrorMessage { get; set; }


    }
}
