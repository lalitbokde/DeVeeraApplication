using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class VideoServices : IVideoServices
    {
        private readonly IRepository<Video> _videoRepository;

        public VideoServices(IRepository<Video> addressRepository)
        {
            _videoRepository = addressRepository;
        }

        public void DeleteVideo(Video model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _videoRepository.Delete(model);
        }

        public IList<Video> GetAllVideos()
        {
            var query = from vdo in _videoRepository.Table
                        orderby vdo.Title
                        select vdo;
            var warehouses = query.ToList();
            return warehouses;
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
    }
}
