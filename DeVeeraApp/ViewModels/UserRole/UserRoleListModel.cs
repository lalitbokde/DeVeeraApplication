using DeVeeraApp.ViewModels.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.UserRole
{
    
        public partial class UserRoleListModel : BaseEntityModel
        {
            public UserRoleListModel()
            {

            ListUserRole = new List<UserRoleModel>();
             }
           
            [DisplayName("SearchName")]
            public string SearchName { get; set; }
          public UserRoleModel UserRoleData { get; set; }
          public IList<UserRoleModel> ListUserRole { get; set; }
       
        }
    }

