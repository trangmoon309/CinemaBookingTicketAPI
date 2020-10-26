using APICinema.Models;
using APICinema.Pagination;
using APICinema.Services;
using AutoMapper;
using Contracts;
using Contracts.Request;
using Contracts.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Contracts.ApiRoute;

namespace APICinema.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminPolicy")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        private readonly AppDbContext dbContext;
        public MovieController(IMovieService movieService, IMapper mapper, IUriService uriService, AppDbContext dbContext)
        {
            this.movieService = movieService;
            this.mapper = mapper;
            this.uriService = uriService;
            this.dbContext = dbContext;
        }
       
        [HttpGet(ApiRoute.MovieRoute.GetAll)]
        //[Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMovies()
        {
            var result = await movieService.GetAllMovies();
            if (result.Count == 0)
            {
                return null;
            }
            var response = mapper.Map<List<MovieResponse>>(result);   
            return response.ToList();
        }

        [HttpGet("v1/api/pagedmovie")]
        public async Task<ActionResult<IEnumerable<MovieResponse>>> GetAllMoviesPaged([FromQuery] PaginationQuery query)
        {
            var filter = mapper.Map<PaginationFilter>(query);
            var movies = await movieService.GettAllMoviesPaged(filter);
            var moviesResponse = mapper.Map<List<MovieResponse>>(movies);

            if (filter == null || filter.PageSize < 1 || filter.PageNumber < 1)
            {
                return Ok(new PaginationResponse<MovieResponse>(moviesResponse));
            }

            var pagedResponse = PaginationHelper.CreatePagedResponse(moviesResponse, filter, uriService);

            return Ok(pagedResponse);
        }

        [HttpPost(ApiRoute.MovieRoute.Create)]
        public async Task<ActionResult> CreateNewMovie([FromBody] CreateMovieRequest request)
        {
            var createdMovie = mapper.Map<Movie>(request);
            var result = await movieService.CreateNewMovie(createdMovie);
            if (result == false) return BadRequest();

            var locationUri = uriService.GetMovieUri(createdMovie.Id);
            var response = mapper.Map<MovieResponse>(createdMovie);
            return Created(locationUri, response);
        }
        


    }
}
