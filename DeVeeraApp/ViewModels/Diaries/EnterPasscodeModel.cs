using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class EnterPasscodeModel:BaseEntityModel
    {
        
        public string Passcode { get; set; }
        public string MobileNumber { get; set; }
    }
}
