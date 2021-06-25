using CRM.Core.Domain;
using System.Collections.Generic;

namespace CRM.Services
{
    public interface ILanguageService
    {
        void DeleteLanguage(Language model);


        IList<Language> GetAllLanguages();



        Language GetLanguageById(int videoId);


        IList<Language> GetLanguagesByIds(int[] VideoIds);


        void InsertLanguage(Language model);


        void UpdateLanguage(Language model);

    }
}
