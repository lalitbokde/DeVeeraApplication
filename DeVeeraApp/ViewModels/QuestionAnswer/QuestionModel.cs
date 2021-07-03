using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels.QuestionAnswer
{
    public class QuestionModel:BaseEntityModel
    {
        public QuestionModel()
        {
            AvailableLevels = new List<SelectListItem>();
            AvailableModules = new List<SelectListItem>();
            QuestionsList = new List<QuestionModel>();
        }

        [Range(1, int.MaxValue, ErrorMessage = "Please select the level")]
        [Required]
        public int LevelId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select the module")]
        [Required]
        public int ModuleId { get; set; }
        [Required(ErrorMessage ="Enter the question")]
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public ModulesModel Module { get; set; }
        public IList<SelectListItem> AvailableLevels { get; set; }
        public IList<SelectListItem> AvailableModules { get; set; }
        public List<QuestionModel> QuestionsList { get; set; }
      
        [NotMapped]
        public string Questionarrie { get; set; }
    }
}
