using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services
{
    public interface ILocalStringResourcesServices
    {
        void DeleteLocalStringResource(LocaleStringResource model);
        void InsertLocalStringResource(LocaleStringResource model);
        void UpdateLocalStringResource(LocaleStringResource model);
        IList<LocaleStringResource> GetAllLocalStringResources();
        LocaleStringResource GetLocalStringResourceById(int Id);

    }
}
