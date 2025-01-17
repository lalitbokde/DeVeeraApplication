﻿using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services
{
    public class LocalStringResourcesServices : ILocalStringResourcesServices
    {
        #region fields
        private readonly IRepository<LocaleStringResource> _repository;
        #endregion

        #region ctor
        public LocalStringResourcesServices(IRepository<LocaleStringResource> repository)
        {
            _repository = repository;
        }
        #endregion

        public void DeleteLocalStringResource(LocaleStringResource model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public IList<LocaleStringResource> GetAllLocalStringResources()
        {
            var query = from vdo in _repository.Table
                        orderby vdo.LanguageId
                        select vdo;
            var stories = query.ToList();
            return stories;
        }

        public LocaleStringResource GetLocalStringResourceById(int? Id)
        {
            if (Id == 0)
                return null;


            return _repository.GetById(Id);
        }
        public LocaleStringResource GetLocalStringResourceByKey(string ResourceName)
        {
            var query = from vdo in _repository.Table
                        where vdo.ResourceName == ResourceName
                        orderby vdo.ResourceName
                        select vdo;
            var stories = query.LastOrDefault();
            return stories ;
        }
        public string GetLocalStringResourceByResourceName(string ResourceName)
        {
            var query = from vdo in _repository.Table
                        where vdo.ResourceName == ResourceName
                        orderby vdo.ResourceName
                        select vdo;
            var stories = query.LastOrDefault();
            return stories!=null ? stories.ResourceName: ResourceName;
        }
        public string GetResourceValueByResourceName(string ResourceName)
        {
            var query = from vdo in _repository.Table
                        where vdo.ResourceName == ResourceName
                        orderby vdo.ResourceName
                        select vdo;
            var stories = query.LastOrDefault();
            return stories != null ? stories.ResourceValue : ResourceName;
        }
        public string GetResourceValueByResourceNameScreen(string ResourceName)
        {
            var query = from vdo in _repository.Table
                        where vdo.ResourceName== ResourceName
                        select vdo;
            var stories = query.LastOrDefault();
            return stories != null ? stories.ResourceValue : ResourceName;
        }
        public void InsertLocalStringResource(LocaleStringResource model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }


        //public string GetResourceValue(string ResourceName)
        //{
        //    var query = from vdo in _repository.Table
        //                where vdo.ResourceName == ResourceName
        //                orderby vdo.ResourceName
        //                select vdo;
        //    var stories = query.LastOrDefault();
        //    if (stories == null)
        //    {
        //        return "";
        //    }
        //    return stories.ResourceValue== null ?  "": stories.ResourceValue;
        //}


        public void UpdateLocalStringResource(LocaleStringResource model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _repository.Update(model);
        }
    }
}
