using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IVideoServices
    {
       
        void DeleteVideo(Video model);

       
        IList<Video> GetAllVideos();


       
        Video GetVideoById(int videoId);

       
        IList<Video> GetVideoByIds(int[] VideoIds);

       
        void InsertVideo(Video model);

       
        void UpdateVideo(Video model);


    }
}
