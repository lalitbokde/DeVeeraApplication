using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        }
        public int LevelId { get; set; }
        public int ModuleId { get; set; }
        public string Question { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public IList<SelectListItem> AvailableLevels { get; set; }
        public IList<SelectListItem> AvailableModules { get; set; }
    }
}
