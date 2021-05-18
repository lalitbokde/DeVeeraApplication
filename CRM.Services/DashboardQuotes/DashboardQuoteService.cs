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

        public IList<DashboardQuote> GetAllDashboardQuotes()
        {
            var query = from vdo in _dashboardQuoteRepository.Table
                        where vdo.Deleted == false
                        orderby vdo.Id
                        select vdo;
            var quote = query.ToList();
            return quote;
        }

        public DashboardQuote GetDashboardQuoteById(int quoteId)
        {
            if (quoteId == 0)
                return null;


            return _dashboardQuoteRepository.GetById(quoteId);
        }

        public void InsertDashboardQuote(DashboardQuote model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _dashboardQuoteRepository.Insert(model);
        }

        public void UpdateDashboardQuote(DashboardQuote model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));



            _dashboardQuoteRepository.Update(model);
        }


        public void InActiveAllDashboardQuotes()
        {
            var datalist = _dashboardQuoteRepository.Table.Where(val => val.IsDashboardQuote == true && val.Deleted == false).ToList();

            if (datalist.Count() != 0)
            {
                foreach (var item in datalist)
                {
                    item.IsDashboardQuote = false;

                    _dashboardQuoteRepository.Update(item);
                }

            }

        }

        public IList<DashboardQuote> GetDashboardQuoteByLevelId(int LevelId)
        {
            if (LevelId == 0)
                return null;
            var query = from a in _dashboardQuoteRepository.Table
                        where a.LevelId == LevelId && a.Deleted == false
                        select a;

            var data = query.ToList();

            return data;
        }

        #endregion
    }
}
