using CRM.Core.Domain;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface ILocalStringResourcesServices
    {
        void DeleteLocalStringResource(LocaleStringResource model);
        void InsertLocalStringResource(LocaleStringResource model);
        void UpdateLocalStringResource(LocaleStringResource model);
        IList<LocaleStringResource> GetAllLocalStringResources();
        LocaleStringResource GetLocalStringResourceById(int? Id);
        string GetLocalStringResourceByResourceName(string ResourceName);
        string GetResourceValueByResourceName(string ResourceName);

    }
}
