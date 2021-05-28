using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class EnterPasscodeModel:BaseEntityModel
    {
        [Required]
        public string Passcode { get; set; }
    }
}
