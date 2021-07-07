using System.Collections.Generic;
using CRM.Core.Domain;
using CRM.Core.ViewModels;

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

  
        List<DashBoardQuoteViewModel> GetAllDashboardQuoteSp(
         int page_size = 0,
         int page_num = 0,
         bool GetAll = false,
         string SortBy = ""
       );

    }
}
