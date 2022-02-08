using CRM.Core.Domain.VideoModules;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.VideoModules
{
    public class ModuleService:IModuleService
    {
        #region fields
        private readonly IRepository<Modules> _modulesRepository;

        #endregion

        #region ctor
        public ModuleService(IRepository<Modules> modulesRepository)
        {
            _modulesRepository = modulesRepository;
        }
        #endregion


        #region Methods
        public void DeleteModule(Modules Modules)
        {
            if (Modules == null)
                throw new ArgumentNullException(nameof(Modules));

            _modulesRepository.Delete(Modules);
        }

        public IList<Modules> GetAllModules()
        {
            var query = from vdo in _modulesRepository.Table
                        orderby vdo.Id
                        select vdo;
            var modules = query.ToList();
            return modules;
        }

        public Modules GetModuleById(int ModuleId)
        {
            if (ModuleId == 0)
                return null;


            return _modulesRepository.GetById(ModuleId);
        }

        public Modules GetModuleByModuleNo(int moduleNo, int levelid)
        {
            //if (moduleNo == 0)
            //    return null;
            var query = from a in _modulesRepository.Table
                        where a.ModuleNo == moduleNo && a.LevelId == levelid
                        select a;

            var data = query.FirstOrDefault();

            return data;
        }
        public Modules GetModulesdataByLevelId(int levelId)
        {
            if (levelId == 0)
                return null;
            var query = from a in _modulesRepository.Table
                        where a.LevelId == levelId
                        select a;

            var data = query.OrderBy(a => a.ModuleNo).FirstOrDefault();

            return data;
        }

        public IList<Modules> GetModulesByLevelId(int levelId)
        {
            if (levelId == 0)
                return null;
            var query = from a in _modulesRepository.Table
                        where a.LevelId == levelId
                        select a;

            var data = query.OrderBy(a=>a.ModuleNo).ToList();

            return data;
        }

        public void InsertModule(Modules Modules)
        {
            if (Modules == null)
                throw new ArgumentNullException(nameof(Modules));

            _modulesRepository.Insert(Modules);
        }

        public void UpdateModule(Modules Modules)
        {
            if (Modules == null)
                throw new ArgumentNullException(nameof(Modules));



            _modulesRepository.Update(Modules);
        }


        #endregion
    }
}
