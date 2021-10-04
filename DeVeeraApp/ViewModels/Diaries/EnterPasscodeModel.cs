using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class EnterPasscodeModel:BaseEntityModel
    {
        [Required]
        public string Passcode { get; set; }
        public string MobileNumber { get; set; }
    }
}
