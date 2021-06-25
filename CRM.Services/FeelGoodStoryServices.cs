using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services
{
    public class FeelGoodStoryServices : IFeelGoodStoryServices
    {
        #region fields
        private readonly IRepository<FeelGoodStory> _repository;

        #endregion

        #region ctor

        public FeelGoodStoryServices(IRepository<FeelGoodStory> repository)
        {
            _repository = repository;
        }
        #endregion


        #region methods

        public void DeleteFeelGoodStory(FeelGoodStory model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
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
