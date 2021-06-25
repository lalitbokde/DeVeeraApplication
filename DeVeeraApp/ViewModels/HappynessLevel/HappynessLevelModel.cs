using System.ComponentModel.DataAnnotations;

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
