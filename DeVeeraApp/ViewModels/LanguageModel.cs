using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeVeeraApp.ViewModels
{
    public class LanguageModel : BaseEntityModel
    {

        public LanguageModel()
        {
            AvailableLanguages = new List<SelectListItem>();
        }

        //[StringLength(150)]
        //[Required(ErrorMessage ="Enter Name")]
        public string Name { get; set; }

        //[StringLength(200)]
        //[Required(ErrorMessage ="Enter language culture")]
        public string LanguageCulture { get; set; }

        //[StringLength(150)]
        //[Required(ErrorMessage = "Enter UniqueSeoCode")]
        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }

        public bool Published { get; set; }

        public string ReturnUrl { get; set; }
        public bool Rtl { get; set; }

        
        [Required(ErrorMessage ="Enter Display Order")]
        public int DisplayOrder { get; set; }

       
        public int LanguageId { get; set; }
        public IList<SelectListItem> AvailableLanguages { get; set; }

    }
}
