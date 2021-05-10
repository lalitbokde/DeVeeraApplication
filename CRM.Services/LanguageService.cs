using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services
{
    public class LanguageService : ILanguageService
    {
        #region fields
        private readonly IRepository<Language> _repository;

        #endregion


        #region ctor

        public LanguageService(IRepository<Language> repository)
        {
            _repository = repository;
        }
        #endregion


        #region methods
        public void DeleteLanguage(Language model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Delete(model);
        }

        public IList<Language> GetAllLanguages()
        {
            var query = from vdo in _repository.Table
                        orderby vdo.Id
                        select vdo;
            var languages = query.ToList();
            return languages;
        }

        public Language GetLanguageById(int languageId)
        {
            if (languageId == 0)
                return null;


            return _repository.GetById(languageId);
        }

        public IList<Language> GetLanguagesByIds(int[] languageIds)
        {
            if (languageIds == null || languageIds.Length == 0)
                return new List<Language>();

            var query = from c in _repository.Table
                        where languageIds.Contains(c.Id)
                        select c;
            var Users = query.ToList();
            //sort by passed identifiers
            var sortedLanguages = new List<Language>();
            foreach (var id in languageIds)
            {
                var User = Users.Find(x => x.Id == id);
                if (User != null)
                    sortedLanguages.Add(User);
            }
            return sortedLanguages;
        }

        public void InsertLanguage(Language model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _repository.Insert(model);
        }

        public void UpdateLanguage(Language model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _repository.Update(model);
        }

        #endregion
    }
}
