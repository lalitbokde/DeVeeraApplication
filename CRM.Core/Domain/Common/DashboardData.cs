using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Core.Domain.Common
{
    public class DashboardData
    {
        public DashboardData()
        {
            this.AvailableTechnicians = new List<SelectListItem>();
        }
        public int TechnicianId { get; set; }
        public int TotalWorkRequests { get; set; }
        public int UpComingRequests { get; set; }
        public int Accepted { get; set; }
        public int Pending { get; set; }
        public int Declined { get; set; }
        public int Cancelled { get; set; }
        public int Completed { get; set; }
        public List<CalendarData> CalendarDatas { get; set; }
        public IList<SelectListItem> AvailableTechnicians { get; set; }

        public class CalendarData:BaseEntity
        {
            public string start { get; set; }           
            public string title { get; set; }
            public string color { get; set; }
            public string url { get; set; }
            public string type { get; set; }
            public string UserId { get; set; }
            public decimal TreatmentPayment { get; set; }
            public int? WorkRequestStatusId { get; set; }
        }
    }
}
