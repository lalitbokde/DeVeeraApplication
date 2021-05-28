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
        public string Name { get; set; }

        public string LanguageCulture { get; set; }

        public string UniqueSeoCode { get; set; }
        public string FlagImageFileName { get; set; }

        public bool Published { get; set; }

        public string ReturnUrl { get; set; }
        public bool Rtl { get; set; }

        public int DisplayOrder { get; set; }
        public int LanguageId { get; set; }
        public IList<SelectListItem> AvailableLanguages { get; set; }

    }
}
