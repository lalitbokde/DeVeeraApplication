using System.Collections.Generic;
using CRM.Core.Domain;

namespace CRM.Services.DashboardQuotes
{
    public interface IDashboardQuoteService
    {
        void DeleteDashboardQuote(DashboardQuote quote);
        IList<DashboardQuote> GetAllDashboardQuotes();
        DashboardQuote GetDashboardQuoteById(int quoteId);
        void InsertDashboardQuote(DashboardQuote quote);
        void UpdateDashboardQuote(DashboardQuote quote);
        void InActiveAllDashboardQuotes();
        IList<DashboardQuote> GetDashboardQuoteByLevelId(int LevelId);
    }
}
