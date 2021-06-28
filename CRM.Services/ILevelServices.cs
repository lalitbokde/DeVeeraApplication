using CRM.Core.Domain;
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



    }
}
