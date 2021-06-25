using CRM.Core.Domain;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface IVideoMasterService
    {
        void DeleteVideo(Video model);


        IList<Video> GetAllVideos();



        Video GetVideoById(int videoId);


        IList<Video> GetVideoByIds(int[] VideoIds);


        void InsertVideo(Video model);


        void UpdateVideo(Video model);

    }
}
