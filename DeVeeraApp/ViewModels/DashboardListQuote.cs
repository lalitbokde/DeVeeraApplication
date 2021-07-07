using CRM.Core.ViewModels;
using DeVeeraApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeVeeraApp.ViewModels
{
    public class DashboardListQuote: CommonPageModel
    {
        public DashboardListQuote()
        {
            DashboardQuote = new DashboardQuoteModel();
        }

        public DashboardQuoteModel DashboardQuote { get; set; }
        public PagedResult<DashBoardQuoteViewModel> DashboardQuoteListPaged { get; set; }
    }
}
