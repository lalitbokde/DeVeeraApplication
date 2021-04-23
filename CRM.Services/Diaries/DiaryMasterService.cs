using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CRM.Core.Domain.VideoModules;

namespace CRM.Services
{
    public class DiaryMasterService : IDiaryMasterService
    {
        #region fields
        private readonly IRepository<Diary> _DiaryRepository;
        #endregion


        #region ctor
        public DiaryMasterService(IRepository<Diary> DiaryRepository)
        {
            _DiaryRepository = DiaryRepository;
        }

        #endregion

        #region Method
        public void DeleteDiary(Diary model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _DiaryRepository.Delete(model);
        }

        public IList<Diary> GetAllDiarys()
        {
            var query = from vdo in _DiaryRepository.Table
                        orderby vdo.CreatedOn descending
                        select vdo;
            var Diarys = query.ToList();
            return Diarys;
        }

        public virtual Diary GetDiaryById(int DiaryId)
        {
            if (DiaryId == 0)
                return null;


            return _DiaryRepository.GetById(DiaryId);
        }

        public IList<Diary> GetDiaryByIds(int[] DiaryIds)
        {
            if (DiaryIds == null || DiaryIds.Length == 0)
                return new List<Diary>();

            var query = from c in _DiaryRepository.Table
                        where DiaryIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedUsers = new List<Diary>();
            foreach (var id in DiaryIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedUsers.Add(User);
            }
            return sortedUsers;
        }

        public void InsertDiary(Diary model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _DiaryRepository.Insert(model);
        }

        public void UpdateDiary(Diary model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _DiaryRepository.Update(model);
        }

        #endregion

    }
}
