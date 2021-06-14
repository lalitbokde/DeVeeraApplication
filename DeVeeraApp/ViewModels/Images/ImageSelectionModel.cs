using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels.Images
{
    public class ImageSelectionModel
    {
        public ImageSelectionModel()
        {
            ImageSelectionListModel = new List<ImageSelectionListModel>();
        }
        public string ImageFieldId { get; set; }
        public string ImageFieldUrl { get; set; }
        public List<ImageSelectionListModel> ImageSelectionListModel { get; set; }
    }
    public class ImageSelectionListModel : BaseEntityModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Key { get; set; }
        public bool Selected { get; set; }
 
    }
}
