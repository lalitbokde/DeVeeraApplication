using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class LanguageModel : BaseEntityModel
    {

        public LanguageModel()
        {
            AvailableLanguages = new List<SelectListItem>();
        }

        [Required]
        public string LanguageName { get; set; }
        [Required]
        public string Abbreviations { get; set; }
        public int LanguageId { get; set; }
        public IList<SelectListItem> AvailableLanguages { get; set; }

    }
}
