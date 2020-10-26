using APICinema.Models;
using APICinema.Pagination;
using Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Services    
{
    public interface IMovieService
    {
        Task<List<Movie>> GetAllMovies();
        Task<List<Movie>> GettAllMoviesPaged(PaginationFilter filter);
        Task<bool> CreateNewMovie(Movie newMovie);
    }
}
