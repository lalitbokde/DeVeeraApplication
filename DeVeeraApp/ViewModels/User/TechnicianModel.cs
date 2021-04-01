using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.User
{
    public class TechnicianModel
    {
        public TechnicianModel()
        {
            UserModel = new UserModel();
        }
        public UserModel UserModel { get; set; }
        public string ActiveTab { get; set; }

        public int Accepted  { get; set; }
        public int Pending { get; set; }
        public int Declined { get; set; }
        public int Cancelled { get; set; }

        public int Completed { get; set; }

        public DateTime BlockoutDay { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }
        public string SeverUrl { get; set; }
        public string ImageUrl { get; set; }
        public List<string> BlockoutDays { get; set; }

        //public UserModel UserModel { get; set; }
    }
}
