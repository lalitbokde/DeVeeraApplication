using System.Collections.Generic;

namespace CRM.Core.Domain.GoogleTranslate.Objects.LanguageDetection
{
    public class LanguageDetectionData
    {
        public List<List<LanguageDetection>> Detections { get; set; }
    }
}