using CRM.Core.Domain;
using CRM.Core.ViewModels;
using CRM.Data;
using CRM.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services
{
    public class LevelServices : ILevelServices
    {
        #region fields
        private readonly IRepository<Level> _levelRepository;
        protected readonly dbContextCRM _dbContext;
        #endregion


        #region ctor
        public LevelServices(IRepository<Level> levelRepository,
            dbContextCRM dbContext)
        {
            _levelRepository = levelRepository;
            _dbContext = dbContext;
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
                        orderby vdo.Id
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

        public virtual Level GetLevelByLevelNo(int LevelNo)
        {
            if (LevelNo == 0)
                return null;


            return _levelRepository.Table.Where(a=>a.LevelNo==LevelNo).FirstOrDefault();
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

        public Level GetFirstRecord()
        {

            var query = from vdo in _levelRepository.Table
                        orderby vdo.Title
                        select vdo;
            var data = query.Take(1).FirstOrDefault();
            return data;
        }

        #endregion

        public List<LevelViewModel> GetAllLevelsDataSp(
      int page_size = 0,
      int page_num = 0,
      bool GetAll = false,
      string SortBy = "",
       string Title = "",
             string Subtitle = "",
             string VideoName = "",
             int LikeId=0,
             int DisLikeId=0
    )
        {

            try
            {

                string query = @"exec [sp_GetAllLevelsData] @page_size = '" + ((page_size == 0) ? 12 : page_size) + "', " +
                                "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                "@sortBy ='" + SortBy + "' , " +
                                 "@Title ='" + Title + "' , " +
                                  "@Subtitle ='" + Subtitle + "' , " +
                                   "@VideoName ='" + VideoName + "' , " +
                                    "@LikeId ='" + LikeId + "' , " +
                                     "@DisLikeId ='" + DisLikeId + "' , " +
                                "@GetAll ='" + GetAll + "'";


                var data = _dbContext.LevelViewModel.FromSql(query).ToList();
               
                return (data.FirstOrDefault() != null) ? data : new List<LevelViewModel>();

            }
            catch (Exception e)
            {
                return new List<LevelViewModel>();
            }

        }
        public List<ModulesViewModel> GetAllModulesDataSp(int LevelId)
        {
            try
            {
                
                string query = @"exec [sp_GetModulesDetailsByLevelId] @LevelId= '" + LevelId + "'";

                var data = _dbContext.ModulesViewModel.FromSql(query).ToList();

                return (data != null) ? data : new List<ModulesViewModel>();
            }
            catch (Exception ex)
            {
                return null;
            }

        }
      
    }
}
