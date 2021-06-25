using CRM.Core.Domain;
using System.Collections.Generic;

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
