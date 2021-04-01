using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.User
{
    public class UserRoleModel : BaseEntityModel
    {

        /// <summary>
        /// Gets or sets the User role name
        /// 
        /// </summary>
         [Required]
        public string Name { get; set; }

      

        /// <summary>
        /// Gets or sets a value indicating whether the User role is active
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the User role is system
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// Gets or sets the User role system name
        /// </summary>
        public string SystemName { get; set; }


       

    }
}
