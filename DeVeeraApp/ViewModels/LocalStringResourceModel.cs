using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class LocalStringResourceModel : BaseEntityModel
    {

        public LocalStringResourceModel()
        {
            Language = new LanguageModel();
        }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public string ResourceName { get; set; }

        [Required]
        public string ResourceValue { get; set; }

        /// <summary>
        /// Gets or sets the language
        /// </summary>
        public virtual LanguageModel Language { get; set; }
    }
}
