namespace CRM.Core.Domain
{
    public class LevelImageList : BaseEntity
    {
        public int LevelId { get; set; }
        public virtual Level Level { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }
    }
}
