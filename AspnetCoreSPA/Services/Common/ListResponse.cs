namespace AspnetCoreSPATemplate.Services.Common
{
    public class ListResponse : ResponseBase
    {
        public ListResponse()
            : base()
        {
        }

        /// <summary>
        /// Number of total pages in the results
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// The current page number.
        /// </summary>
        /// <remarks>
        /// Normally, this will be the same as <c>Request.PageNumber</c>.  If may differ if there are not search results. The previous page will be returned.
        /// </remarks>
        public int PageNumber { get; set; }

        /// <summary>
        /// Number of matching records found
        /// </summary>
        public int RecordCount { get; set; }
    }
}