using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class HappynessLevelModel
    {
       
            [Required(ErrorMessage = "Please rate your happyness level")]
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

    public enum HappynessLevelType
    {
        LevelOne = 1,
        LevelTwo = 2,
        LevelThree = 3,
        LevelFour = 4,
        LevelFive = 5,
        LevelSix = 6,
        LevelSeven = 7,
        LevelEight = 8,
        LevelNine = 9,
        LevelTen = 10
    }
}
