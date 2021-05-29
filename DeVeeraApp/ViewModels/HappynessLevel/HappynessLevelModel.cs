using DeVeeraApp.ViewModels.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.HappynessLevel
{
    public class HappynessLevelModel : BaseEntityModel
    {
        [Required(ErrorMessage = "Please select happyness level.")]
        public int HappynessLevelTypeId { get; set; }

        public HappynessLevelType HappynessLevelType
        {
            get
            {
                return (HappynessLevelType)HappynessLevelTypeId;
            }
            set
            {
                HappynessLevelTypeId = (int)value;
            }
        }

    }
}
