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
    public class FeelGoodStoryServices : IFeelGoodStoryServices
    {
        #region fields
        private readonly IRepository<FeelGoodStory> _repository;
        protected readonly dbContextCRM _dbContext;

        #endregion

        #region ctor

        public FeelGoodStoryServices(IRepository<FeelGoodStory> repository, dbContextCRM dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }
        #endregion


        #region methods

        public void DeleteFeelGoodStory(FeelGoodStory model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public List<FeelGoodViewModel> GetAllFeelGoodStoriesSp(
            int page_size = 0,
            int page_num = 0,
            bool GetAll = false,
            string SortBy = "",
            int ImageId = 0)
        {
            {

                try
                {

                    string query = @"exec [sp_GetAllFeelGoodStories] @page_size = '" + ((page_size == 0) ? 10 : page_size) + "', " +
                                    "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                    "@sortBy ='" + SortBy + "' , " +
                                     "@ImageId ='" + ImageId + "' , " +
                                    "@GetAll ='" + GetAll + "'";


                    var data = _dbContext.FeelGoodViewModels.FromSql(query).ToList();
                    return (data.FirstOrDefault() != null) ? data : new List<FeelGoodViewModel>();

                }
                catch (Exception e)
                {
                    return new List<FeelGoodViewModel>();
                }

            }

        }

        public IList<FeelGoodStory> GetAllFeelGoodStorys()
        {
            var query = from vdo in _repository.Table
                        orderby vdo.Title
                        select vdo;
            var stories = query.ToList();
            return stories;
        }

        public FeelGoodStory GetFeelGoodStoryById(int storyId)
        {
            if (storyId == 0)
                return null;


            return _repository.GetById(storyId);
        }

        public IList<FeelGoodStory> GetFeelGoodStoryByIds(int[] Ids)
        {
            if (Ids == null || Ids.Length == 0)
                return new List<FeelGoodStory>();

            var query = from c in _repository.Table
                        where Ids.Contains(c.Id)
                        select c;
            var images = query.ToList();
            //sort by passed identifiers
            var sortedstories = new List<FeelGoodStory>();
            foreach (var id in Ids)
            {
                var User = sortedstories.Find(x => x.Id == id);
                if (User != null)
                    sortedstories.Add(User);
            }
            return sortedstories;
        }

        public void InsertFeelGoodStory(FeelGoodStory model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void UpdateFeelGoodStory(FeelGoodStory model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _repository.Update(model);
        }

        #endregion
    }
}
