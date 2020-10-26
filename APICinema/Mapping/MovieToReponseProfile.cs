using APICinema.Models;
using AutoMapper;
using Contracts.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICinema.Mapping
{
    public class MovieToReponseProfile : Profile
    {
        public MovieToReponseProfile()
        {
            CreateMap<Movie, MovieResponse>();
        }
    }
}
