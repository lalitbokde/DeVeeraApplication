namespace DeVeeraApp.Utils
{
    public class DataSourceRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public DataSourceRequest()
        {
            this.Page = 1;
           // this.PageSize ;
        }

        /// <summary>
        /// Page number
        /// </summary>
        public int Page { get; set; }
        
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; }

        public string SortBy { get; set; }

        public bool GetAll { get; set; }

        public int SortByStatus { get; set; }

        public int WorkRequestStatusId { get; set; }

    }
}
