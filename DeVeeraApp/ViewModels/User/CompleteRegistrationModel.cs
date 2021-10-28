using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels.User
{
    public class CompleteRegistrationModel
    {


        [Required(ErrorMessage = "Please select the gender !!")]
        public Gender GenderType { get; set; }
        [Required]
        public int Age { get; set; }
        [Required(ErrorMessage ="Please enter the Occupation!!")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "Please select the Education type !!")]
        public Education EducationType { get; set; }
        [Required(ErrorMessage = "Please select Income !!")]
        public Income IncomeAboveOrBelow80K { get; set; }
        [Required(ErrorMessage = "Please select the Family type !!")]
        public FamilyOrRelationship FamilyOrRelationshipType { get; set; }
        public string Reason { get; set; }

        public string HeaderImageUrl { get; set; }
        public int UserId { get; set; }
        public int LevelNo { get; set; }
        public int SrNo { get; set; }

    }
}
