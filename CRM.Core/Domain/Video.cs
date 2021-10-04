using System;

namespace CRM.Core.Domain
{
    public class Video : BaseEntity
    {
        public string Name { get; set; }
        public string VideoUrl { get; set; }

        public string YoutubeVideoUrl { get; set; }
        public string Key { get; set; }

        //for spanish
       
        public string SpanishVideoUrl { get; set; }

        public string SpanishYoutubeVideoUrl { get; set; }
        public string SpanishKey { get; set; }
        public bool IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
