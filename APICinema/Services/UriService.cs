using APICinema.Pagination;
using Contracts;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Services
{
    public class UriService : IUriService
    {
        private readonly string baseUri;
        public UriService(string baseUri)
        {
            this.baseUri = baseUri;
        }
        public Uri GetMovieUri(int movieId)
        {
            return new Uri(baseUri + ApiRoute.MovieRoute.GetOne.Replace("{movieId}", movieId.ToString()));
        }

        public Uri GetPagedMoviesUri(PaginationFilter filter)
        {
            var uri = new Uri(baseUri);
            if (filter == null) return uri;
            var modifiedUri = QueryHelpers.AddQueryString(uri + ApiRoute.MovieRoute.GetAll, "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
