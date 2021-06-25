using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.DashboardMenu
{
    public class DashboardMenuService: IDashboardMenuService
    {
        #region fields
        private readonly IRepository<DashboardMenus> _dashboardMenuRepository;

        #endregion

        #region ctor
        public DashboardMenuService(IRepository<DashboardMenus> dashboardMenuRepository)
        {
            _dashboardMenuRepository = dashboardMenuRepository;
        }
        #endregion


        #region Methods
        public void DeleteDashboardMenu(DashboardMenus model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dashboardMenuRepository.Delete(model);
        }

        public IList<DashboardMenus> GetAllDashboardMenus()
        {
            var query = from vdo in _dashboardMenuRepository.Table
                        orderby vdo.Id
                        select vdo;
            var quote = query.ToList();
            return quote;
        }

        public DashboardMenus GetDashboardMenuById(int menuId)
        {
            if (menuId == 0)
                return null;


            return _dashboardMenuRepository.GetById(menuId);
        }

        public void InsertDashboardMenu(DashboardMenus model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dashboardMenuRepository.Insert(model);
        }

        public void UpdateDashboardMenu(DashboardMenus model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _dashboardMenuRepository.Update(model);
        }

        #endregion
    }
}
