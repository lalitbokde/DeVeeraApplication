using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.User
{
    public partial class UserListModel : BaseEntityModel
    {

        public UserListModel()
        {
            AvailableName = new List<SelectListItem>();
            AvailableEmail = new List<SelectListItem>();
            AvailableActive = new List<SelectListItem>();
            ListUserModel = new List<UserViewModel>();
            UserData = new UserModel();
        }
        [DisplayName("SearchName")]
        public string SearchName { get; set; }
        public IList<SelectListItem> AvailableName { get; set; }
        public IList<SelectListItem> AvailableEmail { get; set; }
        public IList<SelectListItem> AvailableActive { get; set; }
        public IList<UserViewModel> ListUserModel { get; set; }
        public UserModel UserData { get; set; }

    }

    public partial class UserViewModel : BaseEntityModel
    {
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [DisplayName("Company Name")]
        public string CompanyName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Use valid emailId")]
        public string Email { get; set; }

        [DisplayName("Status")]
        public bool Status { get; set; }
        public string UserRoleName { get; set; }
        public int pickupId { get; set; }

       
    }

}
