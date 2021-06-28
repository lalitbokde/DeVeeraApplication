using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.UserLogin
{
    public partial class PasswordRecoveryModel : BaseEntityModel
    {
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        public string Result { get; set; }
    }
}
