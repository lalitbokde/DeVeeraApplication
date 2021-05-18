using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain
{
    public class Language : BaseEntity
    {
        public string LanguageName { get; set; }
        public string Abbreviations { get; set; }
        public bool Deleted { get; set; }
    }
}
