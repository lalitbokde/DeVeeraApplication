using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class LevelServices : ILevelServices
    {
        #region fields
        private readonly IRepository<Level> _levelRepository;
        #endregion


        #region ctor
        public LevelServices(IRepository<Level> levelRepository)
        {
            _levelRepository = levelRepository;
        }

        #endregion

        #region Method
        public void DeleteLevel(Level model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _levelRepository.Delete(model);
        }

        public IList<Level> GetAllLevels()
        {
            var query = from vdo in _levelRepository.Table
                        orderby vdo.Title
                        select vdo;
            var warehouses = query.ToList();
            return warehouses;
        }

        public virtual Level GetLevelById(int videoId)
        {
            if (videoId == 0)
                return null;


            return _levelRepository.GetById(videoId);
        }

        public IList<Level> GetLevelByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<Level>();

            var query = from c in _levelRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<Level>();
            foreach (var id in VideoIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertLevel(Level model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _levelRepository.Insert(model);
        }

        public void UpdateLevel(Level model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _levelRepository.Update(model);
        }

        #endregion
    }
}
