using CRM.Core.Domain;
using CRM.Core.Domain.GoogleTranslate;
using CRM.Core.Domain.GoogleTranslate.GoogleTranslate;
using CRM.Core.Domain.GoogleTranslate.Objects.Translation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.Localization
{
    public class TranslationService : ITranslationService
    {

        #region Fields
        
        private readonly ILocalStringResourcesServices _localStringResourcesServices;

        #endregion

        #region ctor
        public TranslationService(ILocalStringResourcesServices localStringResourcesServices)
        {
            _localStringResourcesServices = localStringResourcesServices;
        }
        #endregion
        public void Translate(string translationStrings, string key)
        {

            GoogleTranslate google = new GoogleTranslate(key);

            //Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
            List<Translation> results = google.Translate(LanguageEnum.English, LanguageEnum.Spanish, translationStrings);

           

            LocaleStringResource data = new LocaleStringResource()
            {
                LanguageId = 5,
                ResourceName = translationStrings,
                ResourceValue = results.FirstOrDefault().TranslatedText
            };
            _localStringResourcesServices.InsertLocalStringResource(data);

        
        }

        public string TranslateLevel(string translationStrings, string key)
        {
            GoogleTranslate google = new GoogleTranslate(key);

            //Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
            List<Translation> results = google.Translate(LanguageEnum.English, LanguageEnum.Spanish, translationStrings);
           var Spanish = results.FirstOrDefault().TranslatedText;
           
            return (Spanish);
        }
    }
}
