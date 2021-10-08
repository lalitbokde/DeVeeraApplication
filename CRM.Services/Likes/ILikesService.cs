using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Likes
{
    public interface ILikesService
    {

        IList<LikesUnlikess> GetAllLikes();
        List<LikesUnlikess> GetAllLikesModules(); 
        void InsertLikes(LikesUnlikess model);


        void UpdateLikes(LikesUnlikess model);
        public LikesUnlikess GetLikesByUserId(int UserId);
        public IList<LikesUnlikess> GetLikesByLevelId(int LevelId);
        public IList<LikesUnlikess> GetCommenntsByLevelId(int LevelId);
        public IList<LikesUnlikess> GetCommenntsByModuleId(int ModuleId);
        public IList<LikesUnlikess> GetDislikesByLevelId(int LevelId);
        public IList<LikesUnlikess> GetLikesByModuleId(int ModuleId);
        public IList<LikesUnlikess> GetDisLikesByModuleId(int ModuleId);
        public LikesUnlikess GetLikesByLevelIdandUserId(int LevelId , int UserId);
        public LikesUnlikess GetLikesByModuleIdandUserId(int ModuleId, int UserId);


        public List<LikesUnlikess> GetLikesByLevelIdandUserIdlist(int LevelId, int UserId); 


    }
}
