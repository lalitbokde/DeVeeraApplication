using CRM.Core.Domain.VideoModules;
using System.Collections.Generic;

namespace CRM.Services.VideoModules
{
    public interface IModuleService
    {
        void DeleteModule(Modules modules);
        IList<Modules> GetAllModules();
        IList<Modules> GetModulesByLevelId(int ModuleId);
        public Modules GetModulesdataByLevelId(int levelId);
        Modules GetModuleByModuleNo(int moduleNo, int levelid);
        Modules GetModuleById(int ModuleId);
        void InsertModule(Modules modules);
        void UpdateModule(Modules modules);
    }
}
