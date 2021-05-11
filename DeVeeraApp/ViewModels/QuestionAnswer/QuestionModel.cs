using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public int LevelId { get; set; }
        public int ModuleId { get; set; }
        [Required]
        public string Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public ModulesModel Module { get; set; }
        public IList<SelectListItem> AvailableLevels { get; set; }
        public IList<SelectListItem> AvailableModules { get; set; }
        public List<QuestionModel> QuestionsList { get; set; }
    }
}
