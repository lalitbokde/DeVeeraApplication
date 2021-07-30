using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class LocalStringResourceModel : BaseEntityModel
    {

        public LocalStringResourceModel()
        {
            this.Language = new LanguageModel();
        }

       
        //[Range(1, int.MaxValue, ErrorMessage = "Please select the language")]
        //[Required]
        public int? LanguageId { get; set; }

        [Required(ErrorMessage ="Enter Resourse name")]
        public string ResourceName { get; set; }

        [Required(ErrorMessage ="Enter Resourse value")]
        public string ResourceValue { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        /// 
       
        public virtual LanguageModel Language { get; set; }
    }
}
