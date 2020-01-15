using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreSPATemplate.Services.Common
{
    [BindProperties(SupportsGet = true)]
    public abstract class ListRequest : RequestBase
    {
        protected ListRequest()
        {
            this.PageNumber = 1;
            this.RowsPerPage = 10;
            this.IsPageCountRequired = true;
            this.ReturnAllRecords = false;
        }

        /// <summary>
        /// Starting page of results
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of rows per single page of results
        /// </summary>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// Indicates if a page count is needed in the response or not
        /// </summary>
        /// <remarks>
        /// Set this to false if you do not need the page count.  This will save 1 SQL call.
        /// </remarks>
        public bool IsPageCountRequired { get; set; }

        /// <summary>
        /// Indicates if a a page need to list all (return all) records.
        /// </summary>
        /// <remarks>
        ///  Default is false for paging. Set true if needed.
        /// </remarks>
        public bool ReturnAllRecords { get; set; }

        /// <summary>
        /// Sort field to be used for results.
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Number of records to retrieve
        /// </summary>
        public int TakeCount => this.RowsPerPage <= 0 ? 10 : this.RowsPerPage;

        /// <summary>
        /// Number of records to skip
        /// </summary>
        public int SkipCount => ((this.PageNumber <= 0 ? 1 : this.PageNumber) - 1) * this.TakeCount;
    }
}