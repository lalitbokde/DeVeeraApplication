using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.User
{
    public class TwoFactorAuthModel
    {
        public string OTP { get; set; }
        public int LevelId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
    }
}
