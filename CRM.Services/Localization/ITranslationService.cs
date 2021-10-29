using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.Localization
{
   public interface ITranslationService
    {

        public void Translate( string  translationStrings, string key);

        public string TranslateLevel(string translationStrings, string key);
        public string TranslateLevelSpanish(string translationStrings, string key);
       public void TranslateEnglishToSpanish(string translationStrings, string key);

        public string GetLocaleStringResource(string translationStrings, string key); 
    }
}
