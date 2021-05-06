using System;
using System.Collections.Generic;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class DiaryModel : BaseEntityModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? LevelId { get; set; }
        public int? ModuleId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Deleted { get; set; }
        public string Level { get; set; }

        public string Module { get; set; }

        public List<DiaryModel> diaryModels { get; set; }
    }
}
