using CRM.Core;
using CRM.Core.Domain;
using CRM.Data.Interfaces;
using CRM.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace CRM.Services.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IRepository<LocaleStringResource> _lsrRepository;
        private readonly ISettingService _settingService;
        private readonly ILanguageService _languageService;
        private readonly IWorkContext _workContext;

        public LocalizationService(IRepository<LocaleStringResource> lsrRepository,
                                   ISettingService settingService,
                                   ILanguageService languageService,
                                   IWorkContext workContext)
        {
            _lsrRepository = lsrRepository;
            _settingService = settingService;
            _languageService = languageService;
            _workContext = workContext;
        }


        #region Utilities

        /// <summary>
        /// Insert resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void InsertLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //insert
            _lsrRepository.Insert(resources);
        }

        /// <summary>
        /// Update resources
        /// </summary>
        /// <param name="resources">Resources</param>
        protected virtual void UpdateLocaleStringResources(IList<LocaleStringResource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException(nameof(resources));

            //update
            _lsrRepository.Update(resources);


        }

        private static Dictionary<string, KeyValuePair<int, string>> ResourceValuesToDictionary(IEnumerable<LocaleStringResource> locales)
        {
            //format: <name, <id, value>>
            var dictionary = new Dictionary<string, KeyValuePair<int, string>>();
            foreach (var locale in locales)
            {
                var resourceName = locale.ResourceName.ToLowerInvariant();
                if (!dictionary.ContainsKey(resourceName))
                    dictionary.Add(resourceName, new KeyValuePair<int, string>(locale.Id, locale.ResourceValue));
            }

            return dictionary;
        }

        #endregion




        #region Methods

        /// <summary>
        /// Deletes a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void DeleteLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Delete(localeStringResource);


        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="localeStringResourceId">Locale string resource identifier</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceById(int localeStringResourceId)
        {
            if (localeStringResourceId == 0)
                return null;

            return _lsrRepository.GetById(localeStringResourceId);
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName)
        {
            if (_settingService.GetSetting().LanguageId != 0)

                return GetLocaleStringResourceByName(resourceName, _settingService.GetSetting().LanguageId);

            return null;
        }

        /// <summary>
        /// Gets a locale string resource
        /// </summary>
        /// <param name="resourceName">A string representing a resource name</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <returns>Locale string resource</returns>
        public virtual LocaleStringResource GetLocaleStringResourceByName(string resourceName, int languageId,
            bool logIfNotFound = true)
        {
            var query = from lsr in _lsrRepository.Table
                        orderby lsr.ResourceName
                        where lsr.LanguageId == languageId && lsr.ResourceName == resourceName
                        select lsr;
            var localeStringResource = query.FirstOrDefault();


            return localeStringResource;
        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <returns>Locale string resources</returns>
        public virtual IList<LocaleStringResource> GetAllResources(int languageId)
        {
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;
            var locales = query.ToList();
            return locales;
        }

        /// <summary>
        /// Inserts a locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void InsertLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Insert(localeStringResource);


        }

        /// <summary>
        /// Updates the locale string resource
        /// </summary>
        /// <param name="localeStringResource">Locale string resource</param>
        public virtual void UpdateLocaleStringResource(LocaleStringResource localeStringResource)
        {
            if (localeStringResource == null)
                throw new ArgumentNullException(nameof(localeStringResource));

            _lsrRepository.Update(localeStringResource);


        }

        /// <summary>
        /// Gets all locale string resources by language identifier
        /// </summary>
        /// <param name="languageId">Language identifier</param>
        /// <param name="loadPublicLocales">A value indicating whether to load data for the public store only (if "false", then for admin area only. If null, then load all locales. We use it for performance optimization of the site startup</param>
        /// <returns>Locale string resources</returns>
        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId)
        {



            //we use no tracking here for performance optimization
            //anyway records are loaded only for read-only operations
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;

            return ResourceValuesToDictionary(query);




        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey)
        {

            var currentUserId = _workContext?.CurrentUser?.Id;
            if(currentUserId != null)
            {
               var data =  _settingService.GetSettingByUserId((int)currentUserId);
                if(data != null)
                {
                    return GetResource(resourceKey, data.LanguageId);
                }
                else
                {
                    return GetResource(resourceKey, _settingService.GetSetting().LanguageId);
                }
            }
            else
            {
                if (_settingService.GetSetting().LanguageId != 0)
                    return GetResource(resourceKey, _settingService.GetSetting().LanguageId);
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets a resource string based on the specified ResourceKey property.
        /// </summary>
        /// <param name="resourceKey">A string representing a ResourceKey.</param>
        /// <param name="languageId">Language identifier</param>
        /// <param name="logIfNotFound">A value indicating whether to log error if locale string resource is not found</param>
        /// <param name="defaultValue">Default value</param>
        /// <param name="returnEmptyIfNotFound">A value indicating whether an empty string will be returned if a resource is not found and default value is set to empty string</param>
        /// <returns>A string representing the requested resource string.</returns>
        public virtual string GetResource(string resourceKey, int languageId,
            bool logIfNotFound = true, string defaultValue = "", bool LoadAllLocaleRecordsOnStartup = false)
        {
            var result = string.Empty;
            if (resourceKey == null)
                resourceKey = string.Empty;
            resourceKey = resourceKey.Trim().ToLowerInvariant();
            if (LoadAllLocaleRecordsOnStartup)
            {
                //load all records (we know they are cached)
                var resources = GetAllResourceValues(languageId);
                if (resources.ContainsKey(resourceKey))
                {
                    result = resources[resourceKey].Value;
                }
            }
            else
            {

                var query = from l in _lsrRepository.Table
                            where l.ResourceName == resourceKey
                            && l.LanguageId == languageId
                            select l.ResourceValue;
                return query.FirstOrDefault();




            }

            if (!string.IsNullOrEmpty(result))
                return result;



            if (!string.IsNullOrEmpty(defaultValue))
            {
                result = defaultValue;
            }


            return result;
        }

        public virtual Dictionary<string, KeyValuePair<int, string>> GetAllResourceValues(int languageId, bool? loadPublicLocales)
        {

            //we use no tracking here for performance optimization
            //anyway records are loaded only for read-only operations
            var query = from l in _lsrRepository.Table
                        orderby l.ResourceName
                        where l.LanguageId == languageId
                        select l;

            return ResourceValuesToDictionary(query);





        }

        /// <summary>
        /// Export language resources to XML
        /// </summary>
        /// <param name="language">Language</param>
        /// <returns>Result in XML format</returns>
        /// 






        #endregion

    }
}
 