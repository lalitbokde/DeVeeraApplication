using CRM.Core.Domain;
using CRM.Core.ViewModels;
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

        List<VideoViewModel> GetAllVideoSp(
            int page_size = 0,
            int page_num = 0,
            bool GetAll = false,
            string SortBy = ""
          );


    }
}
