using CRM.Core.Domain;
using CRM.Core.ViewModels;
using CRM.Data;
using CRM.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CRM.Services.DashboardQuotes
{
    public class DashboardQuoteService : IDashboardQuoteService
    {
        #region fields
        private readonly IRepository<DashboardQuote> _dashboardQuoteRepository;
        protected readonly dbContextCRM _dbContext;
        #endregion

        #region ctor
        public DashboardQuoteService(IRepository<DashboardQuote> dashboardQuoteRepository, dbContextCRM dbContext)
        {
            _dashboardQuoteRepository = dashboardQuoteRepository;
            _dbContext = dbContext;
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
            var datalist = _dashboardQuoteRepository.Table.Where(val => val.IsDashboardQuote == true).ToList();

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
                        where a.LevelId == LevelId
                        select a;

            var data = query.ToList();

            return data;
        }

        public List<DashBoardQuoteViewModel> GetAllDashboardQuoteSp(
            int page_size = 0,
            int page_num = 0,
            bool GetAll = false,
            string SortBy = "")
        {


            try
            {
                string query = @"exec [sp_GetAllWeeklyInspiringQuote] @page_size = '" + ((page_size == 0) ? 10 : page_size) + "', " +
                                "@page_num  = '" + ((page_num == 0) ? 1 : page_num) + "', " +
                                "@sortBy ='" + SortBy + "' , " +
                                "@GetAll ='" + GetAll + "'";


                var data = _dbContext.DashBoardQuoteViewModels.FromSql(query).ToList();
                return (data.FirstOrDefault() != null) ? data : new List<DashBoardQuoteViewModel>();

            }
            catch (Exception e)
            {
                return new List<DashBoardQuoteViewModel>();
            }

        }

        #endregion
    }
}
