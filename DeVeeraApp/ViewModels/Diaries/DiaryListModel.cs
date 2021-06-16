using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using DeVeeraApp.ViewModels.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeVeeraApp.ViewModels.Diaries
{
    public class DiaryListModel : CommonPageModel
    {
        public DiaryListModel()
        {
            Diary = new DiaryModel();
        }
        public int SortTypeId { get; set; }
        public string SearchByDate { get; set; }

        [NotMapped]
        public SortType SortType
        {
            get
            {
                return (SortType)SortTypeId;
            }
            set
            {
                SortTypeId = (int)value;
            }
        }
        public DiaryModel Diary { get; set; }

        public PagedResult<DiaryViewModel>DiaryList { get; set; }
    }
}
