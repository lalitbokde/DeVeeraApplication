using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Likes
{
    public interface ILikesService
    {

        IList<LikesUnlikess> GetAllLikes();
        void InsertLikes(LikesUnlikess model);


        void UpdateLikes(LikesUnlikess model);
        public LikesUnlikess GetLikesByUserId(int UserId);
       
    }
}
