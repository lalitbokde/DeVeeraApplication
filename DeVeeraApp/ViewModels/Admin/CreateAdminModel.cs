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


    }
}
