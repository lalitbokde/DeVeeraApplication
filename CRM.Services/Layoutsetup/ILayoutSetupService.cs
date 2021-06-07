using CRM.Core.Domain.LayoutSetups;
using System;
using System.Collections.Generic;
using System.Text;

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
