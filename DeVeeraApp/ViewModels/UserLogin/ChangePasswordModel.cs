using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class ChangePasswordModel : BaseEntityModel
    {
       
        [DataType(DataType.Password)]
        [DisplayName("OldPassword")]
        public string OldPassword { get; set; }

       
        [DataType(DataType.Password)]
        [DisplayName("NewPassword")]
        public string NewPassword { get; set; }

       
        [DataType(DataType.Password)]
        [DisplayName("ConfirmNewPassword")]
        public string ConfirmNewPassword { get; set; }

        public string Result { get; set; }
    }
}
