using CRM.Core.Domain;

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
