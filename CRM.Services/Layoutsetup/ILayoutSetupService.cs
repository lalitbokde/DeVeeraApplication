using CRM.Core.Domain.LayoutSetups;
using System.Collections.Generic;

namespace CRM.Services.Layoutsetup
{
    public interface ILayoutSetupService
    {
        void DeleteLayoutSetup(LayoutSetup LayoutSetup);
        IList<LayoutSetup> GetAllLayoutSetups();
        LayoutSetup GetLayoutSetupById(int LayoutSetupId);
        void InsertLayoutSetup(LayoutSetup LayoutSetup);
        void UpdateLayoutSetup(LayoutSetup LayoutSetup);
    }
}
