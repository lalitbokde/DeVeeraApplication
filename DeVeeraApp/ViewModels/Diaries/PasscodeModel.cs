using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class PasscodeModel:BaseEntityModel
    {
        public int UserId { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
