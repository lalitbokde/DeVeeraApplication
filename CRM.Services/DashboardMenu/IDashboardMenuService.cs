using CRM.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Services.DashboardMenu
{
    public interface IDashboardMenuService
    {
        void DeleteDashboardMenu(DashboardMenus menu);
        IList<DashboardMenus> GetAllDashboardMenus();
        DashboardMenus GetDashboardMenuById(int menuId);
        void InsertDashboardMenu(DashboardMenus quote);
        void UpdateDashboardMenu(DashboardMenus quote);

    }
}
