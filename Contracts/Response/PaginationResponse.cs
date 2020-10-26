using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Response
{
    public class PaginationResponse<T>
    {
        public PaginationResponse() { }

        public IEnumerable<T> data {get;set;}
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }
    
        public PaginationResponse(IEnumerable<T> data)
        {
            this.data = data;
        }
    }
}
