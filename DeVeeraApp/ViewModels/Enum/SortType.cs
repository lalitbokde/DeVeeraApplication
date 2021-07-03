using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.Enum
{
    public enum SortType
    {
        [Display(Name = "Default")]
        Default = 1,
        [Display(Name = "Created Date")]
        Created_Latest = 2,
        [Display(Name = "Update Date")]
        Updated_Latest = 3,
        [Display(Name = "Title")]
        Title_AtoZ = 4,

    }
}
