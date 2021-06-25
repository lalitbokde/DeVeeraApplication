using CRM.Core.Domain.VideoModules;

namespace CRM.Core.Domain
{
    public class ModuleImageList : BaseEntity
    {
        public int ModuleId { get; set; }
        public int ImageId { get; set; }
        public virtual Modules Modules { get; set; }
        public virtual Image Image { get; set; }


    }
}
