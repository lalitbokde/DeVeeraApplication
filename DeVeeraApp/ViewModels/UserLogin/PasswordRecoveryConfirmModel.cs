using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
