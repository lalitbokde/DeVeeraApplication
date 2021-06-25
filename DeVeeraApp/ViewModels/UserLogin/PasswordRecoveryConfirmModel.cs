using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class PasswordRecoveryConfirmModel : BaseEntityModel
    {
        [DataType(DataType.Password)]
       
        [DisplayName("Account.PasswordRecovery.NewPassword")]
        public string NewPassword { get; set; }

       
        [DataType(DataType.Password)]
        [DisplayName("Account.PasswordRecovery.ConfirmNewPassword")]
        public string ConfirmNewPassword { get; set; }

        public bool DisablePasswordChanging { get; set; }
        public string Result { get; set; }
    }
}
