using CRM.Core.Domain;
using CRM.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CRM.Services.DashboardQuotes
{
   public class DashboardQuoteService:IDashboardQuoteService
    {
        #region fields
        private readonly IRepository<DashboardQuote> _dashboardQuoteRepository;

        #endregion

        #region ctor
        public DashboardQuoteService(IRepository<DashboardQuote> dashboardQuoteRepository)
        {
            _dashboardQuoteRepository = dashboardQuoteRepository;
        }
        #endregion


        #region Methods
        public void DeleteDashboardQuote(DashboardQuote model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dashboardQuoteRepository.Delete(model);
        }

        public IList<DashboardQuote> GetAllDashboardQutoes()
        {
            var query = from vdo in _dashboardQuoteRepository.Table
                        orderby vdo.Id
                        select vdo;
            var quote = query.ToList();
            return quote;
        }

        public DashboardQuote GetDashboardQutoeById(int quoteId)
        {
            if (quoteId == 0)
                return null;


            return _dashboardQuoteRepository.GetById(quoteId);
        }

        public void InsertDashboardQutoe(DashboardQuote model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dashboardQuoteRepository.Insert(model);
        }

        public void UpdateDashboardQutoe(DashboardQuote model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _dashboardQuoteRepository.Update(model);
        }


        #endregion
    }
}
