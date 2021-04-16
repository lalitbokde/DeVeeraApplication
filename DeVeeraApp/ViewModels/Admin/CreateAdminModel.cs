using CRM.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace DeVeeraApp.ViewModels.Admin
{
    public class CreateAdminModel : BaseEntityModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public UserPassword UserPassword { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public int UserRoleId { get; set; }
        public Gender GenderType { get; set; }
       
    }
}
