using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
