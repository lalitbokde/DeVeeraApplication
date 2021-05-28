using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Resources;

namespace CRM.Services
{
    public class WeeklyUpdateServices : IWeeklyUpdateServices
    {
        #region fields
        private readonly IRepository<WeeklyUpdate> _weeklyUpdateRepository;

        #endregion

        #region ctor
        public WeeklyUpdateServices(IRepository<WeeklyUpdate> weeklyUpdateRepository)
        {
            _weeklyUpdateRepository = weeklyUpdateRepository;
        }
        #endregion


        #region Methods

        public void DeleteWeeklyUpdate(WeeklyUpdate model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _weeklyUpdateRepository.Delete(model);
        }

        public IList<WeeklyUpdate> GetAllWeeklyUpdates()
        {
            var query = from vdo in _weeklyUpdateRepository.Table
                        orderby vdo.Title
                        select vdo;
            var warehouses = query.ToList();
            return warehouses;
        }
        public WeeklyUpdate GetWeeklyUpdateById(int videoId)
        {
            if (videoId == 0)
                return null;


            return _weeklyUpdateRepository.GetById(videoId);
        }

        public IList<WeeklyUpdate> GetWeeklyUpdatesByIds(int[] VideoIds)
        {
            if (VideoIds == null || VideoIds.Length == 0)
                return new List<WeeklyUpdate>();

            var query = from c in _weeklyUpdateRepository.Table
                        where VideoIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<WeeklyUpdate>();
            foreach (var id in VideoIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertWeeklyUpdate(WeeklyUpdate model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _weeklyUpdateRepository.Insert(model);
        }

        public void UpdateWeeklyUpdate(WeeklyUpdate model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _weeklyUpdateRepository.Update(model);
        }


        public void InActiveAllActiveQuotes(int quoteTypeId)
        {
            var isActiveQuoteList = _weeklyUpdateRepository.Table.Where(q => q.IsActive == true && Convert.ToInt32(q.QuoteType) == quoteTypeId).ToList();

            if(isActiveQuoteList.Count() != 0)
            {
                foreach (var item in isActiveQuoteList)
                {
                    item.IsActive = false;

                    _weeklyUpdateRepository.Update(item);
                }

            }
        }

        public WeeklyUpdate GetWeeklyUpdateByQuoteType(int typeId)
        {
            if (typeId == 0)
                throw new ArgumentNullException(nameof(typeId));

           // var data = _weeklyUpdateRepository.Table.Where(q => q.IsActive == true && Convert.ToInt32(q.QuoteType) == typeId).FirstOrDefault();

            return new WeeklyUpdate();

        }



        public IList<WeeklyUpdate>GetWeeklyUpdatesByQuoteType(int typeId)
        {
            if (typeId == 0)
                throw new ArgumentNullException(nameof(typeId));

            var data = _weeklyUpdateRepository.Table.Where(q => Convert.ToInt32(q.QuoteType) == typeId).ToList();

            return data;

        }
        #endregion
    }
}
