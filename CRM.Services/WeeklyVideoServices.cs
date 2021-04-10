using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace CRM.Services
{
    public class WeeklyVideoServices : IWeeklyVideoServices
    {
        #region fields
        private readonly IRepository<WeeklyVideo> _weeklyVideoRepository;

        #endregion

        #region ctor
        public WeeklyVideoServices(IRepository<WeeklyVideo> weeklyVideoRepository)
        {
            _weeklyVideoRepository = weeklyVideoRepository;
        }
        #endregion


        #region Methods
        public void DeleteWeeklyVideo(WeeklyVideo model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _weeklyVideoRepository.Delete(model);
        }

        public IList<WeeklyVideo> GetAllWeeklyVideos()
        {
            var query = from vdo in _weeklyVideoRepository.Table
                        orderby vdo.Title
                        select vdo;
            var warehouses = query.ToList();
            return warehouses;
        }

        public WeeklyVideo GetWeeklyVideoById(int videoId)
        {
            if (videoId == 0)
                return null;


            return _weeklyVideoRepository.GetById(videoId);
        }

        public IList<WeeklyVideo> GetWeeklyVideoByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<WeeklyVideo>();

            var query = from c in _weeklyVideoRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<WeeklyVideo>();
            foreach (var id in VideoIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertWeeklyVideo(WeeklyVideo model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _weeklyVideoRepository.Insert(model);
        }

        public void UpdateWeeklyVideo(WeeklyVideo model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _weeklyVideoRepository.Update(model);
        }

        #endregion
    }
}
