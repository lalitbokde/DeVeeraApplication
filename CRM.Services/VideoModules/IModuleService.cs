using CRM.Core.Domain.VideoModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.VideoModules
{
   public interface IModuleService
    {
        void DeleteModule(Modules modules);
        IList<Modules> GetAllModules();
        IList<Modules> GetModulesByLevelId(int ModuleId);
        Modules GetModuleById(int ModuleId);
        void InsertModule(Modules modules);
        void UpdateModule(Modules modules);
    }
}
