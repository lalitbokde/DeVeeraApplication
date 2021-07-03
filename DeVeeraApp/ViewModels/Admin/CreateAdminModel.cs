using CRM.Core.Domain.Users;
using System.ComponentModel.DataAnnotations;


namespace DeVeeraApp.ViewModels.Admin
{
    public class CreateAdminModel : BaseEntityModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Please enter valid username")]
        public string Email { get; set; }
        [Required]
        public UserPassword UserPassword { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public int UserRoleId { get; set; }
        public Gender GenderType { get; set; }

        //public CRM.Core.Domain.Users.UserRole UserRole { get; set; }

}
}
