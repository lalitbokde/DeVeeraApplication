using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IVideoServices
    {
       
        void DeleteVideo(Level model);

       
        IList<Level> GetAllVideos();


       
        Level GetVideoById(int videoId);

       
        IList<Level> GetVideoByIds(int[] VideoIds);

       
        void InsertVideo(Level model);

       
        void UpdateVideo(Level model);


    }
}
