using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface IModuleImageListService 
    {
        List<ModuleImageList> GetModuleImageListByModuleId(int id);
        void InsertModuleImageList(ModuleImageList model);
        void DeleteModuleImageList(ModuleImageList model);
        void DeleteModuleImageListByModuleId(int moduleId);

    }
}
