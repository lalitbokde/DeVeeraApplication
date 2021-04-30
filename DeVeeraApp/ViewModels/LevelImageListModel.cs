using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class LevelImageListModel
    {
        public int LevelId { get; set; }
        public Level Level { get; set; }
        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
