using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using CRM.Core.Domain.VideoModules;
using CRM.Core.ViewModels;
using CRM.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Services
{
    public class DiaryMasterService : IDiaryMasterService
    {
        #region fields
        private readonly IRepository<Diary> _DiaryRepository;
        protected readonly dbContextCRM _dbContext;
        #endregion


        #region ctor
        public DiaryMasterService(dbContextCRM dbContext, IRepository<Diary> DiaryRepository)
        {
            this._dbContext = dbContext;
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
        public Diary GetDiaryByUserId(int? UserId)
        {
            if (UserId == 0)
                return null;


            return _DiaryRepository.GetById(UserId);
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

        #region Stored procedures
      public List<DiaryViewModel> GetAllDiaries(
      int page_size = 0,
      int page_num = 0,
      bool GetAll = false,
      string SortBy = "",
      string SearchByDate = "",
      int UserId = 0
    )
        {

            try
            {

                string query = @"exec [sp_GetAllDiaries] @page_size = '" + ((page_size == 0) ? 12 : page_size) + "', " +
                                "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                "@sortBy ='" + SortBy + "' , " +
                                "@searchBy ='" + SearchByDate + "' , " +
                                "@UserId ='" + UserId + "' , " +
                                "@GetAll ='" + GetAll + "'";
                var data = _dbContext.DiaryViewModel.FromSql(query).ToList();
                return (data.FirstOrDefault() != null) ? data : new List<DiaryViewModel>();

            }
            catch
            {
                return new List<DiaryViewModel>();
            }

        }

        #endregion
    }
}
