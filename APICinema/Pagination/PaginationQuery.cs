using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Pagination
{
    public class PaginationQuery
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public PaginationQuery(int PageSize, int PageNumber)
        {
            this.PageNumber = PageNumber;
            this.PageSize = PageSize;
        }

        public PaginationQuery()
        {
            this.PageNumber = 1;
            this.PageSize = 3;
        }
    }
}
