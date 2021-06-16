using CRM.Core.Domain;
using CRM.Core.Domain.VideoModules;
using CRM.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IDiaryMasterService
    {
        void DeleteDiary(Diary model);


        IList<Diary> GetAllDiarys();



        Diary GetDiaryById(int DiaryId);


        IList<Diary> GetDiaryByIds(int[] DiaryIds);


        void InsertDiary(Diary model);


        void UpdateDiary(Diary model);

        List<DiaryViewModel> GetAllDiaries(
             int page_size = 0,
             int page_num = 0,
             bool GetAll = false,
             string SortBy = "",
             string SearchByDate = "",
              int UserId = 0
              
           );

    }
}
