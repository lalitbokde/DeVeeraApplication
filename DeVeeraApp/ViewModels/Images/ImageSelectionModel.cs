using System.Collections.Generic;

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
