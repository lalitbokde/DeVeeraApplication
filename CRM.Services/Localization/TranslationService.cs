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

            //GoogleTranslate google = new GoogleTranslate(key);

            ////Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
            //List<Translation> results = google.Translate(LanguageEnum.English, LanguageEnum.Spanish, translationStrings);
            var matchvalue = _localStringResourcesServices.GetLocalStringResourceByKey(translationStrings);
           
           
            if (matchvalue  !=null) 
            {
                matchvalue.ResourceValue = key;
                _localStringResourcesServices.UpdateLocalStringResource(matchvalue);
            }
            else
            {
                
                LocaleStringResource data = new LocaleStringResource()
                {
                    LanguageId = 5,
                    ResourceName = translationStrings,
                    ResourceValue = key
                };
                _localStringResourcesServices.InsertLocalStringResource(data);

            }

        }

        public string TranslateLevel(string translationStrings, string key)
        {
            GoogleTranslate google = new GoogleTranslate(key);

            //Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
            List<Translation> results = google.Translate(LanguageEnum.English, LanguageEnum.Spanish, translationStrings);
           var Spanish = results.FirstOrDefault().TranslatedText;
           
            return (Spanish);
        }

        public string TranslateLevelSpanish(string translationStrings, string key)
        {
            GoogleTranslate google = new GoogleTranslate(key);

            //Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
            List<Translation> results = google.Translate(LanguageEnum.Spanish, LanguageEnum.English, translationStrings);
            var Spanish = results.FirstOrDefault().TranslatedText;

            return (Spanish);
        }

        //public void TranslateEnglishToSpanish(string translationStrings, string key)
        //{

        //    //GoogleTranslate google = new GoogleTranslate(key);

        //    ////Notice that we set the source language to Language.Automatic. This means Google Translate automatically detect the source language before translating.
        //    //List<Translation> results = google.Translate(LanguageEnum.English, LanguageEnum.Spanish, translationStrings);
        //    var matchvalue = _localStringResourcesServices.GetLocalStringResourceByKey(translationStrings);


        //    if (matchvalue != null)
        //    {
        //        matchvalue.ResourceValue = key;
        //        _localStringResourcesServices.UpdateLocalStringResource(matchvalue);
        //    }
        //    else
        //    {
        //        string keylevel = "AIzaSyC2wpcQiQQ7ASdt4vcJHfmly8DwE3l3tqE";
        //        var value=TranslateLevel(translationStrings, keylevel);
        //        LocaleStringResource data = new LocaleStringResource()
        //        {
        //            LanguageId = 5,
        //            ResourceName = translationStrings,
        //            ResourceValue = value
        //        };
        //        _localStringResourcesServices.InsertLocalStringResource(data);

        //    }

        //}

        public string GetLocaleStringResource(string translationStrings, string key)
        {
            var matchvalue = _localStringResourcesServices.GetResourceValueByResourceName(translationStrings);
            return matchvalue == null?"":matchvalue;
        }



    }
}
