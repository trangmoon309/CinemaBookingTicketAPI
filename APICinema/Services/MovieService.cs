using APICinema.Models;
using APICinema.Pagination;
using Contracts.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Services
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext dbContext;
        public MovieService(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateNewMovie(Movie newMovie)
        {
            var check = dbContext.Movies.Where(e => e.Title == newMovie.Title).Select(x => x);
            if (check.Count() !=0 ) return false;
            await dbContext.Movies.AddAsync(newMovie);
            var result = await dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async  Task<List<Movie>> GetAllMovies()
        {
            return await dbContext.Movies.Include(x => x.Times).ToListAsync();
        }

        public async Task<List<Movie>> GettAllMoviesPaged(PaginationFilter filter = null)
        {
            if(filter == null)
            {
                return await dbContext.Movies.Include(x => x.Times).ToListAsync();
            }
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            return await dbContext.Movies.Include(x => x.Times).Skip(skip).Take(filter.PageSize).ToListAsync();
        }

    }
}
