using CRM.Core.Domain;
using CRM.Core.ViewModels;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface ILevelServices
    {
       
        void DeleteLevel(Level model);

       
        IList<Level> GetAllLevels();


       
        Level GetLevelById(int videoId);

        Level GetLevelByLevelNo(int LevelNo);



        IList<Level> GetLevelByIds(int[] VideoIds);

       
        void InsertLevel(Level model);

       
        void UpdateLevel(Level model);
        Level GetFirstRecord();

        List<LevelViewModel> GetAllLevelsDataSp(
           int page_size = 0,
           int page_num = 0,
           bool GetAll = false,
           string SortBy = "",
            string Title = "",
             string Subtitle = "",
             string VideoName = "",
             int LikeId = 0,
             int DisLikeId = 0
         );
        public List<ModulesViewModel> GetAllModulesDataSp(int LevelId);

    }
}
