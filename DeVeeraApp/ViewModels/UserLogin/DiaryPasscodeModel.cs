using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public class DiaryPasscodeModel:BaseEntityModel
    {
        public int UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

    }
}
