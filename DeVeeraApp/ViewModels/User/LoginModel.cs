using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels.User
{
    public partial class LoginModel : BaseEntityModel
    {    
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Enter valid email address")]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        public bool UsernamesEnabled { get; set; }

        public string Username { get; set; }

       
        [Required(ErrorMessage = "Please enter Password")]
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
