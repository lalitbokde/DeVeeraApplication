using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class SettingModel : BaseEntityModel
    {
        public int? UserId { get; set; }
        public int LanguageId { get; set; }
        public int? ProjectId { get; set; }

    }
}
