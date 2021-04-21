using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace CRM.Services
{
    public class VideoMasterService : IVideoMasterService
    {
        #region fields
        private readonly IRepository<Video> _videoRepository;
        #endregion


        #region ctor
        public VideoMasterService(IRepository<Video> videoRepository)
        {
            _videoRepository = videoRepository;
        }

        #endregion

        #region Method
        public void DeleteVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _videoRepository.Delete(model);
        }

        public IList<Video> GetAllVideos()
        {
            var query = from vdo in _videoRepository.Table
                        orderby vdo.VideoUrl
                        select vdo;
            var videos = query.ToList();
            return videos;
        }

        public virtual Video GetVideoById(int videoId)
        {
            if (videoId == 0)
                return null;


            return _videoRepository.GetById(videoId);
        }

        public IList<Video> GetVideoByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<Video>();

            var query = from c in _videoRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<Video>();
            foreach (var id in VideoIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _videoRepository.Insert(model);
        }

        public void UpdateVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _videoRepository.Update(model);
        }

        #endregion

    }
}
