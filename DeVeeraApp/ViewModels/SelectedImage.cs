using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class SelectedImage : BaseEntityModel
    {
        public bool Selected { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string ImageUrl { get; set; }
    }
}
