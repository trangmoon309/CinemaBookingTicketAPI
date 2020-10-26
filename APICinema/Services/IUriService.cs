using APICinema.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Services
{
    public interface IUriService
    {
        Uri GetMovieUri(int movieId);
        Uri GetPagedMoviesUri(PaginationFilter filter);
    }
}
