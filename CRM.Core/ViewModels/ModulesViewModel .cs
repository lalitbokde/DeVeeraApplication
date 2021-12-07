using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.ViewModels
{
    public class ModulesViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ModuleNo { get; set; }
        public string VideoName { get; set; }
        public Int32? LikeId { get; set; }
        public Int32? DisLikeId { get; set; }

    }
}
