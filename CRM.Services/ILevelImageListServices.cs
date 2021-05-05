using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface ILevelImageListServices
    {
        void DeleteLevelImage(LevelImageList model);

        void DeleteLevelImagesByLevelId(int levelId);

        IList<LevelImageList> GetAllLevelImage();



        LevelImageList GetLevelImageById(int Id);



        void InsertLevelImage(LevelImageList model);


        void UpdateLevelImage(LevelImageList model);

        List<LevelImageList> GetLevelImageListByLevelId(int id);

    }
}
