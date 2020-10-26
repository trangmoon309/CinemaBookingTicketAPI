using APICinema.Services;
using Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Pagination
{
    public class PaginationHelper
    {
        public static PaginationResponse<T> CreatePagedResponse<T>(List<T> response, PaginationFilter filter, IUriService uriService)
        {
            var nextPage = filter.PageNumber >=1 ? uriService.GetPagedMoviesUri(new PaginationFilter
            {
                PageNumber = filter.PageNumber + 1,
                PageSize = filter.PageSize
            }
            ).ToString() : null;

            var prevPage = filter.PageNumber - 1 >=1 ? uriService.GetPagedMoviesUri(new PaginationFilter
            {
                PageNumber = filter.PageNumber - 1,
                PageSize = filter.PageSize
            }
            ).ToString() : null;

            return new PaginationResponse<T>
            {
                data = response,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = prevPage,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }
    }
}
