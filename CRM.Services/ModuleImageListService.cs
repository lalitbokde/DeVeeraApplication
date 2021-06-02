using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Services
{
    public class ModuleImageListService : IModuleImageListService
    {
        private readonly IRepository<ModuleImageList> _repository;

        public ModuleImageListService(IRepository<ModuleImageList> repository)
        {
            this._repository = repository;
        }

        public void DeleteModuleImageList(ModuleImageList model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public void DeleteModuleImageListByModuleId(int moduleId)
        {
            if (moduleId == 0)
                throw new ArgumentNullException(nameof(moduleId));

            var data = _repository.Table.Where(i => i.ModuleId == moduleId).ToList();
            if (data.Count() > 0)
            {
                foreach (var item in data)
                {
                    _repository.Delete(item);
                }
            }

        }

        public List<ModuleImageList> GetModuleImageListByModuleId(int id)
        {
            if (id == 0)
                throw new ArgumentNullException(nameof(id));

            var data = _repository.Table.Where(i => i.ModuleId == id).ToList();

            return data;
        }

        public void InsertModuleImageList(ModuleImageList model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        //public void UpdateModuleImageList(LevelImageList model)
        //{
        //    if (model == null)
        //        throw new ArgumentNullException(nameof(model));



        //    _repository.Update(model);
        //}


    }
}
