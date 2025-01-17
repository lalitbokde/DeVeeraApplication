﻿using CRM.Core.Domain;
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
        LocaleStringResource GetLocalStringResourceByKey(string Key);
        string GetLocalStringResourceByResourceName(string ResourceName);
        string GetResourceValueByResourceName(string ResourceName);
        string GetResourceValueByResourceNameScreen(string ResourceName);

        //string GetResourceValue(string ResourceName); 
          

    }
}
