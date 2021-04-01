using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
