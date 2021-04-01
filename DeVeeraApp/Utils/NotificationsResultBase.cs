using System;
using System.Collections.Generic;

namespace DeVeeraApp.Utils
{
    public class NotificationsResultBase
    {
        public NotificationsResultBase()
        {
           
        }
        public string Message { get; set; }
        public string Phone { get; set; }
        public bool IsSend { get; set; }
        public int? AssignedBy { get; set; }
        public int? AssignedTo { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
      
    }
}
