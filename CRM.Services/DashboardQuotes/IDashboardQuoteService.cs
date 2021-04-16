using System;
using System.Collections.Generic;
using System.Text;
using CRM.Core.Domain;

namespace CRM.Services.DashboardQuotes
{
    public interface IDashboardQuoteService
    {
        void DeleteDashboardQuote(DashboardQuote quote);
        IList<DashboardQuote> GetAllDashboardQutoes();
        DashboardQuote GetDashboardQutoeById(int quoteId);
        void InsertDashboardQutoe(DashboardQuote quote);
        void UpdateDashboardQutoe(DashboardQuote quote);
    }
}
