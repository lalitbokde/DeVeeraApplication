using DeVeeraApp.ViewModels.User;
using System.Collections.Generic;
using System.ComponentModel;

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

