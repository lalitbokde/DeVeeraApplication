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

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }

        public bool DisplayCaptcha { get; set; }

        public string BannerImageUrl { get; set; }

    }
}
