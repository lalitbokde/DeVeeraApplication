using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class LevelImageListServices : ILevelImageListServices
    {
        #region fields
        private readonly IRepository<LevelImageList> _repository;
        #endregion


        #region ctor
        public LevelImageListServices(IRepository<LevelImageList> repository)
        {
            _repository = repository;
        }
        #endregion

        #region methods

        public void DeleteLevelImage(LevelImageList model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public void DeleteLevelImagesByLevelId(int levelId)
        {
            if (levelId == 0)
                throw new ArgumentNullException(nameof(levelId));

            var data = _repository.Table.Where(i => i.LevelId == levelId).ToList();
            
            foreach(var item in data)
            {
                _repository.Delete(item);
            }
            
        }

        public IList<LevelImageList> GetAllLevelImage()
        {
            var query = from vdo in _repository.Table
                        orderby vdo.Id
                        select vdo;
            var warehouses = query.ToList();
            return warehouses;
        }

        public LevelImageList GetLevelImageById(int Id)
        {
            if (Id == 0)
                return null;


            return _repository.GetById(Id);
        }


        public void InsertLevelImage(LevelImageList model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void UpdateLevelImage(LevelImageList model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _repository.Update(model);
        }

        public List<LevelImageList> GetLevelImageListByLevelId(int id)
        {
            if(id == null)
                throw new ArgumentNullException(nameof(id));

           var data = _repository.Table.Where(i => i.LevelId == id).ToList();

            return data;
        }

        #endregion
    }
}
